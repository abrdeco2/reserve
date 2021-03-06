﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net.Http;
using System.Threading.Tasks;
using System.Net;
using Newtonsoft.Json;

namespace WebApplication5.Controllers
{
    public class COVID19Controller : Controller
    {
        private static HttpClient httpClient;

        

        // GET: COVID19
        public ActionResult Index()
        {
            var json = "";

            using (WebClient wc = new WebClient())
            {
                json = wc.DownloadString("https://api.covid19api.com/summary").Replace("\\", "");
            }

            var model = Newtonsoft.Json.JsonConvert.DeserializeObject<FullRequest>(json);

            var top10model = model.Countries.ToList();

            var top10 = top10model.OrderByDescending(o => (o.TotalConfirmed - o.TotalRecovered)).Take(10);

            //ViewBag.top10 = top10.ToList();


            //return Json(model, JsonRequestBehavior.AllowGet);


            return View(top10);
        }

        [HttpGet]
        public ActionResult GetJson()
        {
            
            var json = "";
            
            using(WebClient wc = new WebClient())
            {
                json = wc.DownloadString("https://api.covid19api.com/summary").Replace("\\","");
            }

                var model = Newtonsoft.Json.JsonConvert.DeserializeObject<FullRequest>(json);

            var top10model = model.Countries.ToList();
            
            var top10 = top10model.OrderByDescending(o => o.TotalConfirmed).Take(10);

            ViewBag.top10 = top10.ToList();
            

            return Json(model, JsonRequestBehavior.AllowGet);

            //return Redirect("Index");
            
            //return new HttpStatusCodeResult(HttpStatusCode.InternalServerError, "Erro ao acessar a API.");
        }

        


    }
    

    public partial class FullRequest
    {
        [JsonProperty("ID")]
        public Guid Id { get; set; }

        [JsonProperty("Message")]
        public string Message { get; set; }

        [JsonProperty("Global")]
        public Global Global { get; set; }

        [JsonProperty("Countries")]
        public Country[] Countries { get; set; }

        [JsonProperty("Date")]
        public DateTimeOffset Date { get; set; }
    }

    public partial class Country
    {
        [JsonProperty("ID")]
        public Guid Id { get; set; }

        [JsonProperty("Country")]
        public string CountryCountry { get; set; }

        [JsonProperty("CountryCode")]
        public string CountryCode { get; set; }

        [JsonProperty("Slug")]
        public string Slug { get; set; }

        [JsonProperty("NewConfirmed")]
        public long NewConfirmed { get; set; }

        [JsonProperty("TotalConfirmed")]
        public long TotalConfirmed { get; set; }

        [JsonProperty("NewDeaths")]
        public long NewDeaths { get; set; }

        [JsonProperty("TotalDeaths")]
        public long TotalDeaths { get; set; }

        [JsonProperty("NewRecovered")]
        public long NewRecovered { get; set; }

        [JsonProperty("TotalRecovered")]
        public long TotalRecovered { get; set; }

        [JsonProperty("Date")]
        public DateTimeOffset Date { get; set; }

        [JsonProperty("Premium")]
        public Premium Premium { get; set; }
    }

    public partial class Premium
    {
    }

    public partial class Global
    {
        [JsonProperty("NewConfirmed")]
        public long NewConfirmed { get; set; }

        [JsonProperty("TotalConfirmed")]
        public long TotalConfirmed { get; set; }

        [JsonProperty("NewDeaths")]
        public long NewDeaths { get; set; }

        [JsonProperty("TotalDeaths")]
        public long TotalDeaths { get; set; }

        [JsonProperty("NewRecovered")]
        public long NewRecovered { get; set; }

        [JsonProperty("TotalRecovered")]
        public long TotalRecovered { get; set; }

        [JsonProperty("Date")]
        public DateTimeOffset Date { get; set; }
    }

}