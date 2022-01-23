using Domain.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Dal
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using var context = new SlotsDbContext(
            serviceProvider.GetRequiredService<DbContextOptions<SlotsDbContext>>());
            context.BookedSlots.AddRange(new BookedSlot { Id = Guid.NewGuid().ToString(), StartTimeHour = 11, StartTimeMinute = 0 },
                new BookedSlot { Id = Guid.NewGuid().ToString(), StartTimeHour = 12, StartTimeMinute = 0 },
                new BookedSlot { Id = Guid.NewGuid().ToString(), StartTimeHour = 12, StartTimeMinute = 30 },
                new BookedSlot { Id = Guid.NewGuid().ToString(), StartTimeHour = 15, StartTimeMinute = 30 });
            context.SaveChanges();
        }
    }
}
