using System;
using System.Collections.Generic;

namespace WebAPISimpleCode.Models.Context;

public partial class AuthorList
{
    public int AuthorId { get; set; }

    public string? AuthorName { get; set; }

    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();
}
