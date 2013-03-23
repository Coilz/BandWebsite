using System.Collections.Generic;

namespace Ewk.BandWebsite.Web.Common.Models
{
    public class ItemListModel<T>
    {
        public string Title { get; set; }

        public IEnumerable<T> Items { get; set; } 
    }
}