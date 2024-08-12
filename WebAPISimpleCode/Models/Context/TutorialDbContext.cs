using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPISimpleCode.Models.Context;

public partial class TutorialDbContext : DbContext
{
    public TutorialDbContext(DbContextOptions<TutorialDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Account> Accounts { get; set; }

    public virtual DbSet<AuthorList> AuthorLists { get; set; }

    public virtual DbSet<BookAuthor> BookAuthors { get; set; }

    public virtual DbSet<BooksInfo> BooksInfos { get; set; }

    public virtual DbSet<RankingList> RankingLists { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .UseCollation("utf8mb4_general_ci")
            .HasCharSet("utf8mb4");

        modelBuilder.Entity<Account>(entity =>
        {
            entity.HasKey(e => e.AccountId).HasName("PRIMARY");

            entity.ToTable("accounts");

            entity.HasIndex(e => e.Username, "username").IsUnique();

            entity.Property(e => e.AccountId)
                .HasColumnType("int(11)")
                .HasColumnName("account_id");
            entity.Property(e => e.CreatedAt)
                .HasDefaultValueSql("current_timestamp()")
                .HasColumnType("datetime")
                .HasColumnName("created_at");
            entity.Property(e => e.Email)
                .HasMaxLength(255)
                .HasColumnName("email");
            entity.Property(e => e.IsActive)
                .HasDefaultValueSql("'0'")
                .HasColumnName("is_active");
            entity.Property(e => e.PasswordHash)
                .HasMaxLength(255)
                .HasColumnName("password_hash");
            entity.Property(e => e.Salt)
                .HasMaxLength(255)
                .HasColumnName("salt");
            entity.Property(e => e.Username).HasColumnName("username");
        });

        modelBuilder.Entity<AuthorList>(entity =>
        {
            entity.HasKey(e => e.AuthorId).HasName("PRIMARY");

            entity.ToTable("author_list");

            entity.Property(e => e.AuthorId)
                .HasColumnType("int(11)")
                .HasColumnName("author_id");
            entity.Property(e => e.AuthorName)
                .HasMaxLength(255)
                .HasColumnName("author_name");
        });

        modelBuilder.Entity<BookAuthor>(entity =>
        {
            entity.HasKey(e => e.BookAuthorId).HasName("PRIMARY");

            entity.ToTable("book_authors");

            entity.HasIndex(e => e.AuthorId, "author_id");

            entity.HasIndex(e => e.BookId, "book_id");

            entity.Property(e => e.BookAuthorId)
                .HasColumnType("int(11)")
                .HasColumnName("book_author_id");
            entity.Property(e => e.AuthorId)
                .HasColumnType("int(11)")
                .HasColumnName("author_id");
            entity.Property(e => e.BookId)
                .HasColumnType("int(11)")
                .HasColumnName("book_id");

            entity.HasOne(d => d.Author).WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.AuthorId)
                .HasConstraintName("book_authors_ibfk_2");

            entity.HasOne(d => d.Book).WithMany(p => p.BookAuthors)
                .HasForeignKey(d => d.BookId)
                .HasConstraintName("book_authors_ibfk_1");
        });

        modelBuilder.Entity<BooksInfo>(entity =>
        {
            entity.HasKey(e => e.BookId).HasName("PRIMARY");

            entity.ToTable("books_info");

            entity.HasIndex(e => e.RankingListId, "ranking_list_id");

            entity.Property(e => e.BookId)
                .HasColumnType("int(11)")
                .HasColumnName("book_id");
            entity.Property(e => e.BookUrl)
                .HasColumnType("text")
                .HasColumnName("book_url");
            entity.Property(e => e.Category)
                .HasColumnType("text")
                .HasColumnName("category");
            entity.Property(e => e.Isbn)
                .HasMaxLength(20)
                .HasColumnName("isbn");
            entity.Property(e => e.PlatformId)
                .HasMaxLength(50)
                .HasColumnName("platform_id");
            entity.Property(e => e.PublishedAt).HasColumnName("published_at");
            entity.Property(e => e.RankingListId)
                .HasColumnType("int(11)")
                .HasColumnName("ranking_list_id");
            entity.Property(e => e.Title)
                .HasMaxLength(255)
                .HasColumnName("title");

            entity.HasOne(d => d.RankingList).WithMany(p => p.BooksInfos)
                .HasForeignKey(d => d.RankingListId)
                .HasConstraintName("books_info_ibfk_1");
        });

        modelBuilder.Entity<RankingList>(entity =>
        {
            entity.HasKey(e => e.RankingListId).HasName("PRIMARY");

            entity.ToTable("ranking_list");

            entity.Property(e => e.RankingListId)
                .HasColumnType("int(11)")
                .HasColumnName("ranking_list_id");
            entity.Property(e => e.RecordDatetime)
                .HasColumnType("datetime")
                .HasColumnName("record_datetime");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
