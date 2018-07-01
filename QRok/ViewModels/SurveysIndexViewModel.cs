using QRok.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QRok.ViewModels
{
    public class SurveysIndexViewModel
    {
        public Survey Survey { get; set; }

        [Required]
        public List<string> Options { get; set; }
    }
}
