using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace rating_review_example.Models
{
    public class PassCode
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string Token { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public DateTime LoginAt { get; set; }

        public virtual Service Service { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}