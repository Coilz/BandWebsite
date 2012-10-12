using System.Web.Security;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Web.UI.ModelMappers
{
    public interface IUserMapper
    {
        MembershipUser Map(User user);
        User Map(MembershipUser user);
    }
}
