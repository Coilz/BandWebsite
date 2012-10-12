using System;

namespace Ewk.BandWebsite.Domain.AppModel
{
    /// <summary>
    /// A respresentation of a band.
    /// </summary>
    public class Band : Entity
    {
        /// <summary>
        /// The public identifier of the band.
        /// </summary>
        public string PublicId { get; set; }

        /// <summary>
        /// The name of the band.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// The date the band was founded.
        /// </summary>
        public DateTime Founded { get; set; }

        /// <summary>
        /// A description of the band.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// The passphrase from which a pseudo-random password will be derived for cryptography.
        /// </summary>
        public string Passphrase { get; set; }

        /// <summary>
        /// The initvector is required to encrypt the first block of plaintext data.
        /// </summary>
        public string InitVector { get; set; }

        /// <summary>
        /// Salt value used for password hashing during key generation.
        /// </summary>
        public string SaltValue { get; set; }
    }
}