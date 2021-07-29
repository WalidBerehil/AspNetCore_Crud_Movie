using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MvcMovie.Models
{
    public class Movie
    {
        public int Id { get; set; }

        public string Image { get; set; }

        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


        public int Price { get; set; }

        [Required]
        public int ActorId { get; set; }
        [ForeignKey("ActorId")]
        public Actor actor { get; set; }

        [NotMapped]
        [Required]
        public List<int> CategoryIdHelper { get; set; }
        public ICollection<CategoryMovie> categories { get; set; }


    }
}
