using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Dal
{
    public class SlotsDbContext: DbContext
    {
        public SlotsDbContext(DbContextOptions<SlotsDbContext> options)
        : base(options) { }

        public DbSet<BookedSlot> BookedSlots { get; set; }
    }
}
