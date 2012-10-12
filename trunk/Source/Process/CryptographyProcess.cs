using System.Security.Cryptography;
using System.Text;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Common.Cryptography;

namespace Ewk.BandWebsite.Process
{
    public class CryptographyProcess : ProcessBase, ICryptographyProcess
    {
        private const int MinSaltLen = 8;
        private const int MaxSaltLen = 16;
        private const int KeySize = 256;
        private const int PasswordIterations = 20;

        private RijndaelEnhanced _cryptographer;

        public CryptographyProcess(ICatalogsContainer catalogsContainer)
            : base(catalogsContainer)
        {
        }

        #region Implementation of ICryptographyProcess

        public string Encrypt(string value)
        {
            return Cryptographer.Encrypt(value);
        }

        public string Decrypt(string encryptedValue)
        {
            return Cryptographer.Decrypt(encryptedValue);
        }

        public string GenerateSecureRandomNumber(int lenth)
        {
            var randomNumber = new byte[lenth];
            using (var cryptoServiceProvider = new RNGCryptoServiceProvider())
            {
                cryptoServiceProvider.GetBytes(randomNumber);

                return Encoding.ASCII.GetString(randomNumber);
            }
        }

        #endregion

        private RijndaelEnhanced Cryptographer
        {
            get
            {
                var band = AppRepository.GetBand();

                return _cryptographer ??
                       (_cryptographer =
                        new RijndaelEnhanced(band.Passphrase,
                                             band.InitVector,
                                             MinSaltLen,
                                             MaxSaltLen,
                                             KeySize,
                                             band.SaltValue,
                                             PasswordIterations));
            }
        }
    }
}