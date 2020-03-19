using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class SliderImage : BaseEntity
    {
        public string ImageName { get; set; }
        [MaxLength(100, ErrorMessage ="Description must be less then 100")]
        public string Description { get; set; }
        public bool IsActive { get; set; }
    }
}
