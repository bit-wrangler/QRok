using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QRok.Models
{
    public class SurveyOption
    {
        public int SurveyId { get; set; }
        public Survey Survey { get; set; }

        public int OptionNumber { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }

        public int Count { get; set; }

    }
}
