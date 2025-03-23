using System;
using System.Collections.Generic;

namespace MauiFilmLibrary.Models.Models;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleName { get; set; } = null!;

    public virtual ICollection<MoviePersonRole> MoviePersonRoles { get; set; } = new List<MoviePersonRole>();
}
