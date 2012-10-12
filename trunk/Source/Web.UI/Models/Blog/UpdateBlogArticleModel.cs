using System;
using System.ComponentModel.DataAnnotations;

namespace Ewk.BandWebsite.Web.UI.Models.Blog
{
    public class UpdateBlogArticleModel
    {
        public Guid Id { get; set; }

        [Required]
        public string Title { get; set; }

        [Required]
        [DataType(DataType.MultilineText)]
        public string Content { get; set; }
    }
}