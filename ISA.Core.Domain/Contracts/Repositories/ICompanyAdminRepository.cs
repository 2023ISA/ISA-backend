﻿using ISA.Core.Domain.Entities.User;

namespace ISA.Core.Domain.Contracts.Repositories;

public interface ICompanyAdminRepository
{
    Task AddAsync(CompanyAdmin companyAdmin);
    Task<CompanyAdmin?> GetByIdAsync(Guid id);
    void UpdateAndSaveChanges(CompanyAdmin companyAdmin);
    void Update(CompanyAdmin companyAdmin);
}
