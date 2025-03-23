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

    public virtual ICollection<Genre> Geners { get; set; } = new List<Genre>();

    public string GenresText => Geners != null && Geners.Any()
        ? string.Join(", ", Geners.Select(g => g.GenreName))
        : "Неизвестный жанр";

    public string ActorsText => MoviePersonRoles != null && MoviePersonRoles.Any()
        ? string.Join(", ", MoviePersonRoles.Select(mpr => mpr.Person.PersonName))
        : "Неизвестные актёры";
}
