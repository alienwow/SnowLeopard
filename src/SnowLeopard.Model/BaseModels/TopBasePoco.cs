using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SnowLeopard.Model.BaseModels
{
    /// <summary>
    /// TopBasePoco
    /// </summary>
    public abstract class TopBasePoco
    {
        /// <summary>
        /// Id
        /// </summary>
        [Display(Name = "Id")]
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
    }

    /// <summary>
    /// TopBasePocoGuid
    /// </summary>
    public abstract class TopBasePocoGuid
    {
        private Guid? _id;

        /// <summary>
        /// Id
        /// </summary>
        [Display(Name = "Id")]
        [Key]
        public Guid Id
        {
            get
            {
                if (!_id.HasValue || _id.Value == Guid.Empty)
                {
                    _id = Guid.NewGuid();
                }
                return _id.Value;
            }
            set
            {
                _id = value;
            }
        }
    }
}
