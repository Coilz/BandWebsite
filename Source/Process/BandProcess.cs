using System;
using System.Linq;
using Ewk.BandWebsite.Catalogs;
using Ewk.BandWebsite.Domain.AppModel;

namespace Ewk.BandWebsite.Process
{
    public class BandProcess : ProcessBase, IBandProcess
    {
        public BandProcess(ICatalogsContainer catalogsContainer)
            : base(catalogsContainer)
        {
        }

        #region Implementation of IBandProcess

        public Band EnsureBandExists()
        {
            var bands = AppRepository.GetAllBands();

            var band =  bands.SingleOrDefault();
            if (band == null)
            {
                var cryptographyProcess = CatalogsConsumerHelper.ResolveCatalogsConsumer<ICryptographyProcess>(CatalogsContainer);

                band = new Band
                           {
                               InitVector = cryptographyProcess.GenerateSecureRandomNumber(16),
                               Passphrase = cryptographyProcess.GenerateSecureRandomNumber(32),
                               SaltValue = cryptographyProcess.GenerateSecureRandomNumber(16),
                           };

                AppRepository.AddBand(band);
            }

            return band;
        }

        public Band GetBand()
        {
            return AppRepository.GetBand();
        }

        public Band UpdateBand(Band band)
        {
            if (band == null) throw new ArgumentNullException("band");

            return AppRepository.UpdateBand(band);
        }

        #endregion
    }
}