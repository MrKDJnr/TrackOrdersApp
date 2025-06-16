using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using TrackOrdersApp.Data;
using TrackOrdersApp.Models;

namespace TrackOrdersApp.Pages
{
    public class OrdersModel : PageModel
    {
        private readonly AppDbContext _context;

        public OrdersModel(AppDbContext context)
        {
            _context = context;
        }

        public List<Order> FilteredOrders { get; set; } = new();

        [BindProperty(SupportsGet = true)]
        public string? Status { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? FromDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public DateTime? ToDate { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MinTotal { get; set; }

        [BindProperty(SupportsGet = true)]
        public decimal? MaxTotal { get; set; }

        public async Task OnGetAsync()
        {
            var query = _context.Orders.Include(o => o.Items).AsQueryable();

            if (!string.IsNullOrEmpty(Status) && Enum.TryParse(Status, out OrderStatus parsedStatus))
            {
                query = query.Where(o => o.Status == parsedStatus);
            }

            if (FromDate.HasValue)
            {
                query = query.Where(o => o.CreatedDate >= FromDate.Value);
            }

            if (ToDate.HasValue)
            {
                query = query.Where(o => o.CreatedDate <= ToDate.Value);
            }

            if (MinTotal.HasValue)
            {
                query = query.Where(o => o.Total >= MinTotal.Value);
            }

            if (MaxTotal.HasValue)
            {
                query = query.Where(o => o.Total <= MaxTotal.Value);
            }

            FilteredOrders = await query.OrderByDescending(o => o.CreatedDate).ToListAsync();
        }

        [IgnoreAntiforgeryToken]
        public async Task<IActionResult> OnPostMarkPaidAsync(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            order.Status = OrderStatus.Paid;
            await _context.SaveChangesAsync();

            return new JsonResult(new { success = true });
        }


    }
}
