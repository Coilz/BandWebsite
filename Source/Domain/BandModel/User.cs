using Ewk.BandWebsite.Domain.AppModel;

namespace Ewk.BandWebsite.Domain.BandModel
{
    public class User : Person
    {
        public LoginAccount Login { get; set; }
    }
}