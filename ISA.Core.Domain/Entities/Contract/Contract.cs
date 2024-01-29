﻿namespace ISA.Core.Domain.Entities.Delivery;

public class Contract : Entity<Guid>
{
    public Company.Company Company { get; set; }
    public List<ContractEquipment> Equipments { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime DeliveryDate { get; set; }

    public Contract()
    {

    }

    public Contract(Company.Company company, List<ContractEquipment> equipments, DateTime createdAt, DateTime deliveryDate)
    {
        Company = company;
        Equipments = equipments;
        CreatedAt = createdAt;
        DeliveryDate = deliveryDate;
    }
}