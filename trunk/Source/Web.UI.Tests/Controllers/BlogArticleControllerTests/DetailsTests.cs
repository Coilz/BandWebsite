using System.Web.Mvc;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Web.UI.Tests.Controllers.BlogArticleControllerTests
{
    [TestClass]
    public class DetailsTests : BlogArticleControllerTestBase
    {
        [TestMethod]
        public void When_Details_is_called_GetBlogArticle_on_IBlogProcess_is_called_with_the_correct_parameter_and_the_result_is_mapped_with_BlogArticleMapper()
        {
            var entity = BlogArticleCreator.CreateSingle();
            var author = UserCreator.CreateSingle();
            entity.AuthorId = author.Id;

            UserProcess
                .Expect(process =>
                        process.GetUser(entity.AuthorId))
                .Return(author)
                .Repeat.Once();
            UserProcess.Replay();

            BlogProcess
                .Expect(process =>
                        process.GetBlogArticle(entity.Id))
                .Return(entity)
                .Repeat.Once();
            BlogProcess.Replay();

            var detailsModel = CreateBlogArticleDetailsModel(entity.Id);

            BlogArticleMapper
                .Expect(mapper =>
                        mapper.MapToDetail(entity, author))
                .Return(detailsModel)
                .Repeat.Once();
            BlogArticleMapper.Replay();

            var result = Controller.Details(entity.Id).Result as ViewResult; ;
            Assert.IsNotNull(result);
            
            BlogProcess.VerifyAllExpectations();
            UserProcess.VerifyAllExpectations();
            BlogArticleMapper.VerifyAllExpectations();
        }
    }
}