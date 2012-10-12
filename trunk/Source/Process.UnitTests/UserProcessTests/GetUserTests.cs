using System;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.UserProcessTests
{
    [TestClass]
    public class GetUserTests : UserProcessTestBase
    {
        [TestMethod]
        public void When_GetUser_is_called_with_a_valid_Guid_then_GetUser_on_the_BandRepository_is_called_with_that_Guid()
        {
            var user = UserCreator.CreateSingle();

            BandRepository
                .Expect(repository =>
                        repository.GetUser(user.Id))
                .Return(user)
                .Repeat.Once();
            BandRepository.Replay();

            Process.GetUser(user.Id);

            BandRepository.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(ArgumentNullException))]
        public void When_GetUser_is_called_with_an_empty_Guid_for_userId_then_an_ArgumentNullException_is_thrown()
        {
            Process.GetUser(Guid.Empty);
        }
    }
}