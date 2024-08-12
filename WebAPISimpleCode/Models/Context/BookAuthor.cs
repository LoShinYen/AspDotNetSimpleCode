using System;
using System.Collections.Generic;

namespace WebAPISimpleCode.Models.Context;

public partial class BookAuthor
{
    public int BookAuthorId { get; set; }

    public int? BookId { get; set; }

    public int? AuthorId { get; set; }

    public virtual AuthorList? Author { get; set; }

    public virtual BooksInfo? Book { get; set; }
}
