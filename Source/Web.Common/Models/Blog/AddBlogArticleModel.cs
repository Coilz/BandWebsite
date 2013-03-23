using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.Common.Models.Blog
{
    public class AddBlogArticleModel
    {
        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}