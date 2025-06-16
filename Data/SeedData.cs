using Microsoft.EntityFrameworkCore;
using TrackOrdersApp.Models;
using TrackOrdersApp.Data;

namespace OrderViewer.Data
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider services)
        {
            using var context = new AppDbContext(
                services.GetRequiredService<DbContextOptions<AppDbContext>>());

            if (context.Orders.Any()) return;

            var random = new Random();
            var statuses = Enum.GetValues<OrderStatus>();

            for (int i = 1; i <= 50; i++)
            {
                var order = new Order
                {
                    CustomerName = $"Customer {i}",
                    CreatedDate = DateTime.Today.AddDays(-random.Next(30)),
                    Status = statuses[random.Next(statuses.Length)],
                    Items = new List<OrderItem>()
                };

                for (int j = 0; j < random.Next(1, 5); j++)
                {
                    var item = new OrderItem
                    {
                        ProductName = $"Product {random.Next(1, 100)}",
                        Quantity = random.Next(1, 5),
                        UnitPrice = Math.Round((decimal)(random.NextDouble() * 100), 2)
                    };
                    order.Total += item.Quantity * item.UnitPrice;
                    order.Items.Add(item);
                }

                context.Orders.Add(order);
            }

            await context.SaveChangesAsync();
        }
    }
}
