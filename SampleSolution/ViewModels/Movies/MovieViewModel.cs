using SampleSolution.Models;
using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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
            Rating = movie.Rating;
        }

        public Movie Movie { get; set; }

        public int Id { get; set; }

        [StringLength(60, MinimumLength = 3)]
        [Required]
        public string Title { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z0-9""'\s-]*$"), StringLength(5), Required]
        public string Rating { get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true), DataType(DataType.Date), Display(Name = "Release Date")]
        public DateTime ReleaseDate { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$"), Required, StringLength(30)]
        public string Genre { get; set; }

        [Column(TypeName = "decimal(18, 2)")]
        [Range(1, 100), DataType(DataType.Currency)]
        public decimal Price { get; set; }
        
    }
}
