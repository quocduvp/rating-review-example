using rating_review_example.Repositories;
using rating_review_example.Repositories.Dto;
using rating_review_example.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rating_review_example.Controllers.Api
{
    public class ReviewController : ApiController
    {

        private readonly BaseRepository baseRepository = BaseRepository.getInstance();

        [HttpGet]
        [Route("api/v1/rating/score-values")]
        public IHttpActionResult Profile()
        {
            try
            {
                var results = baseRepository.ReviewRepository.GetScoreValues();
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [CustomAuthorize]
        [HttpPost]
        [Route("api/v1/rating/submit-review")]
        public IHttpActionResult SubmitReview(SubmitReviewDto body)
        {
            try
            {
                int currentPassCode = Util.getInstance().GetPassCode(Request);
                var passCode = baseRepository.AuthRepository.GetPassCode(currentPassCode);
                body.ServiceId = passCode.ServiceId;
                body.PassCodeId = passCode.Id;
                var results = baseRepository.ReviewRepository.SumitReview(body);
                return Ok(results);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
