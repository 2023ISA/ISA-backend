﻿namespace ISA.Core.Domain.UseCases.Reservation;

using AutoMapper;
using ISA.Application.API.Models.Requests;
using ISA.Core.Domain.Contracts.Repositories;
using ISA.Core.Domain.Contracts.Services;
using ISA.Core.Domain.Entities.Reservation;

public class ReservationService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly ICustomerRepository _customerRepository;
    private readonly IAppointmentRepository _appointmentRepository;
    private readonly IReservationEquipmentRepository _reservationEquipmentRepository;
    private readonly IISAUnitOfWork _isaUnitOfWork;
    private readonly IMapper _mapper;

    public ReservationService(IEquipmentRepository equipmentRepository, IReservationRepository reservationRepository, ICustomerRepository customerRepository, IAppointmentRepository appointmentRepository, IReservationEquipmentRepository reservationEquipmentRepository, IISAUnitOfWork isaUnitOfWork, IMapper mapper)
    {
        _equipmentRepository = equipmentRepository;
        _reservationRepository = reservationRepository;
        _customerRepository = customerRepository;
        _appointmentRepository = appointmentRepository;
        _reservationEquipmentRepository = reservationEquipmentRepository;
        _isaUnitOfWork = isaUnitOfWork;
        _mapper = mapper;
    }

    public async Task AddAsync(Guid userId, Guid appointmentId, List<ReservationEquipmentRequest> requests)
    {
        var customer = await _customerRepository.GetByIdAsync(userId);
        var appointment = await _appointmentRepository.GetByIdAsync(appointmentId);
        List<ReservationEquipment> reservationEquipment = new List<ReservationEquipment>();
        if (customer is null || appointment is null)
        {
            throw new ArgumentNullException("Not good appointment");
        }

        await _isaUnitOfWork.StartTransactionAsync();
        try
        {
            foreach(var r in requests)
            {
                if  (await _equipmentRepository.ExistEnough(r.EquipmentId, r.Quantity) is false){
                    throw new ArgumentException("No enough equipment");
                }
                ReservationEquipment re = new ReservationEquipment(appointment.Id, r.EquipmentId, r.Quantity);
                reservationEquipment.Add(re);
            }

           
            Reservation reservation = new Reservation(appointment, customer, reservationEquipment);
            await _reservationRepository.AddAsync(reservation);
            foreach (var r in reservation.Equipments)
            {
                await _reservationEquipmentRepository.AddAsync(r);
                await _equipmentRepository.EquipmentSold(r.EquipmentId, r.Quantity);
            }
            await _isaUnitOfWork.SaveAndCommitChangesAsync();
            
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }

    }



    public async Task<List<Reservation>> OverdueReservations()
    {
        return await _reservationRepository.CheckForOverdueReservations();
    }

    public async Task SetReservationAsOverdue(Guid reservationId)
    {
        var reservation = await _reservationRepository.GetByIdAsync(reservationId) ?? throw new KeyNotFoundException();
        reservation.SetAsOverdue();
        _reservationRepository.Update(reservation);
    }
}
