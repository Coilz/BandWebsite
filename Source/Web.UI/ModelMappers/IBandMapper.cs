using System;
using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Web.UI.Models.Home;

namespace Ewk.BandWebsite.Web.UI.ModelMappers
{
    public interface IBandMapper
    {
        AboutModel MapToAboutModel(Band entity);

        Band Map(AboutUpdateModel model);
        AboutUpdateModel MapToAboutUpdateModel(Band entity);
    }
}