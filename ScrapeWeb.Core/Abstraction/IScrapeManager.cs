namespace ScrapeWeb.Core.Abstraction
{
    public interface IScrapeManager<out TPost, in TScrapeFilter>
        where TPost : IPost
        where TScrapeFilter : IScrapeFilter
    {
        IScrapeManager<TPost, TScrapeFilter> AddFilter(TScrapeFilter filter);
        TPost Process(string url);
    }
}