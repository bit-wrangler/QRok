using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace QRok.Models
{
    public class Survey
    {
        public int Id { get; set; }

        public Guid Guid { get; set; }

        [Required]
        [MaxLength(255)]
        public string Title { get; set; }
    }
}
