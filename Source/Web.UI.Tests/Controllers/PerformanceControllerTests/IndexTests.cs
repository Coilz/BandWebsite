using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Ewk.BandWebsite.Web.UI.Models;
using Ewk.BandWebsite.Web.UI.Models.Performance;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.PerformanceControllerTests
{
    [TestClass]
    public class IndexTests : PerformanceControllerTestBase
    {
        [TestMethod]
        public void When_Index_is_called_GetPerformances_on_IPerformanceProcess_is_called_and_the_result_is_mapped_with_PerformanceMapper()
        {
            var performances = PerformanceCreator.CreateCollection();

            PerformanceProcess
                .Expect(process =>
                        process.GetPerformances())
                .Return(performances)
                .Repeat.Once();
            PerformanceProcess.Replay();

            var performanceDetailsModelcollection = CreatePerformanceDetailsModelCollection();

            PerformanceMapper
                .Expect(mapper =>
                        mapper.Map(
                            Arg<IEnumerable<Performance>>.Matches(articles =>
                                                                  performances.All(performance =>
                                                                                   articles.Any(article =>
                                                                                                article.Id == performance.Id)))))
                .Return(performanceDetailsModelcollection)
                .Repeat.Once();
            PerformanceMapper.Replay();

            var result = Controller.Index().Result as ViewResult;
            Assert.IsNotNull(result);

            var model = result.Model as ItemListModel<PerformanceDetailsModel>;
            Assert.IsNotNull(model);

            PerformanceProcess.VerifyAllExpectations();
            PerformanceMapper.VerifyAllExpectations();
        }
    }
}