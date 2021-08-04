using Microsoft.AspNetCore.Mvc.Rendering;
using SampleSolution.Models;
using System.Collections.Generic;

namespace SampleSolution.ViewModels.Movies
{ 
    public class MovieGenreViewModel
    {
        public List<Movie> Movies { get; set; }
        public SelectList Genres { get; set; }
        public string MovieGenre { get; set; }
        public string SearchString { get; set; }
    }
}
