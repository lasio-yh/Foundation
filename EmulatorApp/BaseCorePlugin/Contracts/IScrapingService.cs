namespace Foundation.Contracts
{
    public interface IScrapingService
    {
        void OnScraping(string uri, object type);
    }
}
