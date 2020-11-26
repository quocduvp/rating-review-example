using rating_review_example.Context;
using rating_review_example.Models;
using rating_review_example.Repositories.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rating_review_example.Repositories
{
    public class ReviewRepository
    {
        private DBContext context = new DBContext();
        public IQueryable<object> GetScoreValues ()
        {
            try
            {
                return context.ScoreValue.Select(s => new { 
                    Id= s.Id,
                    Text = s.Text,
                    Score = s.Score,
                    Icon = s.Icon
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public Review SumitReview(SubmitReviewDto data)
        {
            try
            {
                var review = context.Review.Add(new Review
                {
                    ScoreId = data.ScoreId,
                    PassCodeId = data.PassCodeId,
                    ServiceId = data.ServiceId,
                    Note = data.Note != null ? data.Note : "",
                    createdAt = DateTime.Now
                });
                context.SaveChanges();
                return review;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}