using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<CategoryMovie> movies { get; set; }


    }
}
