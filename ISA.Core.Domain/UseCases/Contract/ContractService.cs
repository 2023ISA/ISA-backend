﻿using ISA.Core.Domain.Contracts.Repositories;
using ISA.Core.Domain.Contracts.Services;
using ISA.Core.Domain.Dtos.Requests;
using ISA.Core.Domain.Entities.Company;
using ISA.Core.Domain.Entities.Delivery;
using ISA.Core.Domain.Entities.User;
using ISA.Core.Domain.UseCases.Company;
using ISA.Core.Domain.UseCases.User;

namespace ISA.Core.Domain.UseCases.Contract;

public class ContractService
{
	private readonly IContractRepository _contractRepository;
	private readonly UserService _userService;
	private readonly EquipmentService _equipmentService;
	private readonly IISAUnitOfWork _unitOfWork;
    public ContractService(IContractRepository contractRepository, UserService userService, EquipmentService equipmentService, IISAUnitOfWork unitOfWork)
	{
		_contractRepository = contractRepository;
		_userService = userService;
		_equipmentService = equipmentService;
		_unitOfWork = unitOfWork;
	}

	public async Task AddContract(DateTime deliveryDate, List<GeneralEquipment> equipment, Guid userId)
	{

        await _unitOfWork.StartTransactionAsync();
		var companyAdmin = await _userService.GetCompanyAdmin(userId);

		if(await _contractRepository.CompanyAlreadyHaveContract(companyAdmin.Company.Id) is true)
		{
            await DeleteOldContract(companyAdmin);
        }
        
        var newEquipments = await ParseEquipments(equipment);

        await CreateContract(newEquipments, deliveryDate, companyAdmin);
        await _unitOfWork.SaveAndCommitChangesAsync();

    }
	private async Task CreateContract(List<Equipment> newEquipments, DateTime deliveryDate, CompanyAdmin companyAdmin)
	{
        Entities.Delivery.Contract contract = new();
        contract.Id = Guid.NewGuid();

        List<ContractEquipment> contractEquipment = new();

        foreach (var equipmentItem in newEquipments)
        {
            contractEquipment.Add(new ContractEquipment(contract.Id, equipmentItem.Id, (int)equipmentItem.Quantity));
        }

        contract.DeliveryDate = deliveryDate;
        contract.CreatedAt = DateTime.Now.ToUniversalTime();
        contract.Equipments = contractEquipment;
        contract.Company = companyAdmin.Company;

        await _contractRepository.AddAsync(contract);
    }
    private async Task DeleteOldContract(CompanyAdmin companyAdmin)
    {
        var contract = await _contractRepository.GetContractByCompanyAsync(companyAdmin.Company.Id);
        foreach (var equipmentItem in contract.Equipments)
        {
            await _equipmentService.DeleteEquipment(equipmentItem.EquipmentId);
        }
        await _contractRepository.DeleteContract(contract.Id);
    }
    private async Task<List<Equipment>> ParseEquipments(List<GeneralEquipment> equipment)
    {
        List<Equipment> newEquipments = new();

        foreach (var equipmentItem in equipment)
        {
            Equipment newEquipment = await _equipmentService.GetById(equipmentItem.EquipmentId);
            newEquipment.Quantity = equipmentItem.Quantity;
            newEquipment.Id = Guid.NewGuid();
            newEquipments.Add(newEquipment);
            await _equipmentService.AddContractEquipmentAsync(newEquipment);
        }

        return newEquipments;
    }




}
