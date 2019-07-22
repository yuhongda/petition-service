using System;
namespace cms_webservice.Model
{
    public class Petition
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime CreateTime { get; set; }
        public string FromUserId { get; set; }
        public string ToUserId { get; set; }
        public string ToUserName { get; set; }
        public int Status { get; set; }
    }
}
