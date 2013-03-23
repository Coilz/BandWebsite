using Ewk.BandWebsite.Domain.AppModel;
using Ewk.BandWebsite.Web.Common.Models.Home;

namespace Ewk.BandWebsite.Web.Common.ModelMappers
{
    public interface IBandMapper
    {
        AboutModel MapToAboutModel(Band entity);

        Band Map(AboutUpdateModel model);
        AboutUpdateModel MapToAboutUpdateModel(Band entity);
    }
}