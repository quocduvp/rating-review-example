using rating_review_example.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace rating_review_example.Controllers.Api
{
    public class ServiceController : ApiController
    {

        private readonly BaseRepository baseRepository = BaseRepository.getInstance();

        [HttpGet]
        [Route("api/v1/services")]
        public IHttpActionResult GêtService()
        {
            try
            {
                var result = baseRepository.ServiceRepository.GetServices();
                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
