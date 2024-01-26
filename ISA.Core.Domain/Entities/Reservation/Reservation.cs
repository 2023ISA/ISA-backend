﻿using ISA.Core.Domain.Entities.Company;
using ISA.Core.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ISA.Core.Domain.Entities.Reservation;

public enum ReservationState
{
    Finished = 0,
    Pending = 1,
    Overdue = 2,

}
public class Reservation
{
    [Key]
    [ForeignKey("Appointment")] 
    public Guid AppointmentID { get; set; }
    public virtual Appointment Appointment { get; set; }
    public ReservationState State { get; set; } = ReservationState.Pending;
    public virtual Customer Customer { get; set; }
    public virtual List<ReservationEquipment> Equipments { get; set; }

    public Reservation()
    {

    }
    public Reservation(Appointment appointment, ReservationState state)
    {
        Appointment = appointment;
        State = state;
    }

    public Reservation(Appointment appointment, Customer customer, List<ReservationEquipment> equipments)
    {
        Appointment = appointment;
        Customer = customer;
        Equipments = equipments;
    }

    public void SetAsOverdue()
    {
        State = ReservationState.Overdue;
    }
}
