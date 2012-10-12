using System;
using System.Threading.Tasks;
using Ewk.BandWebsite.Common;
using Ewk.Configuration;

namespace Ewk.BandWebsite.Catalogs
{
    /// <summary>
    /// Contains methods that create and manages instances of <see cref="ICatalogsContainer"/> and contains methods
    /// that resolve instances of classes that are constructed with an <see cref="ICatalogsContainer"/>.
    /// </summary>
    public static class CatalogsConsumerHelper
    {
        /// <summary>
        /// Resolves instances of classes that are constructed with an <see cref="ICatalogsContainer"/>.
        /// </summary>
        /// <typeparam name="TCatalogsConsumer">The <see cref="Type"/> to resolve.</typeparam>
        /// <param name="catalogsContainer">The <see cref="ICatalogsContainer"/> that is used as the constructor parameter.</param>
        /// <returns>An instance of the requested <see cref="Type"/>.</returns>
        public static TCatalogsConsumer ResolveCatalogsConsumer<TCatalogsConsumer>(ICatalogsContainer catalogsContainer)
            where TCatalogsConsumer : class
        {
            return DependencyConfiguration.DependencyResolver.Resolve<TCatalogsConsumer>(
                new DependencyResolverParameterOverride("catalogsContainer", catalogsContainer));
        }

        /// <summary>
        /// Executes an <see cref="Action{ICatalogsContainer}"/> that takes an <see cref="ICatalogsContainer"/> as parameter within
        /// a using block.
        /// </summary>
        /// <param name="action">The <see cref="Action{ICatalogsContainer}"/> that is executed within the using block.</param>
        public static void ExecuteWithCatalogScope(Action<ICatalogsContainer> action)
        {
            using (var catalogsContainer = DependencyConfiguration.DependencyResolver.Resolve<ICatalogsContainer>())
            {
                action(catalogsContainer);
            }
        }

        /// <summary>
        /// Executes an <see cref="Action{ICatalogsContainer}"/> that takes an <see cref="ICatalogsContainer"/> as parameter within
        /// a using block.
        /// </summary>
        /// <param name="action">The <see cref="Action{ICatalogsContainer}"/> that is executed within the using block.</param>
        public static async Task ExecuteWithCatalogScopeAsync(Action<ICatalogsContainer> action)
        {
            await TaskStarter.StartNew(() => ExecuteWithCatalogScope(action));
        }

        /// <summary>
        /// Executes an <see cref="Func{ICatalogsContainer, TReturn}"/> that takes an <see cref="ICatalogsContainer"/> as parameter within
        /// a using block.
        /// </summary>
        /// <typeparam name="TReturn">The <see cref="Type"/> of the instance that is returned.</typeparam>
        /// <param name="function">The <see cref="Func{ICatalogsContainer, TReturn}"/> that is executed within the using block.</param>
        /// <returns>An instance of the specified <see cref="Type"/>.</returns>
        public static TReturn ExecuteWithCatalogScope<TReturn>(Func<ICatalogsContainer, TReturn> function)
        {
            using (var catalogsContainer = DependencyConfiguration.DependencyResolver.Resolve<ICatalogsContainer>())
            {
                return function(catalogsContainer);
            }
        }

        /// <summary>
        /// Executes an <see cref="Func{ICatalogsContainer, TReturn}"/> that takes an <see cref="ICatalogsContainer"/> as parameter within
        /// a using block.
        /// </summary>
        /// <typeparam name="TReturn">The <see cref="Type"/> of the instance that is returned.</typeparam>
        /// <param name="function">The <see cref="Func{ICatalogsContainer, TReturn}"/> that is executed within the using block.</param>
        /// <returns>An instance of the specified <see cref="Type"/>.</returns>
        public static async Task<TReturn> ExecuteWithCatalogScopeAsync<TReturn>(Func<ICatalogsContainer, TReturn> function)
        {
            return await TaskStarter.StartNew(() => ExecuteWithCatalogScope(function));
        }
    }
}