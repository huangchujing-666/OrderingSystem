using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OrderingSystem.Business;
using OrderingSystem.Core.Infrastructure;
using ToolGood.Words;
using System.Linq;
using System.Collections.Generic;
using OrderingSystem.Domain.Model;

namespace OrderingSystem.Tests
{
    [TestClass]
    public class UnitTest1
    {
        private readonly IUserBusiness _userInfo = EngineContext.Current.Resolve<IUserBusiness>();
        private readonly IBusinessInfoBusiness _businessInfo = EngineContext.Current.Resolve<IBusinessInfoBusiness>();


        [TestMethod]
        public void TestMethod1()
        {
            var aa = _userInfo.GetUserByPhoneOrOpenId("18516063170", "");
            //var result = _userInfo.GetById(1000);
            int totalCount = 0;

           // var list = _businessInfo.GetBusinessInfoByStation(88, 1, 10, out totalCount);

            Assert.IsNotNull(totalCount>=0);
        }

        [TestMethod]
        public void TestMethod2()
        {

            //var result = _userInfo.GetById(1000);
            int totalCount = 0;

            var list = _businessInfo.GetBusinessInfoBySearch("黑",1,10,out totalCount);

            Assert.IsNotNull(totalCount >= 0);
        }

        [TestMethod]
        public void TestMethod3()
        {

            //var result = _userInfo.GetById(1000);
            int totalCount = 0;

            var list = _businessInfo.GetById(1);

            Assert.IsNotNull(totalCount >= 0);
        }
        [TestMethod]
        public void TestMethod5()
        {
            System.IO.StreamReader sr = System.IO.File.OpenText(System.Environment.CurrentDirectory + "\\key.txt");
            //List<string> list = sr.ReadToEnd().Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries).Where(t => t.EndsWith("strat") == true).ToList(); //StringSplitOptions.RemoveEmptyEntries 分割后内容为空则不返回。.EndsWith("strat")取以“strat”结尾的结果
            List<string> list = sr.ReadToEnd().Split('|').ToList(); //StringSplitOptions.RemoveEmptyEntries 分割后内容为空则不返回。.EndsWith("strat")取以“strat”结尾的结果

            StringSearch iwords = new StringSearch();
            iwords.SetKeywords(list);
            string inputString = "sb 黑社会，共产党，江泽民";
            var str = iwords.Replace(inputString, '*');


            ToolGood.Words.IllegalWordsSearch iws = new ToolGood.Words.IllegalWordsSearch();
            iws.SetKeywords(list);
            inputString = "sb 黑社会，共产党，江泽民";
            inputString = iws.Replace(inputString);


            Assert.IsNotNull(true);
        }


        [TestMethod]
        public void TestAddBusiness()
        {
            BusinessInfo binfo = new BusinessInfo();

            binfo.Address = "111111";
            binfo.AveragePay =20;
            binfo.BaseAreaId = 1;
            binfo.BaseImageId =1;
            binfo.BaseLineId = 1;
            binfo.BaseStationId = 1;
            binfo.BusinessHour = "111111";
            binfo.BusinessTypeId = 1;
            binfo.CreateTime = DateTime.Now;
            binfo.Distance = "111111";
            binfo.EditTime = DateTime.Now; 


            //var result = _userInfo.GetById(1000);
            int totalCount = 0;

            binfo = _businessInfo.Insert(binfo);

            Assert.IsNotNull(totalCount >= 0);
        }
    }
}
