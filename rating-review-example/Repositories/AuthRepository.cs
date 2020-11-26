using rating_review_example.Context;
using rating_review_example.Models;
using rating_review_example.Repositories.Dto;
using rating_review_example.Utils;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rating_review_example.Repositories
{
    public class AuthRepository
    {
        private readonly Util _Util = Util.getInstance();

        public object Login(LoginDto data)
        {
            using (var context = new DBContext())
            {
                try
                {
                    var hash = _Util.HashPass.GenerateHash(data.Password);
                    var passCode = (from p in context.PassCode where p.Token == hash && p.ServiceId == data.ServiceId select p).FirstOrDefault();
                    if (passCode == null)
                    {
                        throw new Exception("Mật khẩu không hợp lệ vui lòng nhập lại.");
                    }
                    var token = _Util.JWT.Encode(passCode, 1);
                    passCode.LoginAt = DateTime.Now;
                    context.SaveChanges();
                    return new { Token = token };
                } catch (Exception ex)
                {
                    throw ex;
                }

            }
        }

        public PassCode GetPassCode(int id)
        {
            using (var context = new DBContext())
            {
                try
                {
                    var passCode = (from p in context.PassCode where p.Id == id select p).FirstOrDefault();
                    if (passCode == null)
                    {
                        throw new Exception("Passcode không hợp lệ.");
                    }
                    return passCode;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}