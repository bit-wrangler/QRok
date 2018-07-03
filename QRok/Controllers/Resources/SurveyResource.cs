using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace QRok.Controllers.Resources
{
    public class SurveyResource
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        public string Title { get; set; }

        public IList<SurveyOptionResource> SurveyOptions { get; set; }

        public DateTime CloseDateTime { get; set; }
        public DateTime DeleteDateTime { get; set; }

        public SurveyResource()
        {
            SurveyOptions = new List<SurveyOptionResource>();
        }
    }
}
