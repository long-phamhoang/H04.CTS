using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using Microsoft.AspNetCore.Authorization;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using Volo.Abp.Domain.Entities;
using Volo.Abp;
using Volo.Abp.Linq;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Dtos.DanhMucs;

namespace H04.Cts.Application.DanhMucs;

public class NguoiTiepNhanAppService : ApplicationService, INguoiTiepNhanAppService
{
    private readonly IRepository<NguoiTiepNhan, long> _repository;
    private readonly IRepository<ToChuc, long> _organizationRepository;
        private readonly IRepository<NoiCapCCCD, long> _noiCapCCCDRepository;

        public NguoiTiepNhanAppService(IRepository<NguoiTiepNhan, long> repository, IRepository<ToChuc, long> organizationRepository, IRepository<NoiCapCCCD, long> noiCapCCCDRepository)
    {
        _repository = repository;
        _organizationRepository = organizationRepository;
        _noiCapCCCDRepository = noiCapCCCDRepository;
    }

    public async Task<NguoiTiepNhanDto> GetAsync(long id)
    {
        var queryable = await _repository
            .WithDetailsAsync(x => x.Organizations, x => x.NoiCapCCCDFk);
            
        var nguoiTiepNhan = await AsyncExecuter.FirstOrDefaultAsync(queryable, x => x.Id == id && !x.IsDeleted);

        if (nguoiTiepNhan == null)
        {
            throw new EntityNotFoundException(typeof(NguoiTiepNhan), id);
        }

        // Map to DTO using navigation properties
        return ObjectMapper.Map<NguoiTiepNhan, NguoiTiepNhanDto>(nguoiTiepNhan);
    }

