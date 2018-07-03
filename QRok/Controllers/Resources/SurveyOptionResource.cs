using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRok.Controllers.Resources
{
    public class SurveyOptionResource
    {
        public int SurveyId { get; set; }

        public int OptionNumber { get; set; }

        public string Title { get; set; }

        public int Count { get; set; }

    }
}
