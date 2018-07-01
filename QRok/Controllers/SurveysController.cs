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
using Microsoft.EntityFrameworkCore;

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
        public IActionResult Create(string surveyJson, [FromServices] QRokContext qRokContext)
        {
            var surveys = qRokContext.Surveys;
            var survey = JsonConvert.DeserializeObject<Survey>(surveyJson);

            var options = new List<SurveyOption>();
            foreach (var option in survey.SurveyOptions) options.Add(new SurveyOption
            {
                Count = 0,
                OptionNumber = option.OptionNumber,
                Title = option.Title
            });


            var newSurvey = new Survey
            {
                Title = survey.Title,
                Guid = Guid.NewGuid(),
                CloseDateTime = DateTime.Now.AddMinutes(5),
                DeleteDateTime = DateTime.Now.AddHours(1),
                SurveyOptions = options
            };
            surveys.Add(newSurvey);
            qRokContext.SaveChanges();

            Response.Cookies.Append(
                string.Format("guid:{0}",newSurvey.Id.ToString()), 
                newSurvey.Guid.ToString(),
                new Microsoft.AspNetCore.Http.CookieOptions { Expires = newSurvey.DeleteDateTime});

            return RedirectToAction(
                "Details",
                new {id = newSurvey.Id}
            );

        }

        public IActionResult Details(int id, string guid, [FromServices] QRokContext qRokContext)
        {
            var survey = qRokContext.Surveys.Include(s => s.SurveyOptions).Single(s => s.Id == id);
            bool valid = false;

            if (Request.Cookies.ContainsKey(string.Format("guid:{0}", id.ToString())))
                valid = Request.Cookies[string.Format("guid:{0}", id.ToString())].Equals(survey.Guid.ToString());

            if (guid != null && !valid)
            {
                valid = guid.Equals(survey.Guid.ToString());
                if (valid)
                    Response.Cookies.Append(
                        string.Format("guid:{0}", id.ToString()),
                        guid,
                        new Microsoft.AspNetCore.Http.CookieOptions { Expires = survey.DeleteDateTime });
            }

            if (valid)
                return View(survey);
            else
                return BadRequest();
        }

    }
}