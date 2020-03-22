using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public  class Comment : BaseEntity
    {
        [MinLength(15)]
        [MaxLength(600)]
        public string Text { get; set; }
        public string UserId { get; set; }
        public int Rating { get; set; }
        public DateTime TimeStamp { get; set; }
        public int EntityID { get; set; }
        public int RecordID { get; set; }
        public virtual List<DealDoubleUser> Users { get; set; }


    }
}
