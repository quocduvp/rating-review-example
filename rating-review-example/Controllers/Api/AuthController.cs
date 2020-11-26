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
    public class AuthController : ApiController
    {
        private BaseRepository baseRepository = BaseRepository.getInstance();

        [HttpPost]
        [Route("api/v1/auth/login")]
        public IHttpActionResult register(LoginDto body)
        {
            try
            {
                var result = this.baseRepository.AuthRepository.Login(body);
                return Ok(result);
            } catch (Exception ex)
            {
                
                return Content(HttpStatusCode.Forbidden, ex.Message);
            }
        }

        [CustomAuthorize]
        [HttpGet]
        [Route("api/v1/auth/profile")] 
        public IHttpActionResult Profile()
        {
            try
            {
                int currentPassCode = Util.getInstance().GetPassCode(Request);
                var passCode = baseRepository.AuthRepository.GetPassCode(currentPassCode);
                return Ok(new { 
                    Id = passCode.Id,
                    ServiceId = passCode.ServiceId,
                    LoginAt = passCode.LoginAt,
                });
            }
            catch (Exception ex)
            {
                return Content(HttpStatusCode.Forbidden, ex.Message);
            }
        }
    }
}
