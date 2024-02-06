using System.Globalization;
using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;


namespace RESTaurantAPI.HelpingServices
{
    public static class GetSpecyficTable
    {
        public static async Task<Table> GetTableById(this DbSet<Table> tables, int id, CancellationToken cancellationToken)
        {
            Table table = await tables.FirstOrDefaultAsync(r => r.Id == id , cancellationToken);

            return table;
        }
    }
}
