using System;
using System.Collections.Generic;

namespace MauiFilmLibrary.Models;

public partial class GenreMovie
{
    public int GenreId { get; set; }

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;
    public virtual Genre Genre { get; set; } = null!;

}
