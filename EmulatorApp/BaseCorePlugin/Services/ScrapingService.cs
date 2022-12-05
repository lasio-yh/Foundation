using Foundation.Contracts;
using HtmlAgilityPack;

namespace Foundation.Services
{
    public class ScrapingService : IScrapingService
    {
        public delegate void PushHandler(object args);
        private PushHandler _push;
        public ScrapingService(PushHandler callback)
        {
            if (callback == null)
                return;

            _push = callback;
        }

        public void OnScraping(string uri, object type)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(uri);
            var documentMetaList = doc.DocumentNode.SelectNodes("//meta");
            if (documentMetaList == null)
                return;

            var documentVendorList = doc.DocumentNode.SelectNodes("//div[@class='prod-ctl-or-fbt-recommend impression-log']");
            if (documentVendorList == null)
                return;

            var documentOriginPriceList = doc.DocumentNode.SelectNodes("//span[@class='origin-price']");
            if (documentOriginPriceList == null)
                return;

            var documentDiscountRateList = doc.DocumentNode.SelectNodes("//span[@class='discount-rate']");
            if (documentDiscountRateList == null)
                return;

            var documentTotalPriceList = doc.DocumentNode.SelectNodes("//span[@class='total-price']");
            if (documentTotalPriceList == null)
                return;

            _push(documentMetaList);
            _push(documentVendorList);
            _push(documentOriginPriceList);
            _push(documentDiscountRateList);
            _push(documentTotalPriceList);
        }
    }
}