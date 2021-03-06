﻿using Ewk.BandWebsite.Adapters;
using Ewk.BandWebsite.Adapters.Flickr;
using Ewk.BandWebsite.Adapters.SoundCloud;
using Ewk.BandWebsite.Adapters.Youtube;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Catalogs.MongoDb;
using Ewk.BandWebsite.Common;
using Ewk.BandWebsite.Process;
using Ewk.BandWebsite.Repositories;
using Ewk.BandWebsite.Web.Common;
using Ewk.BandWebsite.Web.Common.ModelMappers;
using Ewk.Configuration;
using Microsoft.Practices.Unity;

namespace Ewk.BandWebsite.Web.UI
{
    public class UnityContainerSetup
    {
        public void Initialize()
        {
            var unityContainer = new UnityContainer();
            unityContainer

                // Mappers
                .RegisterType<IAudioAdapterSettingsMapper, AudioAdapterSettingsMapper>()
                .RegisterType<IBandMapper, BandMapper>()
                .RegisterType<IBlogArticleMapper, BlogArticleMapper>()
                .RegisterType<IUserMapper, UserMapper>()
                .RegisterType<IPerformanceMapper, PerformanceMapper>()
                .RegisterType<IPhotoAdapterSettingsMapper, PhotoAdapterSettingsMapper>()
                .RegisterType<IVideoAdapterSettingsMapper, VideoAdapterSettingsMapper>()

                // Processes
                .RegisterType<IAudioProcess, AudioProcess>()
                .RegisterType<IBandProcess, BandProcess>()
                .RegisterType<IBlogProcess, BlogProcess>()
                .RegisterType<IUserProcess, UserProcess>()
                .RegisterType<IPerformanceProcess, PerformanceProcess>()
                .RegisterType<IPhotoProcess, PhotoProcess>()
                .RegisterType<IVideoProcess, VideoProcess>()
                .RegisterType<ICryptographyProcess, CryptographyProcess>()

                // Adapters
                .RegisterType<IAudioAdapter, AudioAdapter>()
                .RegisterType<IPhotoAdapter, PhotoAdapter>()
                .RegisterType<IVideoAdapter, VideoAdapter>()

                // Repositories
                .RegisterType<IAppRepository, AppRepository>()
                .RegisterType<IBandRepository, BandRepository>()

                // Catalogs
                .RegisterType<ICatalogsContainer, CatalogsContainer>()
                .RegisterType<IAppCatalog, AppCatalog>()
                .RegisterType<IBandCatalog, BandCatalog>()

                .RegisterType<IBandIdResolver, ThreadContextAccessor>()
                .RegisterType<IBandIdInstaller, ThreadContextAccessor>();

            DependencyConfiguration.DependencyResolver = new UnityDependencyResolver(unityContainer);
        }
    }
}