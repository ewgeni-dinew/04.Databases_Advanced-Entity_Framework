using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Stations.Models
{
    public class SeatingClass
    {
        public int Id { get; set; }
        public string Name { get; set; }

        [Required]
        [StringLength(2,MinimumLength =2)]
        public string Abbreviation { get; set; }
    }
}
