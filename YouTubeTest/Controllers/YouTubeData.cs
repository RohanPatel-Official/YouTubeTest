using Google.Apis.Services;
using Google.Apis.YouTube.v3;
using Google.Apis.YouTube.v3.Data;
using Microsoft.AspNetCore.Mvc;
using System.Text;

namespace YouTubeTest.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class YouTubeData : Controller
    {
        public IActionResult Index(string ytid)
        {
            YouTubeService youtubeService = new YouTubeService(new BaseClientService.Initializer()
            {
                ApiKey = "api_key",
                ApplicationName = "YoutubeTest"
            });
            // Define the video ID
            //https://www.youtube.com/watch?v=AopeJjkcRvU
            //string videoId = "AopeJjkcRvU";
            string videoId = ytid;
            // Prepare the request
            VideosResource.ListRequest listRequest = youtubeService.Videos.List("snippet,contentDetails,statistics,status");
            listRequest.Id = videoId;
            StringBuilder ytData = new StringBuilder();
            try
            {
                // Execute the request
                VideoListResponse response = listRequest.Execute();
                // Access the video information
                foreach (var item in response.Items)
                {
                    ytData.Append("<b>Title: </b>" + item.Snippet.Title + "<br/>");
                    ytData.Append("<b>View Count: </b>" + item.Statistics.ViewCount + "<br/>");
                    ytData.Append("<b>Like Count: </b>" + item.Statistics.LikeCount + "<br/>");
                    ytData.Append("<b>Dislike Count: </b>" + item.Statistics.DislikeCount + "<br/>");
                    ytData.Append("<b>Comment Count: </b>" + item.Statistics.CommentCount + "<br/>");
                    ytData.Append("<b>Favorite Count: </b>" + item.Statistics.FavoriteCount + "<br/>");
                    ytData.Append("<b>Description: </b>" + item.Snippet.Description + "<br/>");
                }
            }
            catch (Exception e)
            {
                // Log the error
                Console.WriteLine("An error occurred: " + e.Message);
            }
            //Console.ReadLine();
            return base.Content(ytData.ToString(), "text/html");
        }
    }
}
