using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using System.Buffers;

namespace H04.Cts.DanhMucs.XaPhuongs;

[Authorize(CtsPermissions.DanhMucs.XaPhuong)]
public class XaPhuongAppService : ApplicationService
{
    private readonly IRepository<TinhThanhPho, Guid> _provinceRepository;
    private readonly IRepository<XaPhuong, Guid> _wardRepository;

    public XaPhuongAppService(IRepository<TinhThanhPho, Guid> provinceRepository, IRepository<XaPhuong, Guid> wardRepository)
    {
        _provinceRepository = provinceRepository;
        _wardRepository = wardRepository;
    }

    // Get all XaPhuong
    public async Task<PagedResultDto<XaPhuong>> GetAll(Guid? TinhThanhPhoId, string? tenXaPhuong, PagedAndSortedResultRequestDto input)
    {
        try
        {
            var queryable = await _wardRepository.GetQueryableAsync();
            var query = queryable;

            if (!string.IsNullOrEmpty(tenXaPhuong))
            {
                query = query.Where(x => x.TenXaPhuong.Contains(tenXaPhuong));
            }

            if (TinhThanhPhoId.HasValue)
            {
                query = query.Where(x => x.TinhThanhPhoId == TinhThanhPhoId.Value);
            }

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = query
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "TenXaPhuong" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var xaPhuongs = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<XaPhuong>(totalCount, xaPhuongs);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving wards.", ex);
        }
    }

    // Get a ward by ID
    public async Task<XaPhuong> GetWardByIdAsync(Guid id)
    {
        try
        {
            return await _wardRepository.GetAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving the ward with ID {id}.", ex);
        }
    }

    // Create a new ward
    [Authorize(CtsPermissions.DanhMucs.XaPhuongCreate)]
    public async Task<WardDto> CreateWardAsync(WardDto wardDto)
    {
        try
        {
            if (wardDto == null)
            {
                throw new ArgumentNullException(nameof(wardDto), "Ward data cannot be null.");
            }
            // Check if the province exists
            var provinceExists = await _provinceRepository.AnyAsync(x => x.Id == wardDto.TinhThanhPhoId);
            if (!provinceExists)
            {
                throw new Exception($"Province with ID {wardDto.TinhThanhPhoId} does not exist.");
            }
            // Check if the ward code already exists
            var isExistCode = await _wardRepository.AnyAsync(x => x.MaXaPhuong == wardDto.MaXaPhuong);
            if (isExistCode)
            {
                throw new Exception($"Ward with code {wardDto.MaXaPhuong} already exists.");
            }
            var ward = ObjectMapper.Map<WardDto, XaPhuong>(wardDto);
            await _wardRepository.InsertAsync(ward);
            return ObjectMapper.Map<XaPhuong,WardDto>(ward); ;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the ward.", ex);
        }
    }

    // Create bulk wards
    [Authorize(CtsPermissions.DanhMucs.XaPhuongCreate)]
    public async Task<List<WardDto>> CreateBulkWardsAsync(List<WardDto> wardDtos)
    {
        try
        {
            if (wardDtos == null || !wardDtos.Any())
            {
                throw new ArgumentNullException(nameof(wardDtos), "Ward data cannot be null or empty.");
            }
            // Check for duplicate codes and names
            var duplicateCodes = wardDtos
                .GroupBy(w => w.MaXaPhuong?.Trim(), StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (duplicateCodes.Any())
            {
                throw new Exception($"Duplicate codes found in input list: {string.Join(", ", duplicateCodes)}");
            }
            foreach (var wardDto in wardDtos)
            {
                // Check if the province exists
                var provinceExists = await _provinceRepository.AnyAsync(x => x.Id == wardDto.TinhThanhPhoId);
                if (!provinceExists)
                {
                    throw new Exception($"Province with ID {wardDto.TinhThanhPhoId} does not exist.");
                }
                // Check if the ward code already exists
                var isExistCode = await _wardRepository.AnyAsync(x => x.MaXaPhuong == wardDto.MaXaPhuong);
                if (isExistCode)
                {
                    throw new Exception($"Ward with code {wardDto.MaXaPhuong} already exists.");
                }
            }
            var wards = ObjectMapper.Map<List<WardDto>, List<XaPhuong>>(wardDtos);
            await _wardRepository.InsertManyAsync(wards);
            return ObjectMapper.Map<List<XaPhuong>, List<WardDto>>(wards); ;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating bulk wards.", ex);
        }
    }

    // Update an existing ward
    [Authorize(CtsPermissions.DanhMucs.XaPhuongEdit)]
    public async Task<WardDto> UpdateWardAsync(Guid id, WardDto wardDto)
    {
        try
        {
            if (wardDto == null)
            {
                throw new ArgumentNullException(nameof(wardDto), "Ward data cannot be null.");
            }
            var existingWard = await _wardRepository.GetAsync(id);
            if (existingWard == null)
            {
                throw new Exception($"Ward with ID {id} does not exist.");
            }
            // Check if the province exists
            var provinceExists = await _provinceRepository.AnyAsync(x => x.Id == wardDto.TinhThanhPhoId);
            if (!provinceExists)
            {
                throw new Exception($"Province with ID {wardDto.TinhThanhPhoId} does not exist.");
            }
            // Check if the ward code already exists
            var isExistCode = await _wardRepository.AnyAsync(x => x.MaXaPhuong == wardDto.MaXaPhuong && x.Id != id);
            if (isExistCode)
            {
                throw new Exception($"Ward with code {wardDto.MaXaPhuong} already exists.");
            }
            wardDto.Id = id;
            var newXaPhuong = ObjectMapper.Map(wardDto, existingWard);
            await _wardRepository.UpdateAsync(existingWard);
            return ObjectMapper.Map<XaPhuong, WardDto>(newXaPhuong);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the ward.", ex);
        } 
    }

    // Delete a ward
    [Authorize(CtsPermissions.DanhMucs.XaPhuongDelete)]
    public async Task DeleteWardAsync(Guid id)
    {
        try
        {
            var existingWard = await _wardRepository.GetAsync(id);
            if (existingWard == null)
            {
                throw new Exception($"Ward with ID {id} does not exist.");
            }
            await _wardRepository.DeleteAsync(existingWard);
            return;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the ward.", ex);
        }
    }
}