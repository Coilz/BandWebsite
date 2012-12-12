using System;
using System.Collections.Generic;
using System.Globalization;
using Ewk.BandWebsite.Common;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Web.UI.ModelMappers;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.AudioAdapterSettings;
using Ewk.BandWebsite.Web.UI.Models.Blog;
using Ewk.BandWebsite.Web.UI.Models.Home;
using Ewk.BandWebsite.Web.UI.Models.Performance;
using Ewk.BandWebsite.Web.UI.Models.PhotoAdapterSettings;
using Ewk.BandWebsite.Web.UI.Models.VideoAdapterSettings;
using Ewk.UnitTests;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers
{
    public abstract class ControllerTestBase : UnitTestBase
    {
        protected IBandIdResolver BandIdResolver { get; private set; }
        protected IBandIdInstaller BandIdInstaller { get; private set; }

        protected IAudioAdapterSettingsMapper AudioAdapterSettingsMapper { get; private set; }
        protected IBandMapper BandMapper { get; private set; }
        protected IBlogArticleMapper BlogArticleMapper { get; private set; }
        protected IPerformanceMapper PerformanceMapper { get; private set; }
        protected IPhotoAdapterSettingsMapper PhotoAdapterSettingsMapper { get; private set; }
        protected IVideoAdapterSettingsMapper VideoAdapterSettingsMapper { get; private set; }

        protected IAudioProcess AudioProcess { get; private set; }
        protected IBandProcess BandProcess { get; private set; }
        protected IBlogProcess BlogProcess { get; private set; }
        protected IUserProcess UserProcess { get; private set; }
        protected IPerformanceProcess PerformanceProcess { get; private set; }
        protected IPhotoProcess PhotoProcess { get; private set; }
        protected IVideoProcess VideoProcess { get; private set; }

        protected override void AdditionalSetup()
        {
            base.AdditionalSetup();

            BandIdResolver = MockHelper.CreateAndRegisterMock<IBandIdResolver>();
            BandIdResolver
                .Expect(resolver => resolver.GetBandId())
                .Return(Guid.NewGuid())
                .Repeat.Any();
            BandIdResolver.Replay();

            BandIdInstaller = MockHelper.CreateAndRegisterMock<IBandIdInstaller>();

            AudioAdapterSettingsMapper = MockHelper.CreateAndRegisterMock<IAudioAdapterSettingsMapper>();
            BandMapper = MockHelper.CreateAndRegisterMock<IBandMapper>();
            BlogArticleMapper = MockHelper.CreateAndRegisterMock<IBlogArticleMapper>();
            PerformanceMapper = MockHelper.CreateAndRegisterMock<IPerformanceMapper>();
            PhotoAdapterSettingsMapper = MockHelper.CreateAndRegisterMock<IPhotoAdapterSettingsMapper>();
            VideoAdapterSettingsMapper = MockHelper.CreateAndRegisterMock<IVideoAdapterSettingsMapper>();

            AudioProcess = MockHelper.CreateAndRegisterMock<IAudioProcess>();
            BandProcess = MockHelper.CreateAndRegisterMock<IBandProcess>();
            BlogProcess = MockHelper.CreateAndRegisterMock<IBlogProcess>();
            UserProcess = MockHelper.CreateAndRegisterMock<IUserProcess>();
            PerformanceProcess = MockHelper.CreateAndRegisterMock<IPerformanceProcess>();
            PhotoProcess = MockHelper.CreateAndRegisterMock<IPhotoProcess>();
            VideoProcess = MockHelper.CreateAndRegisterMock<IVideoProcess>();
        }

        #region About

        protected static AboutModel CreateAboutModel()
        {
            return new AboutModel
                       {
                           DateFounded = DateTime.Now.AddMonths(-4),
                           Info = "Info",
                       };
        }

        protected static AboutUpdateModel CreateUpdateAboutModel()
        {
            return new AboutUpdateModel
                       {
                           DateFounded = DateTime.Now.AddMonths(-4),
                           Info = "Info",
                       };
        }

        #endregion

        #region BlogArticle

        protected static ItemListModel<BlogArticleDetailsModel> CreateBlogArticleDetailsModelCollection()
        {
            return new ItemListModel<BlogArticleDetailsModel>
                       {
                           Title = "Blog",
                           Items = new List<BlogArticleDetailsModel>
                                       {
                                           CreateBlogArticleDetailsModel(Guid.NewGuid()),
                                           CreateBlogArticleDetailsModel(Guid.NewGuid()),
                                           CreateBlogArticleDetailsModel(Guid.NewGuid()),
                                           CreateBlogArticleDetailsModel(Guid.NewGuid()),
                                           CreateBlogArticleDetailsModel(Guid.NewGuid()),
                                           CreateBlogArticleDetailsModel(Guid.NewGuid()),
                                       }
                       };
        }

        protected static BlogArticleDetailsModel CreateBlogArticleDetailsModel(Guid id)
        {
            return new BlogArticleDetailsModel
                       {
                           Id = id,
                           Content = string.Format(CultureInfo.InvariantCulture, "Content {0}", id),
                           Title = string.Format(CultureInfo.InvariantCulture, "Title {0}", id),
                           CreationDate = DateTime.UtcNow.AddDays(-10),
                           ModificationDate = DateTime.UtcNow.AddDays(-3),
                           PublishDate = DateTime.UtcNow.AddDays(-1),
                           AuthorName = string.Format(CultureInfo.InvariantCulture, "AuthorName {0}", id),
                       };
        }

        protected static UpdateBlogArticleModel CreateUpdateBlogArticleModel(Guid id)
        {
            return new UpdateBlogArticleModel
                       {
                           Content = string.Format(CultureInfo.InvariantCulture, "Content {0}", id),
                           Title = string.Format(CultureInfo.InvariantCulture, "Title {0}", id),
                       };
        }

        #endregion

        #region Performance

        protected static ItemListModel<PerformanceDetailsModel> CreatePerformanceDetailsModelCollection()
        {
            return new ItemListModel<PerformanceDetailsModel>
            {
                Title = "Shows",
                Items = new List<PerformanceDetailsModel>
                                       {
                                           CreatePerformanceDetailsModel(Guid.NewGuid()),
                                           CreatePerformanceDetailsModel(Guid.NewGuid()),
                                           CreatePerformanceDetailsModel(Guid.NewGuid()),
                                           CreatePerformanceDetailsModel(Guid.NewGuid()),
                                           CreatePerformanceDetailsModel(Guid.NewGuid()),
                                           CreatePerformanceDetailsModel(Guid.NewGuid()),
                                       }
            };
        }

        protected static PerformanceDetailsModel CreatePerformanceDetailsModel(Guid id)
        {
            var startDateTime = DateTime.UtcNow.AddDays(10);

            return new PerformanceDetailsModel
                       {
                           Id = id,
                           CreationDate = DateTime.UtcNow.AddDays(-10),
                           ModificationDate = DateTime.UtcNow.AddDays(-3),
                           Date = startDateTime,
                           StartTime = startDateTime,
                           EndTime = startDateTime.AddHours(4),
                           City = string.Format(CultureInfo.InvariantCulture, "City {0}", id),
                           VenueName = string.Format(CultureInfo.InvariantCulture, "VenueName {0}", id),
                       };
        }

        protected static UpdatePerformanceModel CreateUpdatePerformanceModel(Guid id)
        {
            var startDateTime = DateTime.UtcNow.AddDays(10);

            return new UpdatePerformanceModel
                       {
                           Date = startDateTime,
                           StartTime = startDateTime,
                           EndTime = startDateTime.AddHours(4),
                           City = string.Format(CultureInfo.InvariantCulture, "City {0}", id),
                           VenueName = string.Format(CultureInfo.InvariantCulture, "VenueName {0}", id),
                       };
        }

        #endregion

        #region PhotoAdapterSettings

        protected static PhotoAdapterSettingsDetailsModel CreatePhotoAdapterSettingsDetailsModel(Guid id)
        {
            return new PhotoAdapterSettingsDetailsModel
                       {
                           Id = id,
                           CreationDate = DateTime.UtcNow.AddDays(-10),
                           ModificationDate = DateTime.UtcNow.AddDays(-3),
                           FullName = string.Format(CultureInfo.InvariantCulture, "FullName {0}", id),
                           UserId = string.Format(CultureInfo.InvariantCulture, "UserId {0}", id),
                           UserName = string.Format(CultureInfo.InvariantCulture, "UserName {0}", id),
                           SetName = string.Format(CultureInfo.InvariantCulture, "SetName {0}", id),
                       };
        }

        protected static UpdatePhotoAdapterSettingsModel CreateUpdatePhotoAdapterSettingsModel(Guid id)
        {
            return new UpdatePhotoAdapterSettingsModel
                       {
                           FullName = string.Format(CultureInfo.InvariantCulture, "FullName {0}", id),
                           UserId = string.Format(CultureInfo.InvariantCulture, "UserId {0}", id),
                           UserName = string.Format(CultureInfo.InvariantCulture, "UserName {0}", id),
                           SetName = string.Format(CultureInfo.InvariantCulture, "SetName {0}", id),
                       };
        }

        #endregion

        #region AudioAdapterSettings

        protected static AudioAdapterSettingsDetailsModel CreateAudioAdapterSettingsDetailsModel(Guid id)
        {
            return new AudioAdapterSettingsDetailsModel
            {
                Id = id,
                CreationDate = DateTime.UtcNow.AddDays(-10),
                ModificationDate = DateTime.UtcNow.AddDays(-3),
                FullName = string.Format(CultureInfo.InvariantCulture, "FullName {0}", id),
                UserId = string.Format(CultureInfo.InvariantCulture, "UserId {0}", id),
                UserName = string.Format(CultureInfo.InvariantCulture, "UserName {0}", id),
                SetName = string.Format(CultureInfo.InvariantCulture, "SetName {0}", id),
            };
        }

        protected static UpdateAudioAdapterSettingsModel CreateUpdateAudioAdapterSettingsModel(Guid id)
        {
            return new UpdateAudioAdapterSettingsModel
            {
                FullName = string.Format(CultureInfo.InvariantCulture, "FullName {0}", id),
                UserId = string.Format(CultureInfo.InvariantCulture, "UserId {0}", id),
                UserName = string.Format(CultureInfo.InvariantCulture, "UserName {0}", id),
                SetName = string.Format(CultureInfo.InvariantCulture, "SetName {0}", id),
            };
        }

        #endregion

        #region VideoAdapterSettings

        protected static VideoAdapterSettingsDetailsModel CreateVideoAdapterSettingsDetailsModel(Guid id)
        {
            return new VideoAdapterSettingsDetailsModel
            {
                Id = id,
                CreationDate = DateTime.UtcNow.AddDays(-10),
                ModificationDate = DateTime.UtcNow.AddDays(-3),
                FullName = string.Format(CultureInfo.InvariantCulture, "FullName {0}", id),
                UserId = string.Format(CultureInfo.InvariantCulture, "UserId {0}", id),
                UserName = string.Format(CultureInfo.InvariantCulture, "UserName {0}", id),
                SetName = string.Format(CultureInfo.InvariantCulture, "SetName {0}", id),
            };
        }

        protected static UpdateVideoAdapterSettingsModel CreateUpdateVideoAdapterSettingsModel(Guid id)
        {
            return new UpdateVideoAdapterSettingsModel
            {
                FullName = string.Format(CultureInfo.InvariantCulture, "FullName {0}", id),
                UserId = string.Format(CultureInfo.InvariantCulture, "UserId {0}", id),
                UserName = string.Format(CultureInfo.InvariantCulture, "UserName {0}", id),
                SetName = string.Format(CultureInfo.InvariantCulture, "SetName {0}", id),
            };
        }

        #endregion
    }
}