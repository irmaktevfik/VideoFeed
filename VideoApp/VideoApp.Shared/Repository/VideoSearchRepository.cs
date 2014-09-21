using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace VideoApp.Repository
{
    public class VideoSearchRepository : BaseRepository, IDisposable
    {
        public async Task<List<VideoAppModels.VideoFeeds>> GetVideosByTitleSearch(string searchParameter)
        {
            UriString parameters = new UriString();
            parameters.Add("searchParameter", searchParameter);
            var obj = await base.GetAsync<List<VideoAppModels.VideoFeeds>>(parameters, "GetVideosByTitleSearch", "Video");
            return obj;
        }

        public async Task<List<VideoAppModels.VideoFeeds>> GetAllVideos()
        {
            var obj = await base.GetAsync<List<VideoAppModels.VideoFeeds>>("GetAllVideos", "Video");
            return obj;
        }

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
    }
}
