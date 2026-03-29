using Microsoft.EntityFrameworkCore;
using CloudBackend.Models;

namespace CloudBackend.Data;

public class CloudTaskDbContext : DbContext
{
    public CloudTaskDbContext(DbContextOptions<CloudTaskDbContext> options) : base(options) { }

    public DbSet<CloudTask> Tasks { get; set; } 
}