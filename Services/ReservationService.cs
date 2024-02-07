using RESTaurantAPI.Data;
using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;
using RESTaurantAPI.HelpingServices;


namespace RESTaurantAPI.Services
{
    public class ReservationService
    {
        private readonly APIDbContext _dbContext;

        public ReservationService(APIDbContext dbContext)
        {
            this._dbContext = dbContext;
        }

        public async Task<List<Reservation>> GetAllReservations(CancellationToken cancellationToken, int? skip = null,
            int? limit = null)
        {
            var reservations = await this._dbContext.Reservations
                .Include(x=>x.Table).ToListAsync(cancellationToken);

            return reservations == null
                ? throw new ApplicationException("No reservations are in the database right now.")
                : reservations;
        }

        public async Task<Reservation> GetReservationById(int reservationId, CancellationToken cancellationToken)
        {
            var reservation = await this._dbContext.Reservations.Where(x => x.Id == reservationId)
                .FirstOrDefaultAsync(cancellationToken);

            return reservation == null ? throw new ApplicationException("No reservation was found") : reservation;
        }

        public async Task<List<Reservation>> GetReservationsByDate(DateTime date, CancellationToken cancellationToken)
        {
            var reservations = await this._dbContext.Reservations.Where(x => x.Date == date).ToListAsync(cancellationToken);

            return reservations == null
                ? throw new ApplicationException($"No reservations were found for {date}.")
                : reservations;
        }

        public async Task<Reservation> AddReservation(DateTime date, int seatsNumber, CancellationToken cancellationToken)
        {
            ReservationHelpers.ValidateDay(date);
            var table = TableHelpers.GetTableBySeats(_dbContext.Tables, seatsNumber, cancellationToken);

            var newReservation = new Reservation
            {
                Date = date,
                NumberOfSeats = seatsNumber,
                Table = table.Result
            };

            _dbContext.Reservations.Add(newReservation);
            await _dbContext.SaveChangesAsync(cancellationToken);

            return newReservation;
        }

        public async Task CancelReservation(int reservationId, CancellationToken cancellationToken)
        {
            var reservation =
                await this._dbContext.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId, cancellationToken);

            if (reservation == null)
                throw new ApplicationException("Reservation with that id doesn't exist.");

            _dbContext.Reservations.Remove(reservation);
            await _dbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
