using rating_review_example.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rating_review_example.Repositories
{
    public class ServiceRepository
    {
        private DBContext context = new DBContext();

        public IQueryable<object> GetServices()
        {
            try
            {
                return context.Service.Select(s => new {
                    Id = s.Id,
                    Name = s.Name,
                    Icon = s.Icon,
                    Question = s.Question
                });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}