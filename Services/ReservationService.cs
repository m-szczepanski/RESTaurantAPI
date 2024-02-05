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
            var reservations = await _dbContext.Reservations.ToListAsync(cancellationToken);

            return reservations == null ? throw new ApplicationException("No reservations are in the database right now.") : reservations;
        }

        public async Task<Reservation> GetReservationById(int reservationId, CancellationToken cancellationToken)
        {
            var reservation = await _dbContext.Reservations.Where(x => x.Id == reservationId)
                .FirstOrDefaultAsync(cancellationToken);

            return reservation == null ? throw new ApplicationException("No reservation was found") : reservation;
        }

        public async Task<List<Reservation>> GetReservationsByDate(DateTime date, CancellationToken cancellationToken)
        {
            var reservations = await _dbContext.Reservations.Where(x=>x.Date == date).ToListAsync(cancellationToken);

            return reservations == null ? throw new ApplicationException($"No reservations were found for {date}.") : reservations;
        }

        public async Task<List<Reservation>> GetReservationsByDateAndHour(DateTime date, TimeOnly hour, CancellationToken cancellationToken)
        {
            var reservations = await _dbContext.Reservations.Where(x => x.Date == date && x.Hour == hour)
                .ToListAsync(cancellationToken);

            return reservations == null ? throw new ApplicationException($"No reservations were found for {date}, {hour}.") : reservations;
        }

        /*public async Task<List<Dish>> GetReservationByTableId(int tableId, CancellationToken cancellationToken)
        {
            var table = _dbContext.Tables.GetSpecyficTableById(tableId, cancellationToken);
            var reservations = await _dbContext.Reservations.Where(x => x.Table.Id == table.Id).ToListAsync(cancellationToken);

            return reservations == null ? throw new ApplicationException("No dishes with that cuisine were found") : reservations;
        }*/

        /*public async Task<List<Dish>> GetReservationByTableIdAndDate(int tableId, DateTime date, CancellationToken cancellationToken)*/

public async Task<Reservation> AddReservation(DateTime date, TimeOnly hour, int seatsNumber,
    CancellationToken cancellationToken)
{
    //var table = _dbContext.Tables.GetSpecyficTableById(tableId, cancellationToken);
    var dateToday = DataHelper.GetTodayDate();
    var table = GetTableBySeats.GetSpecyficTableBySeats(_dbContext.Tables, seatsNumber, cancellationToken);

    if (date == dateToday)
        throw new ApplicationException("Cannot place reservation on the same day.");

    var newReservation = new Reservation
    {
        Date = date,
        Hour = hour,
        NumberOfSeats = seatsNumber,
        Table = table.Result
    };

    _dbContext.Reservations.Add(newReservation);
    await _dbContext.SaveChangesAsync(cancellationToken);

    return newReservation;
}

public async Task UpdateReservation(int reservationId, DateTime date, TimeOnly hour, int seatsNumber,
    CancellationToken cancellationToken)
{
    Reservation reservation = await _dbContext.Reservations.FirstOrDefaultAsync(x =>x.Id == reservationId, cancellationToken);

    reservation.Date = date;
    reservation.Hour = hour;
    reservation.NumberOfSeats = seatsNumber;

    await _dbContext.SaveChangesAsync(cancellationToken);
}

public async Task CancelReservation(int reservationId, CancellationToken cancellationToken)
{
    var reservation =
        await _dbContext.Reservations.FirstOrDefaultAsync(x => x.Id == reservationId, cancellationToken);

    if (reservation != null)
        throw new ApplicationException("Reservation with that id doesn't exist.");

    _dbContext.Reservations.Remove(reservation);
    await _dbContext.SaveChangesAsync(cancellationToken);
}
}
}
