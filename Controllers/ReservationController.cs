using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTaurantAPI.DTOs;
using RESTaurantAPI.Services;

namespace RESTaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ReservationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ReservationService reservationService;

        public ReservationController(ReservationService reservationService, IMapper _mapper)
        {
            this._mapper = _mapper;
            this.reservationService = reservationService;
        }

        [HttpGet("GetAllReservations")]
        public async Task<ActionResult<List<ReservationDto>>> GetAllReservations(
            CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var reservations = await this.reservationService.GetAllReservations(cancellationToken);
            var reservationsDto = this._mapper.Map<List<ReservationDto>>(reservations);

            if (skip.HasValue)
            {
                reservationsDto = reservationsDto.Skip(skip.Value).ToList();
            }

            if (limit.HasValue)
            {
                reservationsDto = reservationsDto.Take(limit.Value).ToList();
            }

            return Ok(reservationsDto);
        }



        /*[HttpPost("AddReservation")]
        public async Task<ActionResult<ReservationDto>> AddReservation(DateTime date, string hour, int seatsNumber,
            CancellationToken cancellationToken)
        {
            var reservation = await this.reservationService.AddReservation(date, hour, seatsNumber, cancellationToken);
            var reservationDto = this._mapper.Map<TableDto>(reservation);

            return Ok(reservationDto);
        }*/

    }
}
