using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace rating_review_example.Models
{
    public class Service
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [MaxLength(50)]
        [Required]
        [Index(IsUnique = true)]
        public string Name { get; set; }

        [DataType(DataType.Text)]
        public string Question { get; set; }

        [DataType(DataType.Text)]
        public string Icon { get; set; }

        public virtual ICollection<PassCode> PassCodes { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}