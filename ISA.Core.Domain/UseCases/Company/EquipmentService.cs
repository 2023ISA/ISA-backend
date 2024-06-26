﻿using AutoMapper;
using ISA.Core.Domain.Contracts.Repositories;
using ISA.Core.Domain.Contracts.Services;
using ISA.Core.Domain.Dtos;
using ISA.Core.Domain.Entities.Company;
using ISA.Core.Domain.Entities.Reservation;
using ISA.Core.Domain.Entities.User;
using ISA.Core.Domain.UseCases.User;
using Nest;
using System.Xml.Linq;


namespace ISA.Core.Domain.UseCases.Company;

public class EquipmentService : BaseService<EquipmentDto, Equipment>, IEquipmentService
{
    private readonly IEquipmentRepository _equipmentRepository;
    private readonly IISAUnitOfWork _isaUnitOfWork;
    private readonly ICompanyAdminRepository _companyAdminRepository;
    private readonly IReservationRepository _reservationRepository;
    private readonly ICompanyRepository _companyRepository;
    private readonly IMapper _mapper;

    public EquipmentService(IEquipmentRepository equipmentRepository,ICompanyAdminRepository companyAdminRepository, IReservationRepository reservationRepository, ICompanyRepository companyRepository, IISAUnitOfWork isaUnitOfWork, IMapper mapper)  : base(mapper)

    {
        _equipmentRepository = equipmentRepository;
        _companyAdminRepository = companyAdminRepository;
        _reservationRepository = reservationRepository;
        _companyRepository = companyRepository;
        _isaUnitOfWork = isaUnitOfWork;
        _mapper = mapper;
    }

    public async Task AddAsync(string equipmentName, int quantity, Guid userId)
    {
        
        var companyAdmin = await _companyAdminRepository.GetByIdAsync(userId);
        var company = await _companyRepository.GetByIdAsync(companyAdmin.CompanyId);
        Equipment equipment = new(equipmentName,quantity, company.Id);
        if (company is not null)
        {
            await _isaUnitOfWork.StartTransactionAsync();
            try
            {
                await _equipmentRepository.AddAsync(equipment);
                await _isaUnitOfWork.SaveAndCommitChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

    }

    public async Task AddGeneralAsync(string equipmentName)
    {

        Equipment equipment = new(equipmentName);
        await _isaUnitOfWork.StartTransactionAsync();
        try
        {
            await _equipmentRepository.AddAsync(equipment);
            await _isaUnitOfWork.SaveAndCommitChangesAsync();
        }
        catch (Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }

    public async Task AddContractEquipmentAsync(Equipment equipment)
    {

        await _equipmentRepository.AddAsync(equipment);
        
    }

    public async Task DeleteEquipment(Guid id)
    {

        await _equipmentRepository.DeleteContract(id);

    }

    public async Task<List<GeneralEquipmentDto>> GetGeneralEquipment(int page)
    {
        var generalEquipments =  await _equipmentRepository.GetGeneralEquipment(page);
        List<GeneralEquipmentDto> equipments = new();

        foreach(var equipment in generalEquipments)
        {
            equipments.Add(new GeneralEquipmentDto(equipment.Id,equipment.Name));
        }

        return equipments;
    }


    public async Task RemoveAsync(Guid id, Guid userId)
    {
        
        if (await _reservationRepository.EquipmentCanBeDeleted(id) is false)
        {
            throw new KeyNotFoundException("Oprema ne moze biti obrisana jer se nalazi u zakazanim rezervacijama.");
        }
        var equipment = await _equipmentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        equipment.Delete();
        _equipmentRepository.UpdateAndSaveChanges(equipment);
        
    }

    public async Task<Equipment> GetById(Guid id)
    {
        return await _equipmentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException();
    }

    public async Task UpdateAsync(Guid id, string name, int quantity, Guid userId)
    {
        await _isaUnitOfWork.StartTransactionAsync();
        var companyAdmin = await _companyAdminRepository.GetByIdAsync(userId) ?? throw new KeyNotFoundException();
        var equipment = await _equipmentRepository.GetByIdAsync(id) ?? throw new KeyNotFoundException();
        if (companyAdmin.CompanyId != equipment.CompanyId) throw new ArgumentException();
        equipment.Name = name;
        equipment.Quantity = quantity;
        _equipmentRepository.Update(equipment);
        await _isaUnitOfWork.SaveAndCommitChangesAsync();
        
    }


    

    public async Task ReturnEqupment(IEnumerable<ReservationEquipment> equipment)
    {
        foreach (var equipmentItem in equipment)
        {
            var foundEqupment = await _equipmentRepository.GetByIdAsync(equipmentItem.EquipmentId) ?? throw new KeyNotFoundException();
            foundEqupment.ReturnEquipment(equipmentItem.Quantity);
            _equipmentRepository.Update(foundEqupment);
        }
    }

    public async Task EquipmentSold(Guid id, int quantity)
    {
        var equipment = await GetById(id);
        equipment.Quantity -= quantity;
        _equipmentRepository.Update(equipment);

    }

    public async Task<bool> ExistEnough(Guid id, int quantity)
    {
        return await _equipmentRepository.ExistEnough(id, quantity);
    }

    public async Task UpdateAsync(Equipment equipment)
    {
        _equipmentRepository.UpdateAndSaveChanges(equipment);
    }


}





