﻿using System;
using System.Collections.Generic;

namespace MauiFilmLibrary.Models;

public partial class Genre
{
    public int GenreId { get; set; }

    public string GenreName { get; set; } = null!;

    public virtual ICollection<Movie> Movies { get; set; } = new List<Movie>();
    public virtual ICollection<GenreMovie> GenreMovies { get; set; } = new List<GenreMovie>();
}
