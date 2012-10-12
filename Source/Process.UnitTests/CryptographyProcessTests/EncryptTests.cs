using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.CryptographyProcessTests
{
    [TestClass]
    public class EncryptTests : CryptographyProcessTestBase
    {
        [TestMethod]
        public void When_Encrypt_is_called_multiple_times_then_GetBand_on_the_AppRepository_is_called_once()
        {
            const string value = "This is a value that will be encrypted";
            var band = BandCreator.CreateSingle();

            AppRepository
                .Expect(repository =>
                        repository.GetBand())
                .Return(band)
                .Repeat.Once();
            AppRepository.Replay();

            Process.Encrypt(value);
            Process.Encrypt(value);

            AppRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_Encrypt_is_called_multiple_times_then_the_result_differs_each_time()
        {
            const string value = "This is a value that will be encrypted";
            var band = BandCreator.CreateSingle();

            AppRepository
                .Expect(repository =>
                        repository.GetBand())
                .Return(band)
                .Repeat.Once();
            AppRepository.Replay();

            var result1 = Process.Encrypt(value);
            var result2 = Process.Encrypt(value);

            Assert.IsFalse(string.IsNullOrEmpty(result1));
            Assert.IsFalse(string.IsNullOrEmpty(result2));
            Assert.AreNotEqual(result1, result2);

            AppRepository.VerifyAllExpectations();
        }
    }
}
