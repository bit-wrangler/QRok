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
using AutoMapper;
using QRok.Controllers.Resources;

namespace QRok.Controllers
{
    public class SurveysController : Controller
    {

        private QRokContext _context;
        private readonly IMapper mapper;

        public SurveysController(QRokContext dbContext, IMapper mapper)
            : base()
        {
            _context = dbContext;
            this.mapper = mapper;
        }

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
            return View(mapper.Map<Survey,SurveyResource>(survey));
        }

        [HttpPost]
        public IActionResult Create(string surveyJson)
        {
            var surveys = _context.Surveys;
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
            _context.SaveChanges();

            Response.Cookies.Append(
                string.Format("guid:{0}",newSurvey.Id.ToString()), 
                newSurvey.Guid.ToString(),
                new Microsoft.AspNetCore.Http.CookieOptions { Expires = newSurvey.DeleteDateTime});

            return RedirectToAction(
                "Details",
                new {id = newSurvey.Id}
            );

        }

        public IActionResult Details(int id, string guid)
        {
            var survey = _context.Surveys.Include(s => s.SurveyOptions).Single(s => s.Id == id);
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

            survey.SurveyOptions = survey.SurveyOptions.OrderBy(so => so.OptionNumber).ToList();

            if (valid)
                return View(mapper.Map<Survey,SurveyResource>(survey));
            else
                return BadRequest();
        }

        public IActionResult Vote(SurveyOptionResource surveyOption)
        {
            var survey = _context.Surveys.Include(s => s.SurveyOptions).Single(s => s.Id == surveyOption.SurveyId);
            bool valid = false;
            bool voted = false;

            if (Request.Cookies.ContainsKey(string.Format("guid:{0}", surveyOption.SurveyId.ToString())))
                valid = Request.Cookies[string.Format("guid:{0}", surveyOption.SurveyId.ToString())].Equals(survey.Guid.ToString());

            if (Request.Cookies.ContainsKey(string.Format("voted:{0}", surveyOption.SurveyId.ToString())))
                voted = Request.Cookies[string.Format("voted:{0}", surveyOption.SurveyId.ToString())].Equals("yes");

            if (valid && !voted)
            {
                Response.Cookies.Append(
                    string.Format("voted:{0}", surveyOption.SurveyId.ToString()),
                    "yes",
                    new Microsoft.AspNetCore.Http.CookieOptions { Expires = survey.DeleteDateTime });
                _context
                    .SurveyOptions
                    .Single(so => 
                            so.SurveyId == surveyOption.SurveyId 
                            && so.OptionNumber == surveyOption.OptionNumber)
                    .Count++;
                _context.SaveChanges();
            }
            return RedirectToAction("Details", new { id = survey.Id });

        }

    }
}