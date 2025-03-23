using System;
using System.Collections.Generic;

namespace MauiFilmLibrary.Models;

public partial class Person
{
    public int PersonId { get; set; }

    public string PersonName { get; set; } = null!;

    public virtual ICollection<MoviePersonRole> MoviePersonRoles { get; set; } = new List<MoviePersonRole>();
}
