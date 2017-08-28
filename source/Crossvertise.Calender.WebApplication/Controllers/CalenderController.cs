using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web;
using System.Web.Mvc;
using Crossvertise.Calender.WebApplication.Models;
using Newtonsoft.Json;

namespace Crossvertise.Calender.WebApplication.Controllers
{
    public class CalenderController : Controller
    {
        private HttpClient _apiConsumerClient;

        public CalenderController()
        {
            _apiConsumerClient = new HttpClient();
            _apiConsumerClient.BaseAddress = new Uri(ConfigurationManager.AppSettings["ApiBaseAddress"]);
            _apiConsumerClient.DefaultRequestHeaders.Accept.Clear();
            _apiConsumerClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        //
        // GET: /Calender/
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult GetEventList(int monthId)
        {
            MonthEventListViewModel viewModel = new MonthEventListViewModel();
            viewModel.MonthId = monthId;

            var response = _apiConsumerClient.GetAsync("api/events/GetMonthEvents?monthId=" + monthId).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                viewModel.Events = JsonConvert.DeserializeObjectAsync<EventViewModel[]>(responseString).Result;
            }

            return View(viewModel);
        }


        // i found my self out of time 
        // so I completed using razor view engine
        // and to accomplish the sent UI I did a simple hack
        // by sending 2 requests to API
        // one to get event details
        // and the other for getting month events
        public ActionResult GetEventDetails(int eventId)
        {
            EventDetailsViewModel viewModel = new EventDetailsViewModel();

            var response = _apiConsumerClient.GetAsync("api/events/GetEventDetails?eventId=" + eventId).Result;
            if (response.IsSuccessStatusCode)
            {
                string responseString = response.Content.ReadAsStringAsync().Result;
                viewModel = JsonConvert.DeserializeObjectAsync<EventDetailsViewModel>(responseString).Result;
                if (viewModel != null)
                {
                    viewModel.MonthEventList = new MonthEventListViewModel();
                    viewModel.MonthEventList.MonthId = viewModel.EventDateTime.Month;
                    var secondResponse = _apiConsumerClient
                        .GetAsync("api/events/GetMonthEvents?monthId=" + viewModel.MonthEventList.MonthId).Result;
                    if (secondResponse.IsSuccessStatusCode)
                    {
                        responseString = secondResponse.Content.ReadAsStringAsync().Result;
                        viewModel.MonthEventList.Events = JsonConvert.DeserializeObjectAsync<EventViewModel[]>(responseString).Result;
                    }
                }
            }
            return View(viewModel);
        }
	}
}