using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Yt
{
    public class YouTube
    {
        private string videoId;
        private string title;
        private string description;
        private string thumbnailUrl;
        private DateTime publishedAt;

        public YouTube(string videoId, string title, string description, string thumbnailUrl, DateTime publishedAt)
        {
            this.videoId = videoId;
            this.title = title;
            this.description = description;
            this.thumbnailUrl = thumbnailUrl;
            this.publishedAt = publishedAt;
        }

        // string videoId プロパティ
        public string VideoId
        {
            set { this.videoId = value; }
            get { return this.videoId; }
        }
        // string title プロパティ
        public string Title
        {
            set { this.title = value; }
            get { return this.title; }
        }
        // string description プロパティ
        public string Description
        {
            set { this.description = value; }
            get { return this.description; }
        }
        // string thumbnailUrl プロパティ
        public string ThumbnailUrl
        {
            set { this.thumbnailUrl = value; }
            get { return this.thumbnailUrl; }
        }
        // DateTime publishedAt プロパティ
        public DateTime PublishedAt
        {
            set { this.publishedAt = value; }
            get { return this.publishedAt; }
        }
    }
}
