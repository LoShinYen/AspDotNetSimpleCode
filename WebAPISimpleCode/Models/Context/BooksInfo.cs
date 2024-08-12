using System;
using System.Collections.Generic;

namespace WebAPISimpleCode.Models.Context;

public partial class BooksInfo
{
    public int BookId { get; set; }

    public int? RankingListId { get; set; }

    public string PlatformId { get; set; } = null!;

    public string? Isbn { get; set; }

    public string? Title { get; set; }

    public string? BookUrl { get; set; }

    public string? Category { get; set; }

    public DateOnly? PublishedAt { get; set; }

    public virtual ICollection<BookAuthor> BookAuthors { get; set; } = new List<BookAuthor>();

    public virtual RankingList? RankingList { get; set; }
}
