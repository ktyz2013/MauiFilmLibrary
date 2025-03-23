using System;
using System.Collections.Generic;

namespace MauiFilmLibrary.Models.Models;

public partial class MoviePersonRole
{
    public int MovieId { get; set; }

    public int PersonId { get; set; }

    public int RoleId { get; set; }

    public virtual Movie Movie { get; set; } = null!;

    public virtual Person Person { get; set; } = null!;

    public virtual Role Role { get; set; } = null!;
}
