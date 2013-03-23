using System.Web.Security;
using Ewk.BandWebsite.Domain.BandModel;

namespace Ewk.BandWebsite.Web.Common.ModelMappers
{
    public interface IUserMapper
    {
        MembershipUser Map(User user);
        User Map(MembershipUser user);
    }
}
