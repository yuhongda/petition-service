using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cms_webservice.Model
{
    public class Article
    {
        public string Title { get; set; }
        public DateTime AddTime { get; set; }
        public DateTime UpdateTime { get; set; }
        public string Content { get; set; }
        public string ShortContent { get; set; }
        public DateTime ExpireTime { get; set; }
        public string LongTitle { get; set; }
    }
}
