using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VideoAppModels
{
    public partial class VideoFeeds
    {
        public int Id { get; set; }
        public string VideoTitle { get; set; }
        public string VideoUrl { get; set; }
    }
}
