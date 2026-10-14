using System;
using System.Collections.Generic;
using System.Text;

namespace Domains.Models
{
    public class TbSettings
    {
        public int Id { get; set; }
        public string WebSiteName { get; set; }
        public string Logo { get; set; }
        public string WebSiteDescription { get; set; }
        public string FacebookLink { get; set; }
        public string TwitterLink { get; set; }
        public string InstagramLink { get; set; }
        public string YouTubeLink { get; set; }
        public string MiddlePanner { get; set; }
    }
}
