using ScrapeWeb.Core.Abstraction;

namespace ScrapeWeb.Core.ViewModels
{
    public class PostViewModel
    {
        protected IPost Post;

        public PostViewModel(IPost post)
        {
            Post = post;
        }


    }
}
