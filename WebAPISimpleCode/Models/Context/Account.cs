using System;
using System.Collections.Generic;

namespace WebAPISimpleCode.Models.Context;

public partial class Account
{
    public int AccountId { get; set; }

    public string Username { get; set; } = null!;

    public string PasswordHash { get; set; } = null!;

    public string Salt { get; set; } = null!;

    public string Email { get; set; } = null!;

    public bool? IsActive { get; set; }

    public DateTime? CreatedAt { get; set; }
}
