using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Xml;
using System.Threading.Tasks;
using Yt;

namespace YTDA
{
    // AIzaSyC979dpVpUH7DNQ8E9T5rpMJ72vaARKtzs 
    /// <summary>
    /// よくできています
    /// [maenishi]
    /// </summary>
    public class YouTubeDataApi
    {
        private string key;
        public YouTubeDataApi(string key){
            this.key = key;
        }

        public List<YouTube> Search(string query) {
            List<YouTube> videoList = new List<YouTube>();
            
            String url = "https://www.googleapis.com/youtube/v3/search?q="+ query +"&part=id,snippet&maxResults=10&key=AIzaSyC979dpVpUH7DNQ8E9T5rpMJ72vaARKtzs&order=relevance";
            WebRequest request = WebRequest.Create(url);
            Stream response_stream = request.GetResponse().GetResponseStream();
            StreamReader reader = new StreamReader(response_stream);
            XmlDocument resultXml = (XmlDocument)JsonConvert.DeserializeXmlNode(reader.ReadToEnd(), "root");
            XmlNodeList wordList = resultXml.GetElementsByTagName("items");
            foreach (XmlNode wordNode in wordList)
            {
                YouTube video = new YouTube("", "", "", "", DateTime.Parse("12:15:12"));
                foreach (XmlNode ChildwordNode in wordNode.ChildNodes)
                {
                    if (ChildwordNode.Name == "id")
                    {
                        video.VideoId = ChildwordNode.ChildNodes[1].InnerText;
                    }
                    else if (ChildwordNode.Name == "snippet")
                    {
                        video.PublishedAt = DateTime.Parse(ChildwordNode.ChildNodes[0].InnerText);
                        video.ThumbnailUrl = ChildwordNode.ChildNodes[4].ChildNodes[0].InnerText;
                        video.Title = ChildwordNode.ChildNodes[2].InnerText;
                        video.Description = ChildwordNode.ChildNodes[3].InnerText;
                    }
                }
                videoList.Add(video);
            }
            return videoList;
        }
    }
}
