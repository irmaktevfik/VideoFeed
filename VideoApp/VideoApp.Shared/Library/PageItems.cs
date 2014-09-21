using System;
using System.Collections.Generic;
using System.Text;

namespace VideoApp.Library
{
    public class PageItems
    {
        public string Title { get; set; }
        public object Items { get; set; }

        public static List<PageItems> GetHomeData(List<VideoAppModels.VideoFeeds> obj)
        {
            List<PageItems> menu = new List<PageItems>();
            menu.Add(new PageItems()
            {
                Title = "Videos",
                Items = obj
            }
            );
            return menu;
        }
    }
}
