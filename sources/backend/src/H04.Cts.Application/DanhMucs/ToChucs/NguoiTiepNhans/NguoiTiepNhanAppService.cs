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
        var nguoiTiepNhanQueryable = await _repository.GetQueryableAsync();
        var orgQueryable = await _organizationRepository.GetQueryableAsync();
        var noiCapQueryable = await _noiCapCCCDRepository.GetQueryableAsync();

        var dtoQuery = from x in nguoiTiepNhanQueryable
                       join org in orgQueryable.Where(o => !o.IsDeleted) on x.OrganizationId equals org.Id into orgJoin
                       from org in orgJoin.DefaultIfEmpty()
                       join issuing in noiCapQueryable.Where(n => !n.IsDeleted) on x.NoiCapCCCDId equals issuing.Id into issuingJoin
                       from issuing in issuingJoin.DefaultIfEmpty()
                       where x.Id == id && !x.IsDeleted
                       select new NguoiTiepNhanDto
                       {
                           Id = x.Id,
                           OrganizationId = x.OrganizationId,
                           OrganizationName = org != null ? org.TenToChuc : null,
                           FullName = x.FullName,
                           CCCD = x.CCCD,
                           DateOfIssue = x.DateOfIssue,
                           NoiCapCCCDId = x.NoiCapCCCDId ?? 0,
                           NoiCapCCCDName = issuing != null ? issuing.Name : null,
                           Position = x.Position,
                           Phone = x.Phone,
                           Email = x.Email,
                           SubmissionAddress = x.SubmissionAddress,
                           Province = x.Province,
                           Ward = x.Ward,
                           IsDefault = x.IsDefault,
                           IsDeleted = x.IsDeleted,
                           DeletedBy = x.DeletedBy,
                           DeletedAt = x.DeletedAt
                       };

        var dto = await AsyncExecuter.FirstOrDefaultAsync(dtoQuery);
        if (dto == null)
        {
            throw new EntityNotFoundException(typeof(NguoiTiepNhan), id);
        }
        return dto;
    }

    public async Task<PagedResultDto<NguoiTiepNhanDto>> GetListAsync(GetNguoiTiepNhanListDto input)
    {
        var queryable = await _repository.GetQueryableAsync();
        // Exclude soft-deleted records by default
        queryable = queryable.Where(x => !x.IsDeleted);

        // Only use keyword filter for all searchable fields
        if (!string.IsNullOrWhiteSpace(input.Keyword))
        {
            var keyword = input.Keyword.Trim();

            // Search in related tables by name fields
            var searchOrgQueryable = await _organizationRepository.GetQueryableAsync();
            var searchNoiCapQueryable = await _noiCapCCCDRepository.GetQueryableAsync();

            var matchingOrgIds = searchOrgQueryable
                .Where(org => !org.IsDeleted && org.TenToChuc != null && org.TenToChuc.ToLower().Contains(keyword.ToLower()))
                .Select(org => org.Id)
                .ToList();

            var matchingNoiCapIds = searchNoiCapQueryable
                .Where(issuing => !issuing.IsDeleted && issuing.Name != null && issuing.Name.ToLower().Contains(keyword.ToLower()))
                .Select(issuing => issuing.Id)
                .ToList();

            // Search in main fields
            var mainFieldQueryable = queryable.Where(x =>
                (x.FullName != null && x.FullName.Contains(keyword)) ||
                (x.CCCD != null && x.CCCD.Contains(keyword)) ||
                (x.Position != null && x.Position.Contains(keyword)) ||
                (x.Phone != null && x.Phone.Contains(keyword)) ||
                (x.Email != null && x.Email.Contains(keyword)) ||
                (x.SubmissionAddress != null && x.SubmissionAddress.Contains(keyword)) ||
                (x.Province != null && x.Province.Contains(keyword)) ||
                (x.Ward != null && x.Ward.Contains(keyword))
            );

            // Combine search results
            var combinedIds = mainFieldQueryable.Select(x => x.Id)
                .Union(queryable.Where(x => 
                    (x.OrganizationId.HasValue && matchingOrgIds.Contains(x.OrganizationId.Value)) ||
                    (x.NoiCapCCCDId.HasValue && matchingNoiCapIds.Contains(x.NoiCapCCCDId.Value))
                ).Select(x => x.Id))
                .ToList();

            // Apply the combined search
            queryable = queryable.Where(x => combinedIds.Contains(x.Id));
        }

        var ordered = queryable
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var orgQueryable = await _organizationRepository.GetQueryableAsync();
        var noiCapQueryable = await _noiCapCCCDRepository.GetQueryableAsync();

        var dtoQuery = from x in ordered
                       join org in orgQueryable.Where(o => !o.IsDeleted) on x.OrganizationId equals org.Id into orgJoin
                       from org in orgJoin.DefaultIfEmpty()
                       join issuing in noiCapQueryable.Where(n => !n.IsDeleted) on x.NoiCapCCCDId equals issuing.Id into issuingJoin
                       from issuing in issuingJoin.DefaultIfEmpty()
                       select new NguoiTiepNhanDto
                       {
                           Id = x.Id,
                           OrganizationId = x.OrganizationId,
                           OrganizationName = org != null ? org.TenToChuc : null,
                           FullName = x.FullName,
                           CCCD = x.CCCD,
                           DateOfIssue = x.DateOfIssue,
                           NoiCapCCCDId = x.NoiCapCCCDId ?? 0,
                           NoiCapCCCDName = issuing != null ? issuing.Name : null,
                           Position = x.Position,
                           Phone = x.Phone,
                           Email = x.Email,
                           SubmissionAddress = x.SubmissionAddress,
                           Province = x.Province,
                           Ward = x.Ward,
                           IsDefault = x.IsDefault,
                           IsDeleted = x.IsDeleted,
                           DeletedBy = x.DeletedBy,
                           DeletedAt = x.DeletedAt
                       };

        // Handle sorting for joined fields after the join
        if (!string.IsNullOrWhiteSpace(input.Sorting))
        {
            switch (input.Sorting.ToLower())
            {
                case "organizationname":
                    dtoQuery = dtoQuery.OrderBy(x => x.OrganizationName ?? string.Empty);
                    break;
                case "organizationname desc":
                    dtoQuery = dtoQuery.OrderByDescending(x => x.OrganizationName ?? string.Empty);
                    break;
                case "noicapcccdname":
                    dtoQuery = dtoQuery.OrderBy(x => x.NoiCapCCCDName ?? string.Empty);
                    break;
                case "noicapcccdname desc":
                    dtoQuery = dtoQuery.OrderByDescending(x => x.NoiCapCCCDName ?? string.Empty);
                    break;
                default:
                    // For other fields, use the original sorting
                    dtoQuery = dtoQuery.OrderBy(input.Sorting);
                    break;
            }
        }
        else
        {
            // Default sorting
            dtoQuery = dtoQuery.OrderBy(x => x.FullName ?? string.Empty);
        }

        var nguoiTiepNhans = await AsyncExecuter.ToListAsync(dtoQuery);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<NguoiTiepNhanDto>(totalCount, nguoiTiepNhans);
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
