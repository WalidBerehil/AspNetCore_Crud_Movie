using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Actor
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }

        [Display(Name = "Creation Date")]
        [DataType(DataType.Date)]
        public DateTime CreationDate { get; set; }

    }
}
