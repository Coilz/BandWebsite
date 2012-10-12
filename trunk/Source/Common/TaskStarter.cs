using System;
using System.Threading.Tasks;
using Ewk.Configuration;

namespace Ewk.BandWebsite.Common
{
    public static class TaskStarter
    {
        public static Task StartNew(Action action)
        {
            var bandIdResolver = DependencyConfiguration.DependencyResolver.Resolve<IBandIdResolver>();
            var bandId = bandIdResolver.GetBandId();

            return Task.Factory.StartNew(() =>
                {
                    var bandIdInstaller = DependencyConfiguration.DependencyResolver.Resolve<IBandIdInstaller>();
                    bandIdInstaller.SetBandId(bandId);

                    action();
                });
        }

        public static Task<T> StartNew<T>(Func<T> function)
        {
            var bandIdResolver = DependencyConfiguration.DependencyResolver.Resolve<IBandIdResolver>();
            var bandId = bandIdResolver.GetBandId();

            return Task.Factory.StartNew(() =>
            {
                var bandIdInstaller = DependencyConfiguration.DependencyResolver.Resolve<IBandIdInstaller>();
                bandIdInstaller.SetBandId(bandId);

                return function();
            });
        }
    }
}