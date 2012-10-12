using System;
using System.Collections.Generic;
using System.Linq;
using Ewk.BandWebsite.Domain.BandModel;
using Ewk.BandWebsite.UnitTests.ModelCreators;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Rhino.Mocks;

namespace Ewk.BandWebsite.Repositories.UnitTests.BandRepositoryTests
{
    [TestClass]
    public class AdapterSettingsTests : BandRepositoryTestBase
    {
        [TestMethod]
        public void When_GetPhotoAdapterSettings_is_called_then_the_PhotoAdapterSettings_is_retrieved_from_the_collection()
        {
            const string adapterName = "AdapterName";
            var adapterSettings = AdapterSettingsCreator.CreateSingle(adapterName);

            BandCatalog
                .Expect(catalog => catalog.AdapterSettings)
                .Return(new List<AdapterSettings>{adapterSettings}.AsQueryable())
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.GetAdapterSettings(adapterName);

            Assert.IsNotNull(result);
            Assert.AreEqual(adapterSettings, result);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_GetAllPhotoAdapterSettingss_is_called_and_there_are_more_than_one_PhotoAdapterSettingss_instances_stored_then_only_the_first_instance_is_retrieved_from_the_collection()
        {
            const string adapterName = "AdapterName";
            var adapterSettings = AdapterSettingsCreator.CreateCollection(adapterName, adapterName, adapterName);

            BandCatalog
                .Expect(catalog => catalog.AdapterSettings)
                .Return(adapterSettings)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.GetAdapterSettings(adapterName);

            Assert.IsNotNull(result);
            Assert.AreEqual(adapterSettings.First(), result);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod, ExpectedException(typeof(InvalidOperationException))]
        public void When_GetPhotoAdapterSettings_is_called_and_there_is_no_PhotoAdapterSettings_in_the_collection_then_an_InvalidOperationException_is_thrown()
        {
            const string adapterName = "AdapterName";
            var adapterSettingss = new List<AdapterSettings>().AsQueryable();

            BandCatalog
                .Expect(catalog => catalog.AdapterSettings)
                .Return(adapterSettingss)
                .Repeat.Once();
            BandCatalog.Replay();

            Repository.GetAdapterSettings(adapterName);
        }

        [TestMethod]
        public void When_AddPhotoAdapterSettings_is_called_with_a_PhotoAdapterSettings_then_the_PhotoAdapterSettings_with_is_added_to_the_collection()
        {
            const string adapterName = "AdapterName";
            var adapterSettings = AdapterSettingsCreator.CreateSingle(adapterName);

            BandCatalog
                .Expect(catalog => catalog.Add(adapterSettings))
                .Return(adapterSettings)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.AddAdapterSettings(adapterSettings);

            Assert.IsNotNull(result);
            Assert.AreEqual(adapterSettings, result);

            BandCatalog.VerifyAllExpectations();
        }

        [TestMethod]
        public void When_UpdatePhotoAdapterSettings_is_called_with_a_PhotoAdapterSettings_then_the_PhotoAdapterSettings_with_is_updated_in_the_collection()
        {
            const string adapterName = "AdapterName";
            var adapterSettings = AdapterSettingsCreator.CreateSingle(adapterName);

            BandCatalog
                .Expect(catalog => catalog.Update(adapterSettings))
                .Return(adapterSettings)
                .Repeat.Once();
            BandCatalog.Replay();

            var result = Repository.UpdateAdapterSettings(adapterSettings);

            Assert.IsNotNull(result);
            Assert.AreEqual(adapterSettings, result);

            BandCatalog.VerifyAllExpectations();
        }
    }
}