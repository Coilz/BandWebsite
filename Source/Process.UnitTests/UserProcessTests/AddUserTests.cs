using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.UserProcessTests
{
    [TestClass]
    public class AddUserTests : UserProcessTestBase
    {
        [TestMethod]
        public void When_AddUser_is_called_with_a_valid_User_then_AddUser_on_the_BandRepository_is_called_with_that_User()
        {
            var user = UserCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.AddUser(user))
                .Return(user)
                .Repeat.Once();
            BandRepository.Replay();

            var result = Process.AddUser(user);

            Assert.AreEqual(user.Id, result.Id);

            BandRepository.VerifyAllExpectations();
        }
    }
}
