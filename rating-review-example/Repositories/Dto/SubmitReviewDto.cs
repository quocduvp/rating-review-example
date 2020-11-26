using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rating_review_example.Repositories.Dto
{
    public class SubmitReviewDto
    {
        public int PassCodeId { get; set; }

        public int ServiceId { get; set; }

        public int ScoreId { get; set; }

        public string Note { get; set; }
    }
}