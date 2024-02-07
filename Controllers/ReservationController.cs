using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using RESTaurantAPI.DTOs;
using RESTaurantAPI.Models;
using RESTaurantAPI.Services;

namespace RESTaurantAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]

    public class ReservationController : Controller
    {
        private readonly IMapper _mapper;
        private readonly ReservationService _reservationService;

        public ReservationController(ReservationService reservationService, IMapper _mapper)
        {
            this._mapper = _mapper;
            this._reservationService = reservationService;
        }

        [HttpGet("GetAllReservations")]
        public async Task<ActionResult<List<ReservationDto>>> GetAllReservations(
            CancellationToken cancellationToken, int? skip = null, int? limit = null)
        {
            var reservations = await this._reservationService.GetAllReservations(cancellationToken);
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

        [HttpGet("GetById/{id}")]
        public async Task<ActionResult<ReservationDto>> GetById(int id, CancellationToken cancellationToken)
        {
            var reservation = await this._reservationService.GetById(id, cancellationToken);
            var reservationDto = this._mapper.Map<ReservationDto>(reservation);
            if (reservationDto == null)
            {
                return NotFound();
            }

            return Ok(reservationDto);
        }

        [HttpPost("AddReservation")]
        public async Task<ActionResult<ReservationDto>> AddReservation(DateTime date, int seatsNumber, CancellationToken cancellationToken)
        {
            var reservation = await this._reservationService.AddReservation(date, seatsNumber, cancellationToken);
            var reservationDto = this._mapper.Map<ReservationDto>(reservation);

            return Ok(reservationDto);
        }

        [HttpDelete("Cancel/{id}")]
        public async Task<ActionResult> CancelReservation(int id, CancellationToken cancellationToken)
        {
            await this._reservationService.CancelReservation(id, cancellationToken);

            return Ok("Reservation has been cancelled successfully");
        }

    }
}
