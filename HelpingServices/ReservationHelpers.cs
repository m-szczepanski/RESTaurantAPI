using Microsoft.EntityFrameworkCore;
using RESTaurantAPI.Models;

namespace RESTaurantAPI.HelpingServices
{
    public class ReservationHelpers
    {
        public static async Task ValidateDay(DateTime date)
        {
            var dayToday = DateTime.Today;

            if (date > dayToday)
                throw new ApplicationException("Reservation must be made on a future date.");

            if (date == dayToday)
                throw new ApplicationException("Cannot place reservation on the same day.");
        }
    }
}
