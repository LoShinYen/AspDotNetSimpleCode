using System;
using System.Collections.Generic;

namespace WebAPISimpleCode.Models.Context;

public partial class RankingList
{
    public int RankingListId { get; set; }

    public DateTime? RecordDatetime { get; set; }

    public virtual ICollection<BooksInfo> BooksInfos { get; set; } = new List<BooksInfo>();
}
