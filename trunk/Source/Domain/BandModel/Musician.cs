using Ewk.BandWebsite.Domain.AppModel;

namespace Ewk.BandWebsite.Domain.BandModel
{
    public class Musician : Person
    {
        public string Instrument { get; set; }

        public ContractType Relation { get; set; }

        public enum ContractType
        {
            Member,
            Hired,
            Guest
        }
    }
}