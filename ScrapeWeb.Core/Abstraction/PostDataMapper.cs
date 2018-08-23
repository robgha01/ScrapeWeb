namespace ScrapeWeb.Core.Abstraction
{
    public abstract class PostDataMapper<TPost>
    {
        public abstract TPost Post { get; protected set; }
        public abstract PostDataMapper<TPost> Map(CategoryType type, object value);
    }
}
