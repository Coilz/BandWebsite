using System;
using System.Collections.Generic;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.Blog;

namespace Ewk.BandWebsite.Web.UI.ModelMappers
{
    public interface IBlogArticleMapper
    {
        BlogArticle Map(AddBlogArticleModel model, Guid userId);
        BlogArticle Map(UpdateBlogArticleModel model, Guid blogArticleId);
        UpdateBlogArticleModel MapToUpdate(BlogArticle blogArticle);
        BlogArticleDetailsModel MapToDetail(BlogArticle blogArticle, User author);
        ItemListModel<BlogArticleDetailsModel> Map(IEnumerable<BlogArticle> blogArticle, IEnumerable<User> authors);
    }
}