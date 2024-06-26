﻿namespace ISA.Application.API.Controllers
{
    using ISA.Application.API.Models.Requests;
    using ISA.Core.Domain.Dtos;
    using ISA.Core.Domain.UseCases.Reservation;
    using Microsoft.AspNetCore.Authentication.JwtBearer;
    using Microsoft.AspNetCore.Authorization;
    using Microsoft.AspNetCore.Mvc;

    [ApiController]
    [Route("[controller]")]
    public class ReservationController : ControllerBase
    {
        private readonly ReservationService _reservationService;
        private readonly IHttpContextAccessor _contextAccessor;

        public ReservationController(ReservationService reservationService, IHttpContextAccessor contextAccessor)
        {
            _reservationService = reservationService;
            _contextAccessor = contextAccessor;
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Add")]
        [Authorize(Policy = "customerPolicy")]
        public async Task AddReservation([FromBody] ReservationRequest reservation)
        {
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == "id").Value);
            await _reservationService.AddAsync(userId, reservation.AppointmentId, reservation.Equipments);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("AddExtraordinary")]
        [Authorize(Policy = "customerPolicy")]
        public async Task AddReservationWithExtraordinaryAppointment([FromBody] ExtraordinaryReservationRequest reservation)
        {
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == "id").Value);
            await _reservationService.AddExtraordinaryAsync(userId, reservation.CompanyId, reservation.Appointment.StartingDateTime, reservation.Appointment.EndingDateTime, reservation.Equipments);
        }


        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("Cancel")]
        [Authorize(Policy = "customerPolicy")]
        public async Task CancelReservation(Guid reservationId)
        {
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == "id").Value);
            await _reservationService.CancelReservation(userId, reservationId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("CompanyReservations")]
        [Authorize(Policy = "corpAdminPolicy")]
        public async Task<IEnumerable<ReservationDto>> GetAllCompanyReservations()
        {
            Guid adminId = Guid.Parse(User.Claims.First(x => x.Type == "id").Value);
            return await _reservationService.GetAllCompanyReservations(adminId);
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("CustomerReservations")]
        [Authorize(Policy = "customerPolicy")]
        public async Task<IEnumerable<ReservationDto>> GetAllCustomerReservations()
        {
            Guid adminId = Guid.Parse(User.Claims.First(x => x.Type == "id").Value);
            return await _reservationService.GetAllCustomerReservations(adminId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("CustomerHistoryOfReservations/{customerId}")]
        [Authorize(Policy = "corpAdminPolicy")]
        public async Task<IEnumerable<ReservationDto>> GetAllCustomerReservations([FromRoute] Guid customerId)
        {
            Guid adminId = Guid.Parse(User.Claims.First(x => x.Type == "id").Value);
            return await _reservationService.GetHistoryOfCustomerReservations(adminId, customerId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("CustomerScheduledReservations")]
        [Authorize(Policy = "customerPolicy")]
        public async Task<IEnumerable<ReservationDto>> GetAllScheduledCustomerReservations()
        {
            Guid adminId = Guid.Parse(User.Claims.First(x => x.Type == "id").Value);
            return await _reservationService.GetAllScheduledCustomerReservations(adminId);
        }



        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpPost("PickedUp/{reservationId}")]
        [Authorize(Policy = "corpAdminPolicy")]
        public async Task ReservationPickedUp([FromRoute] Guid reservationId)
        {
            Guid adminId = Guid.Parse(User.Claims.First(x => x.Type == "id").Value);
            await _reservationService.ReservationPickedUp(adminId, reservationId);
        }

        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        [HttpGet("{reservationId}")]
        [Authorize(Policy = "CorpAdminAndCustomerPolicy")]
        public async Task<ReservationDto> GetReservation([FromRoute] Guid reservationId)
        {
            Guid userId = Guid.Parse(User.Claims.First(x => x.Type == "id").Value);
            return await _reservationService.GetReservation(reservationId, userId);
        }

    }
}


