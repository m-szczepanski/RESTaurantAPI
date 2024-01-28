using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RESTaurantAPI.Data;
using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;


namespace RESTaurantAPI.Services
{
    public class TableService
    {
        private readonly APIDbContext dbContext;

        public TableService(APIDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public async Task<List<Table>> GetAllTables(CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var tables = await dbContext.Tables
                .ToListAsync(cancellationToken);

            return tables == null ? throw new ApplicationException("No tables are in the database right now.") : tables;
        }

        public async Task<Table> GetTableById(int tableId, CancellationToken cancellationToken)
        {
            var table = await dbContext.Tables.Where(x => x.Id == tableId).FirstOrDefaultAsync(cancellationToken);

            return table == null ? throw new ApplicationException("No table found") : table;
        }

        public async Task<Table> AddTable(int seats, bool availability, CancellationToken cancellationToken)
        {
            var newTable = new Table
            {
                Seats = seats,
                Availability = availability,
            };

            dbContext.Tables.Add(newTable);
            await dbContext.SaveChangesAsync(cancellationToken);

            return newTable;
        }

        public async Task<List<Table>> GetEmptyTableBySeatsNumber(int seatsNumber, CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var tables = await dbContext.Tables.Where(s => s.Seats == seatsNumber && s.Availability == true).ToListAsync(cancellationToken);

            return tables == null ? throw new ApplicationException("No table with that number of seats is available at the moment.") : tables;
        }

        public async Task UpdateTable(int tableId, int seats, bool availability, CancellationToken cancellationToken)
        {
            Table table = await dbContext.Tables.FirstOrDefaultAsync(s => s.Id == tableId, cancellationToken);

            table.Seats = seats;
            table.Availability = availability;

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task MarkAsTaken(int tableId, CancellationToken cancellationToken)
        {
            Table table = await dbContext.Tables.FirstOrDefaultAsync(s => s.Id == tableId, cancellationToken);

            table.Availability = false;

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task MarkAsEmpty(int tableId, CancellationToken cancellationToken)
        {
            Table table = await dbContext.Tables.FirstOrDefaultAsync(s => s.Id == tableId, cancellationToken);


            table.Availability = true;

            await dbContext.SaveChangesAsync(cancellationToken);
        }

        public async Task DeleteTable(int tableId, CancellationToken cancellationToken)
        {
            var table = await dbContext.Tables.FirstOrDefaultAsync(x => x.Id == tableId, cancellationToken);

            if (table == null)
            {
                throw new ApplicationException("You don't have permission to complete this action or table with that id exists.");
            }

            dbContext.Tables.Remove(table);
            await dbContext.SaveChangesAsync(cancellationToken);
        }

    }
}
