using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace rating_review_example.Models
{
    public class Review
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int PassCodeId { get; set; }

        [Required]
        public int ServiceId { get; set; }

        [Required]
        public int ScoreId { get; set; }

        [DataType(DataType.Text)]
        public string Note { get; set; }

        public DateTime createdAt { get; set; }

        public virtual PassCode PassCode { get; set; }

        public virtual Service Service { get; set; }

        public virtual ScoreValue Score { get; set; }

    }
}