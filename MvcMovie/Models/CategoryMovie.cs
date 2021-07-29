using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MvcMovie.Models
{
    public class CategoryMovie
    {
        public int CategoryId { get; set; }
        public Category category { get; set; }

        public int MovieId { get; set; }

        public Movie movie { get; set; }
    }
}
