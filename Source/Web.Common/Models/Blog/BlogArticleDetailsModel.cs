using System;
using System.ComponentModel.DataAnnotations;
using Ewk.BandWebsite.Common;

namespace Ewk.BandWebsite.Web.Common.Models.Blog
{
    public class BlogArticleDetailsModel
    {
        private string _title;

        public Guid Id { get; set; }
        public string Title
        {
            get { return _title ?? ValueNotSetConstants.RequiredStringNotSet; }
            set { _title = value; }
        }
        public string Content { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }
        public DateTime PublishDate { get; set; }

        [Display(Name = "Author: ")]
        public string AuthorName { get; set; }
    }
}