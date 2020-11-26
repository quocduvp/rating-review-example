using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace rating_review_example.Models
{
    public class ScoreValue
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int Score { get; set; }

        public string Text { get; set; }

        [DataType(DataType.Text)]
        [Required]
        public string Icon { get; set; }

        public virtual ICollection<Review> Reviews { get; set; }
    }
}