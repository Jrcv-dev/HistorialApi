using SaveHistorialYT.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SaveHistorialYT.Controllers
{
    public class HistorialController : ApiController
    {
        YouTubeEntities dbcontext = new YouTubeEntities();

        [HttpGet]
        public List<HistoryVideos> PlayedVideos(int amount = 10)
        {
            var LastVideosWatched = dbcontext.History
                .Where(x=> x.IsEnabled!=false)
                .OrderByDescending(p => p.Id)
                .Take(amount)
                .Select(h => new HistoryVideos
                {
                    Id = h.Id,
                    VideoId = h.VideoId,
                    IsEnabled = h.IsEnabled,
                    Date = h.Date
                });
            return LastVideosWatched.ToList();
        }
        [HttpPost]
        public void Video(string videoId)
        {
            History video = new History();
            video.VideoId = videoId;
            video.IsEnabled = true;
            video.Date = DateTime.Now;
            dbcontext.History.Add(video);
            dbcontext.SaveChanges();
            //var videoGuardado = dbcontext.History.Where(x => x.VideoId == videoId).Select(x => new HistoryVideos
            //{
            //    Id = x.Id,
            //    VideoId = x.VideoId,
            //    IsEnabled = x.IsEnabled,
            //    Date = x.Date
            //}).ToList();
            //var v = videoGuardado[0];
            //return v;
        }
        [HttpDelete]
        public void DeleteFromHistory(int id)
        {
            var itemRemove = dbcontext.History.FirstOrDefault(h => h.Id == id);
            if(itemRemove != null)
            {
                itemRemove.IsEnabled = false;
                dbcontext.SaveChanges();
            }
        }
    }
}
