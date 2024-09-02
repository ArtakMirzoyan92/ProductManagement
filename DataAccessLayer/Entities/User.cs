using System;
using System.Collections.Generic;

namespace DataAccessLayer.Entities;

public partial class User
{
    public Guid Id { get; set; }

    public string UserName { get; set; }

    public string FirstName { get; set; }

    public string Email { get; set; }

    public string City { get; set; }

    public string PasswordHash { get; set; }
}
