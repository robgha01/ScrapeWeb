using System.Windows.Controls.Primitives;

namespace ScrapeWeb.Abstraction
{
    public interface IMultiSelectCollectionView
    {
        void AddControl(Selector selector);
        void RemoveControl(Selector selector); 
    }
}