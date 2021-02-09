using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrderingSystem.Core.Utils
{
    public static class LongitudeToolService
    {
        /// <summary>
        /// 地球半径
        /// </summary>
        private const double EARTH_RADIUS = 6378.137;
        /// <summary>
        /// 纬度转换度 Math.PI / 180.0 =1度
        /// </summary>
        /// <param name="d">需要转换的纬度</param>
        /// <returns>已经转换的纬度</returns>
        private static double rad(double d)
        {
            return d * Math.PI / 180.0;
        }
        /// <summary>
        /// 输入两个经度维度，返回两点间的距离
        /// </summary>
        /// <param name="lng1">第一个点的纬度</param>
        /// <param name="lat1">第一个点的经度</param>
        /// <param name="lng2">第二个点的纬度</param>
        /// <param name="lat2">第二个点的经度</param>
        /// <returns></returns>
        public static int GetDistance(double lng1, double lat1, double lng2, double lat2)
        {
            if (lng1 == 0 || lat1 == 0)
            {
                return 0;
            }
            double radLat1 = rad(lat1);
            double radLat2 = rad(lat2);
            double a = radLat1 - radLat2;
            double b = rad(lng1) - rad(lng2);

            double s = 2 * Math.Asin(Math.Sqrt(Math.Pow(Math.Sin(a / 2), 2) +
             Math.Cos(radLat1) * Math.Cos(radLat2) * Math.Pow(Math.Sin(b / 2), 2)));
            s = s * EARTH_RADIUS;
            s = Math.Round(s * 10000) / 10000 * 1000;
            return Convert.ToInt32(s);
        }
    }
}
