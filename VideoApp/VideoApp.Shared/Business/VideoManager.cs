using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VideoApp.Business
{
    public class VideoManager
    {
        public static async Task<List<VideoAppModels.VideoFeeds>> GetAll()
        {
            using (Repository.VideoSearchRepository rep = new Repository.VideoSearchRepository())
            {
                var VideoList = await rep.GetAllVideos();
                return VideoList;
            }
        }
    }
}
