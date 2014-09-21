using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Data.Entity;

namespace VideoFeedServices.Controllers
{
    public class VideoController : ApiController
    {
        [HttpGet]
        public List<string> Test()
        {
            List<string> testlist = new List<string>();
            testlist.Add("this is a test");
            return testlist;
        }

        [HttpGet]
        public List<VideoFeedDAL.VideoFeeds> TestEF()
        {
            //DbContext is IDisposable
            using (VideoFeedDAL.VideoAppEntities con = new VideoFeedDAL.VideoAppEntities())
            {
                //if there isnt any data, add some dummy 
                if (con.VideoFeeds.Count() == 0)
                {
                    con.VideoFeeds.Add(new VideoFeedDAL.VideoFeeds()
                    {
                        VideoTitle = "Some EF Video From Youtube",
                        VideoUrl = "https://www.youtube.com/watch?v=Z7713GBhi4k&list=PL6n9fhu94yhUPBSX-E2aJCnCR3-_6zBZx"
                    });
                    con.SaveChanges();
                }
                var data = con.VideoFeeds.ToList();
                return data;
            }
        }

        [HttpGet]
        public List<VideoFeedDAL.VideoFeeds> TestEFWithParameter(string title)
        {
            //DbContext is IDisposable
            using (VideoFeedDAL.VideoAppEntities con = new VideoFeedDAL.VideoAppEntities())
            {
                //only data we have currently is the record with title ""Some EF Video From Youtube". lets search the titles with the parameter
                var data = con.VideoFeeds.Where(p => p.VideoTitle.Contains(title)).ToList();
                return data;
            }
        }

        [HttpGet]
        public List<VideoAppModels.VideoFeeds> GetAllVideos()
        {
            using (VideoFeedDAL.VideoAppEntities con = new VideoFeedDAL.VideoAppEntities())
            {
                return con.VideoFeeds.Select(p => new VideoAppModels.VideoFeeds() { Id = p.Id, VideoTitle = p.VideoTitle, VideoUrl = p.VideoUrl }).ToList();
            }
        }

        [HttpGet]
        public List<VideoAppModels.VideoFeeds> GetVideosByTitleSearch(string searchParameter)
        {
            using (VideoFeedDAL.VideoAppEntities con = new VideoFeedDAL.VideoAppEntities())
            {
                return con.VideoFeeds.Where(p => p.VideoTitle.ToLower().Contains(searchParameter.ToLower())).Select(p => new VideoAppModels.VideoFeeds() { Id = p.Id, VideoTitle = p.VideoTitle, VideoUrl = p.VideoUrl }).ToList();
            }
        }
    }
}
