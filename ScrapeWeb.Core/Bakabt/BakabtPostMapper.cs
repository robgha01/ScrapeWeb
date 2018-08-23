using ScrapeWeb.Core.Abstraction;

namespace ScrapeWeb.Core.Bakabt
{
    public class BakabtPostMapper : PostDataMapper<PostEntity>
    {
        public override sealed PostEntity Post { get; protected set; }

        public BakabtPostMapper()
        {
            Post = new PostEntity();
        }

        public override PostDataMapper<PostEntity> Map(CategoryType type, object value)
        {
            switch (type)
            {
                case CategoryType.Cover:
                    // this is the cover image.
                    Post.Cover = (byte[])value;
                    break;
                    case CategoryType.Category:
                    Post.Category = (string)value;
                    break;
            }

            return this;
        }
    }
}
