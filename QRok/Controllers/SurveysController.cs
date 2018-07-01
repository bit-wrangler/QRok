using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using QRok.Models;
using QRok.ViewModels;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using Microsoft.AspNetCore.Routing;

namespace QRok.Controllers
{
    public class SurveysController : Controller
    {
        public IActionResult Index(string surveyJson)
        {
            var survey = (surveyJson==null) ?
                new Survey
                {
                    Title = "Which is gooder?",
                    SurveyOptions = new List<SurveyOption>
                {
                    new SurveyOption{Title="Pancakes"},
                    new SurveyOption{Title="French TOast"},
                }
                }
                    :
                JsonConvert.DeserializeObject<Survey>(surveyJson);
            return View(survey);
        }

        [HttpPost]
        public IActionResult Create(string data)
        {
            return RedirectToAction(
                "Index",
                new {surveyJson = JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Survey>(data)) }
            );

            //return Content(
            //    JsonConvert.SerializeObject(JsonConvert.DeserializeObject<Survey>(data).SurveyOptions)
            //    );
        }

    }
}