using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Engine.Dbo
{
    public class Video
    {
        public enum Order
        {
            Id,
            ViewCountToday,
            ViewCountTotal
        }

        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime UploadDate { get; set; }
        public int ViewCount { get; set; }
        public Dbo.User User { get; set; }
        public List<Dbo.Encode> Encodes { get; set; }
        public List<Dbo.Comment> Comments { get; set; }

        public Video() {}

        public Video(string Title, string Description, Dbo.User User)
        {
            this.Description = Description;
            this.Title = Title;
            this.UploadDate = DateTime.Now;
            this.User = User;
            this.ViewCount = 0;
        }
    }
}