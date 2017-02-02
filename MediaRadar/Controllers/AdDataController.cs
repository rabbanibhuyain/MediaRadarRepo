using MediaRadar.ServiceReference;
using Microsoft.Ajax.Utilities;
using System;
using System.Linq;
using System.Web.Mvc;

namespace MediaRadar.Controllers
{
    public class AdDataController : Controller
    {
        private DateTime FromDate = new DateTime(2011, 01, 01);
        private DateTime ToDate = new DateTime(2011, 04, 01);
        // GET: AdData
        public ActionResult FullList()
        {
            Ad[] adList = GetAdsDataList();
            return View(adList);
        }
        public ActionResult ListOfAdsForCover()
        {
            Ad[] adListArr = GetAdsDataList();

            var adList = from ad in adListArr
                         where ad.Position == "Cover" && ad.NumPages >= 0.5M
                         select ad;

            return View(adList);

        }

        public ActionResult TopAdsByPageCoverage()
        {
            Ad[] adListArr = GetAdsDataList();

            var adList = (from ad in adListArr
                          orderby ad.NumPages descending
                          select ad).DistinctBy(d => d.Brand.BrandName).Take(5);

            return View(adList);

        }

        public ActionResult TopBrandByPageCoverage()
        {
            Ad[] adListArr = GetAdsDataList();

            var adList = adListArr.GroupBy(ad => ad.Brand.BrandName).Select(cl => new Ad
            {
                Brand = cl.First().Brand,
                NumPages = cl.Sum(c => c.NumPages)
            }).ToList().OrderByDescending(x => x.NumPages).Take(5);


            return View(adList);

        }

        private Ad[] GetAdsDataList()
        {
            AdDataServiceClient adc = new AdDataServiceClient();
            Ad[] retAdList = adc.GetAdDataByDateRange(FromDate, ToDate);
            adc.Close();
            return retAdList;
        }
    }
}