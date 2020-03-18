using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DealDouble.Entities
{
    public class ParentCategory: BaseEntity
    {
        [Required(ErrorMessage ="Please Enter Category Name")]
        [MaxLength(30,ErrorMessage ="Maximum length is 30 Characters")]
        public string ParentCategoryName { get; set; }
        [MinLength(10, ErrorMessage = "Minimum length is 10 Characters")]
        [MaxLength(150, ErrorMessage = "Maximum length is 150 Characters")]
        public string Description { get; set; }
        public virtual List<Category> Categories { get; set; }
    }
}
