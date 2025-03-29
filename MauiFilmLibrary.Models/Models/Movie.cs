using System;
using System.Collections.Generic;

namespace MauiFilmLibrary.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public DateOnly? ReleaseYear { get; set; }

    public byte[]? TitleImg { get; set; }

    public virtual ICollection<MoviePersonRole> MoviePersonRoles { get; set; } = new List<MoviePersonRole>();

    public virtual ICollection<GenreMovie> GenreMovies { get; set; } = new List<GenreMovie>();

}
