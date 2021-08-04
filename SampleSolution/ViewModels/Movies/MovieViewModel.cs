using SampleSolution.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace SampleSolution.ViewModels.Movies
{
    public class MovieViewModel
    {
        public MovieViewModel()
        {

        }
        public MovieViewModel(Movie movie)
        {
            Movie = movie;
            Title = movie.Title;
            Id = movie.Id;
            Price = movie.Price;
            ReleaseDate = movie.ReleaseDate;
            Genre = movie.Genre;

        }

        public Movie Movie { get; set; }

        public int Id { get; set; }

        public string Title { get; set; }

        [Display(Name = "Release Date")]
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }
        public string Genre { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        public decimal Price { get; set; }
        
    }
}
