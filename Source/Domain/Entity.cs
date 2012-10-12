using System;

namespace Ewk.BandWebsite.Domain
{
    /// <summary>
    /// The base class of all domain entities.
    /// </summary>
    public abstract class Entity
    {
        /// <summary>
        /// Initializes a new instance.
        /// </summary>
        protected Entity()
        {
            Id = Guid.NewGuid();
            CreationDate = DateTime.UtcNow;
        }

        /// <summary>
        /// A value that uniquely identifies the <see cref="Entity"/>.
        /// </summary>
        public Guid Id { get; set; }

        /// <summary>
        /// The <see cref="DateTime"/> this <see cref="Entity"/> was created.
        /// </summary>
        public DateTime CreationDate { get; set; }

        /// <summary>
        /// The <see cref="DateTime"/> this <see cref="Entity"/> was modified for the last time.
        /// </summary>
        public DateTime ModificationDate { get; set; }
    }
}