using rating_review_example.Repositories;
using rating_review_example.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http.Controllers;

namespace rating_review_example
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CustomAuthorizeAttribute: System.Web.Http.AuthorizeAttribute
    {

        protected override bool IsAuthorized(HttpActionContext actionContext)
        {
            try
            {
                var token = actionContext.Request.Headers.Authorization;
                if (token == null)
                {
                    return false;
                }
                var passCodeId = Util.getInstance().JWT.Verify(token.ToString());
                var passCode = BaseRepository.getInstance().AuthRepository.GetPassCode(passCodeId);
                if (passCode != null)
                {
                    actionContext.Request.Properties["PassCodeId"] = passCode.Id;
                    actionContext.Request.Properties["ServiceId"] = passCode.ServiceId;
                    actionContext.Request.Properties["LoginAt"] = passCode.LoginAt;
                    return true;
                }
                else
                {
                    return false;
                }
            } catch (Exception _ex)
            {
                return false;
            }
            
        }
    }
}