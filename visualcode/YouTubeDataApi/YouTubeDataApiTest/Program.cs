using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Yt;
using YTDA;

namespace YouTubeDataApiTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string key = "AIzaSyC979dpVpUH7DNQ8E9T5rpMJ72vaARKtzs";
            string query = "京都 観光";
            int count = 0;
            YouTubeDataApi youTube = new YouTubeDataApi(key);
            List<YouTube> result = youTube.Search(query);
            Console.WriteLine("検索結果");
            foreach(YouTube resultVideo in result){
                count++;
                Console.WriteLine("");
                Console.WriteLine(count + ":");
                Console.WriteLine(resultVideo.VideoId);
                Console.WriteLine(resultVideo.Title);
                Console.WriteLine(resultVideo.Description);
                Console.WriteLine(resultVideo.ThumbnailUrl);
                Console.WriteLine(resultVideo.PublishedAt);
            }
                Console.ReadKey();
        }
    }
}
