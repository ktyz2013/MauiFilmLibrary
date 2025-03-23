using System;
using System.Collections.Generic;

namespace MauiFilmLibrary.Models;

public partial class GenreMovie
{
    public int GenerId { get; set; }

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;
}
