using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace rating_review_example.Repositories
{
    public class BaseRepository
    {
        public readonly AuthRepository AuthRepository;
        public readonly ReviewRepository ReviewRepository;
        public readonly ServiceRepository ServiceRepository;

        public static BaseRepository instance;

        public BaseRepository ()
        {
            AuthRepository = new AuthRepository();
            ReviewRepository = new ReviewRepository();
            ServiceRepository = new ServiceRepository();
        }

        public static BaseRepository getInstance()
        {
            if(instance == null)
            {
                instance = new BaseRepository();
            }
            return instance;
        }

    }
}