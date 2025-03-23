using System;
using System.Collections.Generic;

namespace MauiFilmLibrary.Models.Models;

public partial class Movie
{
    public int MovieId { get; set; }

    public string Title { get; set; } = null!;

    public string? Description { get; set; }

    public string? ReleaseYear { get; set; }

    public byte[]? TitleImg { get; set; }

    public virtual ICollection<GenerMovie> GenreMovies { get; set; } = new List<GenerMovie>();

    public virtual ICollection<MoviePersonRole> MoviePersonRoles { get; set; } = new List<MoviePersonRole>();


}