    public async Task<PagedResultDto<NguoiTiepNhanDto>> GetListAsync(GetNguoiTiepNhanListDto input)
    {
        // Use WithDetailsAsync from the beginning to load related entities
        var queryable = await _repository
            .WithDetailsAsync(x => x.Organizations, x => x.NoiCapCCCDFk);
            
        // Exclude soft-deleted records by default
        queryable = queryable.Where(x => !x.IsDeleted);

        // Only use keyword filter for all searchable fields
        if (!string.IsNullOrWhiteSpace(input.Keyword))
        {
            var keyword = input.Keyword.Trim().ToLower();

            // Search in main fields and related entities using navigation properties (case-insensitive)
            queryable = queryable.Where(x =>
                (x.FullName != null && x.FullName.ToLower().Contains(keyword)) ||
                (x.CCCD != null && x.CCCD.ToLower().Contains(keyword)) ||
                (x.Position != null && x.Position.ToLower().Contains(keyword)) ||
                (x.Phone != null && x.Phone.ToLower().Contains(keyword)) ||
                (x.Email != null && x.Email.ToLower().Contains(keyword)) ||
                (x.SubmissionAddress != null && x.SubmissionAddress.ToLower().Contains(keyword)) ||
                (x.Province != null && x.Province.ToLower().Contains(keyword)) ||
                (x.Ward != null && x.Ward.ToLower().Contains(keyword)) ||
                // Search in related entities using navigation properties
                (x.Organizations.Any(o => !o.IsDeleted && o.TenToChuc != null && o.TenToChuc.ToLower().Contains(keyword))) ||
                (x.NoiCapCCCDFk != null && !x.NoiCapCCCDFk.IsDeleted && x.NoiCapCCCDFk.Name != null && x.NoiCapCCCDFk.Name.ToLower().Contains(keyword))
            );
        }

        // Per-field filters (concise)
        queryable = queryable
            .WhereIf(!string.IsNullOrWhiteSpace(input.FullName), x => x.FullName != null && x.FullName.ToLower().Contains(input.FullName.Trim().ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(input.CCCD), x => x.CCCD != null && x.CCCD.ToLower().Contains(input.CCCD.Trim().ToLower()))
            .WhereIf(input.DateOfIssue.HasValue, x => x.DateOfIssue == input.DateOfIssue.Value)
            .WhereIf(input.NoiCapCCCDId.HasValue, x => x.NoiCapCCCDId == input.NoiCapCCCDId.Value)
            .WhereIf(!string.IsNullOrWhiteSpace(input.Position), x => x.Position != null && x.Position.ToLower().Contains(input.Position.Trim().ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(input.Phone), x => x.Phone != null && x.Phone.ToLower().Contains(input.Phone.Trim().ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(input.Email), x => x.Email != null && x.Email.ToLower().Contains(input.Email.Trim().ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(input.SubmissionAddress), x => x.SubmissionAddress != null && x.SubmissionAddress.ToLower().Contains(input.SubmissionAddress.Trim().ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(input.Province), x => x.Province != null && x.Province.ToLower().Contains(input.Province.Trim().ToLower()))
            .WhereIf(!string.IsNullOrWhiteSpace(input.Ward), x => x.Ward != null && x.Ward.ToLower().Contains(input.Ward.Trim().ToLower()))
            .WhereIf(input.IsDefault.HasValue, x => x.IsDefault == input.IsDefault.Value)
            .WhereIf(!string.IsNullOrWhiteSpace(input.DeletedBy), x => x.DeletedBy != null && x.DeletedBy.ToLower().Contains(input.DeletedBy.Trim().ToLower()))
            .WhereIf(input.DeletedAt.HasValue, x => x.DeletedAt == input.DeletedAt.Value);

        // Filter by organization ids if provided (concise)
        var orgIdSet = input.OrganizationIds?.Distinct().ToArray();
        queryable = queryable.WhereIf(orgIdSet != null && orgIdSet.Length > 0,
            x => x.Organizations.Any(o => orgIdSet.Contains(o.Id)));

        // Apply pagination
        var totalCount = await AsyncExecuter.CountAsync(queryable);
        var ordered = queryable
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        // Handle sorting for joined fields
        if (!string.IsNullOrWhiteSpace(input.Sorting))
        {
            switch (input.Sorting.ToLower())
            {
                case "organizationname":
                    ordered = ordered.OrderBy(x => x.Organizations.Select(o => o.TenToChuc ?? string.Empty).FirstOrDefault());
                    break;
                case "organizationname desc":
                    ordered = ordered.OrderByDescending(x => x.Organizations.Select(o => o.TenToChuc ?? string.Empty).FirstOrDefault());
                    break;
                case "noicapcccdname":
                    ordered = ordered.OrderBy(x => x.NoiCapCCCDFk != null ? x.NoiCapCCCDFk.Name : string.Empty);
                    break;
                case "noicapcccdname desc":
                    ordered = ordered.OrderByDescending(x => x.NoiCapCCCDFk != null ? x.NoiCapCCCDFk.Name : string.Empty);
                    break;
                default:
                    // For other fields, use the original sorting
                    ordered = ordered.OrderBy(input.Sorting);
                    break;
            }
        }
        else
        {
            // Default sorting
            ordered = ordered.OrderBy(x => x.FullName ?? string.Empty);
        }

        // Execute the query
        var nguoiTiepNhans = await AsyncExecuter.ToListAsync(ordered);
        var dtos = nguoiTiepNhans.Select(x => ObjectMapper.Map<NguoiTiepNhan, NguoiTiepNhanDto>(x)).ToList();

        return new PagedResultDto<NguoiTiepNhanDto>(totalCount, dtos);
    }

    public async Task<NguoiTiepNhanDto> CreateAsync(CreateUpdateNguoiTiepNhanDto input)
    {
        // Validate organizations if provided
        if (input.OrganizationIds != null && input.OrganizationIds.Length > 0)
        {
            var validCount = await _organizationRepository.CountAsync(x => input.OrganizationIds.Contains(x.Id) && !x.IsDeleted);
            if (validCount != input.OrganizationIds.Distinct().Count())
            {
                throw new UserFriendlyException("Một hoặc nhiều tổ chức không tồn tại hoặc đã bị xóa.");
            }
        }

        if (input.NoiCapCCCDId.HasValue)
        {
            var noiCapCCCDExists = await _noiCapCCCDRepository.AnyAsync(x => x.Id == input.NoiCapCCCDId.Value && !x.IsDeleted);
            if (!noiCapCCCDExists)
            {
                throw new UserFriendlyException($"Không tìm thấy cơ quan cấp với Id = {input.NoiCapCCCDId} hoặc cơ quan đã bị xóa.");
            }
        }

        var normalizedCccd = input.CCCD?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(normalizedCccd))
        {
            throw new UserFriendlyException("CCCD không được để trống.");
        }

        var exists = await _repository.AnyAsync(x => x.CCCD == normalizedCccd && !x.IsDeleted);
        if (exists)
        {
            throw new UserFriendlyException($"CCCD '{normalizedCccd}' đã tồn tại.");
        }

        var nguoiTiepNhan = ObjectMapper.Map<CreateUpdateNguoiTiepNhanDto, NguoiTiepNhan>(input);
        nguoiTiepNhan.CCCD = normalizedCccd;
        // Set many-to-many organizations
        if (input.OrganizationIds != null && input.OrganizationIds.Length > 0)
        {
            var organizations = await _organizationRepository.GetListAsync(x => input.OrganizationIds.Contains(x.Id) && !x.IsDeleted);
            foreach (var org in organizations)
            {
                nguoiTiepNhan.Organizations.Add(org);
            }
        }
        await _repository.InsertAsync(nguoiTiepNhan);
        return ObjectMapper.Map<NguoiTiepNhan, NguoiTiepNhanDto>(nguoiTiepNhan);
    }

    public async Task<NguoiTiepNhanDto> UpdateAsync(long id, CreateUpdateNguoiTiepNhanDto input)
    {
        // Validate organizations if provided
        if (input.OrganizationIds != null && input.OrganizationIds.Length > 0)
        {
            var validCount = await _organizationRepository.CountAsync(x => input.OrganizationIds.Contains(x.Id) && !x.IsDeleted);
            if (validCount != input.OrganizationIds.Distinct().Count())
            {
                throw new UserFriendlyException("Một hoặc nhiều tổ chức không tồn tại hoặc đã bị xóa.");
            }
        }

        if (input.NoiCapCCCDId.HasValue)
        {
            var noiCapCCCDExists = await _noiCapCCCDRepository.AnyAsync(x => x.Id == input.NoiCapCCCDId.Value && !x.IsDeleted);
            if (!noiCapCCCDExists)
            {
                throw new UserFriendlyException($"Không tìm thấy cơ quan cấp với Id = {input.NoiCapCCCDId} hoặc cơ quan đã bị xóa.");
            }
        }

        var queryable = await _repository.WithDetailsAsync(x => x.Organizations);
        var nguoiTiepNhan = await AsyncExecuter.FirstOrDefaultAsync(queryable, x => x.Id == id);
        var normalizedCccd = input.CCCD?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(normalizedCccd))
        {
            throw new UserFriendlyException("CCCD không được để trống.");
        }

        var duplicate = await _repository.AnyAsync(x => x.Id != id && x.CCCD == normalizedCccd && !x.IsDeleted);
        if (duplicate)
        {
            throw new UserFriendlyException($"CCCD '{normalizedCccd}' đã tồn tại.");
        }

        ObjectMapper.Map(input, nguoiTiepNhan);
        nguoiTiepNhan.CCCD = normalizedCccd;
        
        // Update many-to-many organizations
        // Ensure Organizations collection is initialized
        if (nguoiTiepNhan.Organizations == null)
        {
            nguoiTiepNhan.Organizations = new List<ToChuc>();
        }
        
        // Clear all existing relationships first
        nguoiTiepNhan.Organizations.Clear();
        
        // Add new relationships
        if (input.OrganizationIds != null && input.OrganizationIds.Length > 0)
        {
            var organizations = await _organizationRepository.GetListAsync(x => input.OrganizationIds.Contains(x.Id) && !x.IsDeleted);
            foreach (var org in organizations)
            {
                nguoiTiepNhan.Organizations.Add(org);
            }
        }
        
        // Save changes to update the many-to-many relationships
        await _repository.UpdateAsync(nguoiTiepNhan);
        
        // Force Entity Framework to track changes properly
        await CurrentUnitOfWork.SaveChangesAsync();
        return ObjectMapper.Map<NguoiTiepNhan, NguoiTiepNhanDto>(nguoiTiepNhan);
    }

    public async Task DeleteAsync(long id)
    {
        await _repository.DeleteAsync(id);
    }

    public async Task DeleteManyAsync(long[] ids)
    {
        if (ids == null || ids.Length == 0)
        {
            return;
        }
        foreach (var id in ids.Distinct())
        {
            await _repository.DeleteAsync(id);
        }
    }

    public async Task<bool> CheckExistAsync(CheckExistDto input)
    {
        if (string.IsNullOrWhiteSpace(input?.Field) || string.IsNullOrWhiteSpace(input?.Value))
        {
            return false;
        }

        var queryable = await _repository.GetQueryableAsync();
        
        // Exclude soft-deleted records
        queryable = queryable.Where(x => !x.IsDeleted);
        
        // Sử dụng switch case thay vì reflection để tương thích với EF Core
        var result = input.Field.ToLower() switch
        {
            "cccd" => await AsyncExecuter.AnyAsync(queryable, x => x.CCCD == input.Value),
            "phone" => await AsyncExecuter.AnyAsync(queryable, x => x.Phone == input.Value),
            "email" => await AsyncExecuter.AnyAsync(queryable, x => x.Email == input.Value),
            _ => false // Trả về false nếu field không hợp lệ
        };
        
        return result;
    }
}
