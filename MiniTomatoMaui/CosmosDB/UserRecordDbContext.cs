
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Cosmos;
using MiniTomatoMaui.Data;
using System.Linq;

namespace MiniTomatoMaui.CosmosDB;

public class UserRecordDbContext : DbContext
{
    public UserRecordDbContext()
    {

    }

    public DbSet<UserRecord> UserRecords { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultContainer("mood-user-db");

        modelBuilder.Entity<UserRecord>(entity =>
        {
            entity.ToContainer("userrecord")
            .HasPartitionKey(x => x.Id);

            entity.Property(x => x.Id)
            .HasConversion(y => y.ToString(), y => new Guid(y));

            entity.HasNoDiscriminator();
        }
        );
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseCosmos("AccountEndpoint=https://mood-users.documents.azure.com:443/;AccountKey=MXEkdkP6kvzk3FM1CNjLwHHWLQc5Fk06w0g2WkEtaWVRcIgP2PwXkSr4wZEgCvBmkYNXsG3Zh3iq9iMiiOLG0A==;", "mood-user-db");
        base.OnConfiguring(optionsBuilder);
    }
}
