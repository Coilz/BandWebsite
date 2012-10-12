using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.UnitTests.ModelCreators
{
    public static class BlogArticleCreator
    {
        public static IQueryable<BlogArticle> CreateCollection()
        {
            return new List<BlogArticle>
                       {
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                           CreateSingle(),
                       }
                .AsQueryable();
        }

        public static BlogArticle CreateSingle()
        {
            var entity = new BlogArticle
                                  {
                                      ModificationDate = DateTime.UtcNow.AddMonths(-1),
                                      AuthorId = UserCreator.CreateSingle().Id,
                                  };

            entity.Title = string.Format(CultureInfo.InvariantCulture, "Title {0}", entity.Id);
            entity.Content = string.Format(CultureInfo.InvariantCulture, "Content {0}", entity.Id);

            return entity;
        }
    }
}