using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Process.UnitTests.CryptographyProcessTests
{
    [TestClass]
    public class DecryptTests : CryptographyProcessTestBase
    {
        [TestMethod]
        public void When_Decrypt_is_called_multiple_times_then_GetBand_on_the_AppRepository_is_called_once()
        {
            const string value = "This is a value that will be encrypted";
            var band = BandCreator.CreateSingle();

            AppRepository
                .Expect(repository =>
                        repository.GetBand())
                .Return(band)
                .Repeat.Once();
            AppRepository.Replay();

            var encryptedValue = Process.Encrypt(value);
            Process.Decrypt(encryptedValue);
            Process.Decrypt(encryptedValue);

            AppRepository.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_Decrypt_is_called_with_an_encrypted_value_then_the_result_is_the_original_value()
        {
            const string value = "This is a value that will be encrypted";
            var band = BandCreator.CreateSingle();

            AppRepository
                .Expect(repository =>
                        repository.GetBand())
                .Return(band)
                .Repeat.Once();
            AppRepository.Replay();

            var encryptedValue = Process.Encrypt(value);
            var decryptedValue = Process.Decrypt(encryptedValue);

            Assert.AreEqual(value, decryptedValue);

            AppRepository.VerifyAllExpectations();
        }
    }
}