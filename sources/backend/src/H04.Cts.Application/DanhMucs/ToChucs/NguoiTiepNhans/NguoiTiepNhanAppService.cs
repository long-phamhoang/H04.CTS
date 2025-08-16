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
            .WithDetailsAsync(x => x.OrganizationFk, x => x.NoiCapCCCDFk);
            
        var nguoiTiepNhan = await AsyncExecuter.FirstOrDefaultAsync(queryable, x => x.Id == id && !x.IsDeleted);

        if (nguoiTiepNhan == null)
        {
            throw new EntityNotFoundException(typeof(NguoiTiepNhan), id);
        }

        // Map to DTO using navigation properties
        return new NguoiTiepNhanDto
        {
            Id = nguoiTiepNhan.Id,
            OrganizationId = nguoiTiepNhan.OrganizationId,
            OrganizationName = nguoiTiepNhan.OrganizationFk?.TenToChuc ?? string.Empty,
            FullName = nguoiTiepNhan.FullName ?? string.Empty,
            CCCD = nguoiTiepNhan.CCCD ?? string.Empty,
            DateOfIssue = nguoiTiepNhan.DateOfIssue,
            NoiCapCCCDId = nguoiTiepNhan.NoiCapCCCDId ?? 0,
            NoiCapCCCDName = nguoiTiepNhan.NoiCapCCCDFk?.Name ?? string.Empty,
            Position = nguoiTiepNhan.Position ?? string.Empty,
            Phone = nguoiTiepNhan.Phone ?? string.Empty,
            Email = nguoiTiepNhan.Email ?? string.Empty,
            SubmissionAddress = nguoiTiepNhan.SubmissionAddress ?? string.Empty,
            Province = nguoiTiepNhan.Province ?? string.Empty,
            Ward = nguoiTiepNhan.Ward ?? string.Empty,
            IsDefault = nguoiTiepNhan.IsDefault,
            IsDeleted = nguoiTiepNhan.IsDeleted,
            DeletedBy = nguoiTiepNhan.DeletedBy ?? string.Empty,
            DeletedAt = nguoiTiepNhan.DeletedAt
        };
    }

    public async Task<PagedResultDto<NguoiTiepNhanDto>> GetListAsync(GetNguoiTiepNhanListDto input)
    {
        // Use WithDetailsAsync from the beginning to load related entities
        var queryable = await _repository
            .WithDetailsAsync(x => x.OrganizationFk, x => x.NoiCapCCCDFk);
            
        // Exclude soft-deleted records by default
        queryable = queryable.Where(x => !x.IsDeleted);

        // Only use keyword filter for all searchable fields
        if (!string.IsNullOrWhiteSpace(input.Keyword))
        {
            var keyword = input.Keyword.Trim();

            // Search in main fields and related entities using navigation properties
            queryable = queryable.Where(x =>
                (x.FullName != null && x.FullName.Contains(keyword)) ||
                (x.CCCD != null && x.CCCD.Contains(keyword)) ||
                (x.Position != null && x.Position.Contains(keyword)) ||
                (x.Phone != null && x.Phone.Contains(keyword)) ||
                (x.Email != null && x.Email.Contains(keyword)) ||
                (x.SubmissionAddress != null && x.SubmissionAddress.Contains(keyword)) ||
                (x.Province != null && x.Province.Contains(keyword)) ||
                (x.Ward != null && x.Ward.Contains(keyword)) ||
                // Search in related entities using navigation properties
                (x.OrganizationFk != null && !x.OrganizationFk.IsDeleted && x.OrganizationFk.TenToChuc != null && x.OrganizationFk.TenToChuc.Contains(keyword)) ||
                (x.NoiCapCCCDFk != null && !x.NoiCapCCCDFk.IsDeleted && x.NoiCapCCCDFk.Name != null && x.NoiCapCCCDFk.Name.Contains(keyword))
            );
        }

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
                    ordered = ordered.OrderBy(x => x.OrganizationFk != null ? x.OrganizationFk.TenToChuc : string.Empty);
                    break;
                case "organizationname desc":
                    ordered = ordered.OrderByDescending(x => x.OrganizationFk != null ? x.OrganizationFk.TenToChuc : string.Empty);
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

        // Map to DTOs using navigation properties
        var dtos = nguoiTiepNhans.Select(x => new NguoiTiepNhanDto
        {
            Id = x.Id,
            OrganizationId = x.OrganizationId,
            OrganizationName = x.OrganizationFk?.TenToChuc ?? string.Empty,
            FullName = x.FullName ?? string.Empty,
            CCCD = x.CCCD ?? string.Empty,
            DateOfIssue = x.DateOfIssue,
            NoiCapCCCDId = x.NoiCapCCCDId ?? 0,
            NoiCapCCCDName = x.NoiCapCCCDFk?.Name ?? string.Empty,
            Position = x.Position ?? string.Empty,
            Phone = x.Phone ?? string.Empty,
            Email = x.Email ?? string.Empty,
            SubmissionAddress = x.SubmissionAddress ?? string.Empty,
            Province = x.Province ?? string.Empty,
            Ward = x.Ward ?? string.Empty,
            IsDefault = x.IsDefault,
            IsDeleted = x.IsDeleted,
            DeletedBy = x.DeletedBy ?? string.Empty,
            DeletedAt = x.DeletedAt
        }).ToList();

        return new PagedResultDto<NguoiTiepNhanDto>(totalCount, dtos);
    }

    public async Task<NguoiTiepNhanDto> CreateAsync(CreateUpdateNguoiTiepNhanDto input)
    {
        // Optional validations when IDs are provided
        if (input.OrganizationId.HasValue)
        {
            var organizationExists = await _organizationRepository.AnyAsync(x => x.Id == input.OrganizationId.Value && !x.IsDeleted);
            if (!organizationExists)
            {
                throw new UserFriendlyException($"Không tìm thấy tổ chức với Id = {input.OrganizationId} hoặc tổ chức đã bị xóa.");
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
        await _repository.InsertAsync(nguoiTiepNhan);
        return ObjectMapper.Map<NguoiTiepNhan, NguoiTiepNhanDto>(nguoiTiepNhan);
    }

    public async Task<NguoiTiepNhanDto> UpdateAsync(long id, CreateUpdateNguoiTiepNhanDto input)
    {
        // Optional validations when IDs are provided
        if (input.OrganizationId.HasValue)
        {
            var organizationExists = await _organizationRepository.AnyAsync(x => x.Id == input.OrganizationId.Value && !x.IsDeleted);
            if (!organizationExists)
            {
                throw new UserFriendlyException($"Không tìm thấy tổ chức với Id = {input.OrganizationId} hoặc tổ chức đã bị xóa.");
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

        var nguoiTiepNhan = await _repository.GetAsync(id);
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
        await _repository.UpdateAsync(nguoiTiepNhan);
        return ObjectMapper.Map<NguoiTiepNhan, NguoiTiepNhanDto>(nguoiTiepNhan);
    }

    public async Task DeleteAsync(long id)
    {
        var nguoiTiepNhan = await _repository.GetAsync(id);
        if (nguoiTiepNhan.IsDeleted)
        {
            throw new UserFriendlyException("Bản ghi đã bị xóa trước đó.");
        }
        
        nguoiTiepNhan.IsDeleted = true;
        await _repository.UpdateAsync(nguoiTiepNhan);
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
