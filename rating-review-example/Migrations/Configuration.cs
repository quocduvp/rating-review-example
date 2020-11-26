namespace rating_review_example.Migrations
{
    using rating_review_example.Models;
    using rating_review_example.Utils;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<rating_review_example.Context.DBContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(rating_review_example.Context.DBContext context)
        {
            context.Service.AddRange(InitServices());
            context.ScoreValue.AddRange(InitScoreValues());
            context.PassCode.AddRange(InitPassCodeAccount());
            //  You can use the DbSet<T>.AddOrUpdate() helper extension method
            base.Seed(context);
            //  to avoid creating duplicate seed data.
        }

        private static IList<Service> InitServices()
        {
            using (var context = new rating_review_example.Context.DBContext())
            {
                IList<Service> defaultStandards = new List<Service>();
                var services = from s in context.Service select s;
                if (services.Count() > 1)
                {
                    return defaultStandards;
                }

                defaultStandards.Add(new Service()
                {
                    Id = 1,
                    Icon = "/Assets/login/bucket.png",
                    Question = "Bạn thấy lớp học, văn phòng ALT IELTS Gia Sư có <b class='text-danger'>Sạch Sẽ</b> không ?",
                    Name = "Vệ sinh"
                });
                defaultStandards.Add(new Service()
                {
                    Id = 2,
                    Icon = "/Assets/login/policeman.png",
                    Question = "Bạn có hài lòng với dịch vụ <b class='text-danger'>Bảo Vệ</b> của ALT IELTS Gia Sư không ?",
                    Name = "Bảo Vệ"
                });
                defaultStandards.Add(new Service()
                {
                    Id = 3,
                    Icon = "/Assets/login/social-care.png",
                    Question = "Bạn có hài lòng với dịch vụ <b class='text-danger'>Chăm Sóc Học Viên</b> của ALT IELTS Gia Sư không ?",
                    Name = "Chăm sóc học viên"
                });
                return defaultStandards;
            }

        }

        private static IList<ScoreValue> InitScoreValues()
        {
            using (var context = new rating_review_example.Context.DBContext())
            {
                IList<ScoreValue> defaultStandards = new List<ScoreValue>();
                var scores = from s in context.ScoreValue select s;
                if (scores.Count() > 1)
                {
                    return defaultStandards;
                }
                defaultStandards.Add(new ScoreValue()
                {
                    Id = 1,
                    Icon = "/Assets/emoji/crying.png",
                    Score = 1,
                    Text = "Rất không sạch sẽ"
                });
                defaultStandards.Add(new ScoreValue()
                {
                    Id = 2,
                    Icon = "/Assets/emoji/unhappy.png",
                    Score = 2,
                    Text = "Tạm được"
                });
                defaultStandards.Add(new ScoreValue()
                {
                    Id = 3,
                    Icon = "/Assets/emoji/happy.png",
                    Score = 3,
                    Text = "Chấp nhận được"
                });
                defaultStandards.Add(new ScoreValue()
                {
                    Id = 4,
                    Icon = "/Assets/emoji/smile.png",
                    Score = 4,
                    Text = "Hơi sạch sẽ"
                });
                defaultStandards.Add(new ScoreValue()
                {
                    Id = 5,
                    Icon = "/Assets/emoji/in-love.png",
                    Score = 5,
                    Text = "Rất sạch sẽ"
                });
                return defaultStandards;
            }
        }

        private static IList<PassCode> InitPassCodeAccount()
        {
            using (var context = new rating_review_example.Context.DBContext())
            {
                IList<PassCode> defaultStandards = new List<PassCode>();
                var passCode = from s in context.PassCode select s;
                if (passCode.Count() > 1)
                {
                    return defaultStandards;
                }
                defaultStandards.Add(new PassCode()
                {
                    ServiceId = 1,
                    Token = Util.getInstance().HashPass.GenerateHash("123"),
                    LoginAt = DateTime.Now
                });
                defaultStandards.Add(new PassCode()
                {
                    ServiceId = 2,
                    Token = Util.getInstance().HashPass.GenerateHash("test321"),
                    LoginAt = DateTime.Now
                });
                defaultStandards.Add(new PassCode()
                {
                    ServiceId = 3,
                    Token = Util.getInstance().HashPass.GenerateHash("test456"),
                    LoginAt = DateTime.Now
                });
                return defaultStandards;
            }
        }
    }
}
