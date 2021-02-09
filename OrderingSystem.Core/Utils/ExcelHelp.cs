using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Core.Utils
{
    public class ExcelHelp
    {

        #region Export

        /// <summary>
        /// 由DataTable导出Excel
        /// </summary>
        /// <param name="sourceTable">要导出数据的DataTable</param>
        /// <param name="sheetName">工作薄名称,可选</param>
        /// <param name="colNames">需要导出的列名,可选</param>
        /// <param name="colAliasNames">导出的列名重命名,可选</param>
        /// <param name="colDataFormats">列格式化集合,可选</param>
        /// <param name="sheetSize">指定每个工作薄显示的记录数，可选（不指定或指定小于0，则表示只生成一个工作薄）</param>
        /// <returns></returns>
        public static MemoryStream ToExcel(DataTable sourceTable, string sheetName = "sheet", string[] colNames = null,
                                           IDictionary<string, string> colAliasNames = null, Dictionary<string, string> colDataFormats = null, int sheetSize = 0)
        {
            if (sourceTable.Rows.Count <= 0) return null;
            //创建Excel文件的对象
            HSSFWorkbook workbook = new HSSFWorkbook();
            ICellStyle headerCellStyle = GetCellStyle(workbook, true);
            if (colNames == null || colNames.Length <= 0)
            {
                colNames = sourceTable.Columns.Cast<DataColumn>().OrderBy(c => c.Ordinal).Select(c => c.ColumnName).ToArray();
            }
            IEnumerable<DataRow> batchDataRows, dataRows = sourceTable.Rows.Cast<DataRow>();
            int sheetCount = 0;
            if (sheetSize <= 0)
            {
                sheetSize = sourceTable.Rows.Count;
            }
            while ((batchDataRows = dataRows.Take(sheetSize)).Count() > 0)
            {
                Dictionary<int, ICellStyle> colStyles = new Dictionary<int, ICellStyle>();
                ISheet sheet = workbook.CreateSheet(sheetName + (++sheetCount).ToString());
                IRow headerRow = sheet.CreateRow(0);
                // handling header.
                for (int i = 0; i < colNames.Length; i++)
                {
                    ICell headerCell = headerRow.CreateCell(i);
                    if (colAliasNames != null && colAliasNames.ContainsKey(colNames[i]))
                    {
                        headerCell.SetCellValue(colAliasNames[colNames[i]]);
                    }
                    else
                    {
                        headerCell.SetCellValue(colNames[i]);
                    }
                    headerCell.CellStyle = headerCellStyle;
                    sheet.AutoSizeColumn(headerCell.ColumnIndex);
                    if (colDataFormats != null && colDataFormats.ContainsKey(colNames[i]))
                    {
                        colStyles[headerCell.ColumnIndex] = GetCellStyleWithDataFormat(workbook, colDataFormats[colNames[i]]);
                    }
                    else
                    {
                        colStyles[headerCell.ColumnIndex] = GetCellStyle(workbook);
                    }
                }
                // handling value.
                int rowIndex = 1;
                foreach (DataRow row in batchDataRows)
                {
                    IRow dataRow = sheet.CreateRow(rowIndex);
                    for (int i = 0; i < colNames.Length; i++)
                    {
                        ICell cell = dataRow.CreateCell(i);
                        SetCellValue(cell, (row[colNames[i]] ?? "").ToString(), sourceTable.Columns[colNames[i]].DataType, colStyles);
                        ReSizeColumnWidth(sheet, cell);
                    }
                    rowIndex++;
                }
                sheet.ForceFormulaRecalculation = true;
                dataRows = dataRows.Skip(sheetSize);
            }
            MemoryStream ms = new MemoryStream();
            workbook.Write(ms);
            workbook.Close();
            ms.Seek(0, SeekOrigin.Begin);
            return ms;
        }

        #endregion

        #region Import

        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <param name="sheetName">Excel工作表名称</param>
        /// <param name="headerRowIndex">Excel表头行索引</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable(string filePath, string sheetName, int headerRowIndex)
        {
            if (string.IsNullOrEmpty(filePath)) return null;
            XSSFWorkbook workbook;
            using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                workbook = new XSSFWorkbook(file);
            }
            ISheet sheet = null;
            int sheetIndex = -1;
            if (int.TryParse(sheetName, out sheetIndex))
            {
                sheet = workbook.GetSheetAt(sheetIndex);
            }
            else
            {
                sheet = workbook.GetSheet(sheetName);
            }
            DataTable table = GetDataTableFromSheet(sheet, headerRowIndex);
            workbook.Close();
            workbook = null;
            sheet = null;
            return table;
        }

        /// <summary>
        /// 由Excel导入DataTable
        /// </summary>
        /// <param name="filePath">路径</param>
        /// <returns>DataTable</returns>
        public static DataTable ToDataTable(string filePath)
        {
            XSSFWorkbook workbook;
            try
            {
                using (FileStream file = new FileStream(filePath, FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(file);
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            ISheet sheet = workbook.GetSheetAt(0);
            System.Collections.IEnumerator rows = sheet.GetRowEnumerator();
            DataTable dt = new DataTable();
            for (int j = 0; j < (sheet.GetRow(0).LastCellNum); j++)
            {
                dt.Columns.Add(Convert.ToChar(((int)'A') + j).ToString());
            }
            while (rows.MoveNext())
            {
                XSSFRow row = (XSSFRow)rows.Current;
                DataRow dr = dt.NewRow();
                for (int i = 0; i < row.LastCellNum; i++)
                {
                    ICell cell = row.GetCell(i);
                    if (cell == null)
                    {
                        dr[i] = null;
                    }
                    else
                    {
                        dr[i] = cell.ToString();
                    }
                }
                dt.Rows.Add(dr);
            }
            workbook.Close();
            workbook = null;
            workbook = null;
            return dt;
        }

        #endregion

        #region Common方法

        /// <summary>
        /// 从工作表中生成DataTable
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="headerRowIndex"></param>
        /// <returns></returns>
        private static DataTable GetDataTableFromSheet(ISheet sheet, int headerRowIndex)
        {
            DataTable table = new DataTable();

            if (sheet.LastRowNum <= 0) return table;

            IRow headerRow = sheet.GetRow(headerRowIndex);
            int cellCount = headerRow.LastCellNum;

            for (int i = headerRow.FirstCellNum; i < cellCount; i++)
            {
                if (headerRow.GetCell(i) == null || headerRow.GetCell(i).StringCellValue.Trim() == "")
                {
                    // 如果遇到第一个空列，则不再继续向后读取
                    cellCount = i;
                    break;
                }
                DataColumn column = new DataColumn(headerRow.GetCell(i).StringCellValue);
                table.Columns.Add(column);
            }

            for (int i = (headerRowIndex + 1); i <= sheet.LastRowNum; i++)
            {
                IRow row = sheet.GetRow(i);
                //如果遇到某行的第一个单元格的值为空，则不再继续向下读取
                if (row != null && row.GetCell(0) != null && !string.IsNullOrEmpty(row.GetCell(0).ToString()))
                {
                    DataRow dataRow = table.NewRow();

                    for (int j = row.FirstCellNum; j < cellCount; j++)
                    {
                        ICell cell = row.GetCell(j);
                        dataRow[j] = cell == null ? "" : cell.ToString();
                    }

                    table.Rows.Add(dataRow);
                }
            }

            return table;
        }

        /// <summary>
        /// 依据值类型为单元格设置值
        /// </summary>
        /// <param name="cell"></param>
        /// <param name="value"></param>
        /// <param name="colType"></param>
        /// <param name="colStyles"></param>
        private static void SetCellValue(ICell cell, string value, Type colType, IDictionary<int, ICellStyle> colStyles)
        {
            string dataFormatStr = null;
            switch (colType.ToString())
            {
                case "System.String": //字符串类型
                    cell.SetCellType(CellType.String);
                    cell.SetCellValue(value);
                    break;
                case "System.DateTime": //日期类型
                    DateTime dateV;
                    if (DateTime.TryParse(value, out dateV))
                    {
                        cell.SetCellValue(dateV);
                    }
                    dataFormatStr = "yyyy/mm/dd hh:mm:ss";
                    break;
                case "System.Boolean": //布尔型
                    bool boolV = false;
                    if (bool.TryParse(value, out boolV))
                    {
                        cell.SetCellType(CellType.Boolean);
                        cell.SetCellValue(boolV);
                    }
                    break;
                case "System.Int16": //整型
                case "System.Int32":
                case "System.Int64":
                case "System.Byte":
                    int intV = 0;
                    if (int.TryParse(value, out intV))
                    {
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue(intV);
                    }
                    dataFormatStr = "0";
                    break;
                case "System.Decimal": //浮点型
                case "System.Double":
                    double doubV = 0;
                    if (double.TryParse(value, out doubV))
                    {
                        cell.SetCellType(CellType.Numeric);
                        cell.SetCellValue(doubV);
                    }
                    dataFormatStr = "0.00";
                    break;
                case "System.DBNull": //空值处理
                    cell.SetCellType(CellType.Blank);
                    cell.SetCellValue("");
                    break;
                default:
                    cell.SetCellType(CellType.Unknown);
                    cell.SetCellValue(value);
                    break;
            }

            if (!string.IsNullOrEmpty(dataFormatStr) && colStyles[cell.ColumnIndex].DataFormat <= 0) //没有设置，则采用默认类型格式
            {
                colStyles[cell.ColumnIndex] = GetCellStyleWithDataFormat(cell.Sheet.Workbook, dataFormatStr);
            }
            cell.CellStyle = colStyles[cell.ColumnIndex];
        }

        /// <summary>
        /// 创建单元格样式并设置数据格式化规则
        /// </summary>
        /// <param name="workbook">workbook</param>
        /// <param name="format">格式化字符串</param>
        private static ICellStyle GetCellStyleWithDataFormat(IWorkbook workbook, string format)
        {
            var style = GetCellStyle(workbook);
            var dataFormat = workbook.CreateDataFormat();
            short formatId = -1;
            if (dataFormat is HSSFDataFormat)
            {
                formatId = HSSFDataFormat.GetBuiltinFormat(format);
            }
            if (formatId != -1)
            {
                style.DataFormat = formatId;
            }
            else
            {
                style.DataFormat = dataFormat.GetFormat(format);
            }
            return style;
        }

        /// <summary>
        /// 根据单元格内容重新设置列宽
        /// </summary>
        /// <param name="sheet"></param>
        /// <param name="cell"></param>
        private static void ReSizeColumnWidth(ISheet sheet, ICell cell)
        {
            int cellLength = (Encoding.Default.GetBytes(cell.ToString()).Length + 2) * 256;
            const int maxLength = 60 * 256; //255 * 256;
            if (cellLength > maxLength) //当单元格内容超过30个中文字符（英语60个字符）宽度，则强制换行
            {
                cellLength = maxLength;
                cell.CellStyle.WrapText = true;
            }
            int colWidth = sheet.GetColumnWidth(cell.ColumnIndex);
            if (colWidth < cellLength)
            {
                sheet.SetColumnWidth(cell.ColumnIndex, cellLength);
            }
        }

        /// <summary>
        /// 创建单元格样式
        /// </summary>
        /// <param name="workbook">workbook</param>
        /// <param name="isHeaderRow">是否获取头部样式</param>
        /// <returns></returns>
        private static ICellStyle GetCellStyle(IWorkbook workbook, bool isHeaderRow = false)
        {
            ICellStyle style = workbook.CreateCellStyle();

            if (isHeaderRow)
            {
                style.FillPattern = FillPattern.SolidForeground;
                style.FillForegroundColor = NPOI.HSSF.Util.HSSFColor.Grey25Percent.Index;
                IFont f = workbook.CreateFont();
                f.Boldweight = (short)FontBoldWeight.Bold;
                style.SetFont(f);
            }

            style.BorderBottom = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderLeft = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderRight = NPOI.SS.UserModel.BorderStyle.Thin;
            style.BorderTop = NPOI.SS.UserModel.BorderStyle.Thin;
            return style;
        }

        /// <summary>
        /// 文件名称生成
        /// </summary>
        /// <param name="head">头标记</param>
        /// <returns></returns>
        public static string CreateFileName(string head)
        {
            string str = string.Format("{0}{1}.xls", head, DateTime.Now.ToString("yyyyMMddHHmmssffff"));
            return str;
        }

        #endregion
    }
}
