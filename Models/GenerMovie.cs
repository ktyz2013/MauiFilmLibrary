using System;
using System.Collections.Generic;

namespace MauiFilmLibrary.Models.Models;

public partial class GenerMovie
{
    public int GenerId { get; set; }

    public int MovieId { get; set; }

    public virtual Movie Movie { get; set; } = null!;
    public virtual Genre Gener { get; set; } = null!;

}
