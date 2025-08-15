using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
                       join org in orgQueryable on x.OrganizationId equals org.Id into orgJoin
                       from org in orgJoin.DefaultIfEmpty()
                       join issuing in noiCapQueryable on x.NoiCapCCCDId equals issuing.Id into issuingJoin
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

        // Keyword filter across short text fields (<=256)
        if (!string.IsNullOrWhiteSpace(input.Keyword))
        {
            var keyword = input.Keyword.Trim();

            queryable = queryable.Where(x =>
                (x.FullName != null && x.FullName.Contains(keyword)) ||
                (x.CCCD != null && x.CCCD.Contains(keyword)) ||
                (x.Position != null && x.Position.Contains(keyword)) ||
                (x.Phone != null && x.Phone.Contains(keyword)) ||
                (x.Email != null && x.Email.Contains(keyword)) ||
                (x.SubmissionAddress != null && x.SubmissionAddress.Contains(keyword)) ||
                (x.Province != null && x.Province.Contains(keyword)) ||
                (x.Ward != null && x.Ward.Contains(keyword))
            );
        }

        // Filters
        if (input.OrganizationId.HasValue)
        {
            queryable = queryable.Where(x => x.OrganizationId == input.OrganizationId);
        }

        if (!string.IsNullOrWhiteSpace(input.FullName))
        {
            queryable = queryable.Where(x => x.FullName.Contains(input.FullName));
        }

        if (!string.IsNullOrWhiteSpace(input.CCCD))
        {
            queryable = queryable.Where(x => x.CCCD == input.CCCD);
        }

        if (input.DateOfIssue.HasValue)
        {
            var date = input.DateOfIssue.Value.Date;
            queryable = queryable.Where(x => x.DateOfIssue.Date == date);
        }

        if (input.NoiCapCCCDId.HasValue)
        {
            queryable = queryable.Where(x => x.NoiCapCCCDId == input.NoiCapCCCDId.Value);
        }

        if (!string.IsNullOrWhiteSpace(input.Position))
        {
            queryable = queryable.Where(x => x.Position.Contains(input.Position));
        }

        if (!string.IsNullOrWhiteSpace(input.Phone))
        {
            queryable = queryable.Where(x => x.Phone.Contains(input.Phone));
        }

        if (!string.IsNullOrWhiteSpace(input.Email))
        {
            queryable = queryable.Where(x => x.Email != null && x.Email.Contains(input.Email));
        }

        if (!string.IsNullOrWhiteSpace(input.SubmissionAddress))
        {
            queryable = queryable.Where(x => x.SubmissionAddress.Contains(input.SubmissionAddress));
        }

        if (!string.IsNullOrWhiteSpace(input.Province))
        {
            queryable = queryable.Where(x => x.Province.Contains(input.Province));
        }

        if (!string.IsNullOrWhiteSpace(input.Ward))
        {
            queryable = queryable.Where(x => x.Ward != null && x.Ward.Contains(input.Ward));
        }

        if (input.IsDefault.HasValue)
        {
            queryable = queryable.Where(x => x.IsDefault == input.IsDefault.Value);
        }

        // Intentionally not exposing deleted records in list

        var ordered = queryable
            .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "FullName" : input.Sorting)
            .Skip(input.SkipCount)
            .Take(input.MaxResultCount);

        var orgQueryable = await _organizationRepository.GetQueryableAsync();
        var noiCapQueryable = await _noiCapCCCDRepository.GetQueryableAsync();

        var dtoQuery = from x in ordered
                       join org in orgQueryable on x.OrganizationId equals org.Id into orgJoin
                       from org in orgJoin.DefaultIfEmpty()
                       join issuing in noiCapQueryable on x.NoiCapCCCDId equals issuing.Id into issuingJoin
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

        var nguoiTiepNhans = await AsyncExecuter.ToListAsync(dtoQuery);
        var totalCount = await AsyncExecuter.CountAsync(queryable);

        return new PagedResultDto<NguoiTiepNhanDto>(totalCount, nguoiTiepNhans);
    }

    public async Task<NguoiTiepNhanDto> CreateAsync(CreateUpdateNguoiTiepNhanDto input)
    {
        // Optional validations when IDs are provided
        if (input.OrganizationId.HasValue)
        {
            var organizationExists = await _organizationRepository.AnyAsync(x => x.Id == input.OrganizationId.Value);
            if (!organizationExists)
            {
                throw new UserFriendlyException($"Không tìm thấy tổ chức với Id = {input.OrganizationId}.");
            }
        }

        if (input.NoiCapCCCDId.HasValue)
        {
            var noiCapCCCDExists = await _noiCapCCCDRepository.AnyAsync(x => x.Id == input.NoiCapCCCDId.Value);
            if (!noiCapCCCDExists)
            {
                throw new UserFriendlyException($"Không tìm thấy cơ quan cấp với Id = {input.NoiCapCCCDId}.");
            }
        }

        var normalizedCccd = input.CCCD?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(normalizedCccd))
        {
            throw new UserFriendlyException("CCCD không được để trống.");
        }

        var exists = await _repository.AnyAsync(x => x.CCCD == normalizedCccd);
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
            var organizationExists = await _organizationRepository.AnyAsync(x => x.Id == input.OrganizationId.Value);
            if (!organizationExists)
            {
                throw new UserFriendlyException($"Không tìm thấy tổ chức với Id = {input.OrganizationId}.");
            }
        }

        if (input.NoiCapCCCDId.HasValue)
        {
            var noiCapCCCDExists = await _noiCapCCCDRepository.AnyAsync(x => x.Id == input.NoiCapCCCDId.Value);
            if (!noiCapCCCDExists)
            {
                throw new UserFriendlyException($"Không tìm thấy cơ quan cấp với Id = {input.NoiCapCCCDId}.");
            }
        }

        var nguoiTiepNhan = await _repository.GetAsync(id);
        var normalizedCccd = input.CCCD?.Trim() ?? string.Empty;
        if (string.IsNullOrWhiteSpace(normalizedCccd))
        {
            throw new UserFriendlyException("CCCD không được để trống.");
        }

        var duplicate = await _repository.AnyAsync(x => x.Id != id && x.CCCD == normalizedCccd);
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
        nguoiTiepNhan.IsDeleted = true;
        await _repository.UpdateAsync(nguoiTiepNhan);
    }
}
