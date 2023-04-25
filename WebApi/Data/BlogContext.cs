using Lab6.WebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace Lab6.WebApi.Data;

public class BlogContext : DbContext
{
    public BlogContext(DbContextOptions<BlogContext> options) : base(options)
    {


    }

    public DbSet<BlogEntry> Entries { get; set; } = default!;
}