using System;
using System.Collections.Generic;

namespace MauiFilmLibrary.Models.Models;

public partial class Genre
{
    public int GenerId { get; set; }

    public string GenreName { get; set; } = null!;
    public virtual ICollection<GenerMovie> GenreMovies { get; set; } = new List<GenerMovie>();
}
