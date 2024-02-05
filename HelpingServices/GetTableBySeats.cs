using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;

namespace RESTaurantAPI.HelpingServices
{
    public static class GetTableBySeats
    {
        public static async Task<Table> GetSpecyficTableBySeats(this DbSet<Table> tables, int seats, CancellationToken cancellationToken)
        {
            Table table = await tables.FirstOrDefaultAsync(r => r.Seats == seats, cancellationToken);

            return table;
        }
    }
}
