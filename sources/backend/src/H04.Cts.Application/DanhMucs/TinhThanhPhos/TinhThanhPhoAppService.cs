using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using H04.Cts.DanhMucs.TinhTHanhPhos;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using System.Linq.Dynamic.Core;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;


namespace H04.Cts.DanhMucs.TinhThanhPhos;

[Authorize(CtsPermissions.DanhMucs.TinhThanhPho)]
public class TinhThanhPhoAppService : ApplicationService
{
    private readonly IRepository<TinhThanhPho, Guid> _repository;

    public TinhThanhPhoAppService(IRepository<TinhThanhPho, Guid> repository)
    {
        _repository = repository;
    }

    // Get All
    public async Task<PagedResultDto<TinhThanhPho>> GetAllAsync(string? tenTinhThanhPho, PagedAndSortedResultRequestDto input)
    {
        try
        {
            var queryable = await _repository.GetQueryableAsync();
            var query = queryable;

            if (!string.IsNullOrEmpty(tenTinhThanhPho))
            {
                query = query.Where(x => x.TenTinhThanhPho.Contains(tenTinhThanhPho));
            }

            var totalCount = await AsyncExecuter.CountAsync(query);

            query = query.OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "TenTinhThanhPho" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var tinhs = await AsyncExecuter.ToListAsync(query);

            return new PagedResultDto<TinhThanhPho>(totalCount, tinhs);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while retrieving provinces.", ex);
        }

    }

    // get province by id
    public async Task<TinhThanhPho> GetAsync(Guid id)
    {
        try
        {
            return await _repository.GetAsync(id);
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving the province with ID {id}.", ex);
        }
    }

    // create a new province
    [Authorize(CtsPermissions.DanhMucs.TinhThanhPhoCreate)]
    public async Task<CreateProvinceDto> CreateAsync(CreateProvinceDto province)
    {
        try
        {
            if (province == null)
            {
                throw new ArgumentNullException(nameof(province), "Province data cannot be null.");
            }
            var is_exist_code = await _repository.AnyAsync(x => x.MaTinhThanhPho == province.MaTinhThanhPho);
            if (is_exist_code)
            {
                throw new Exception($"Province with code {province.MaTinhThanhPho} already exists.");
            }

            var newProvince = ObjectMapper.Map<CreateProvinceDto, TinhThanhPho>(province);
            await _repository.InsertAsync(newProvince);
            return ObjectMapper.Map<TinhThanhPho, CreateProvinceDto>(newProvince);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the province.", ex);
        }
    }

    // Create bulks provinces
    [Authorize(CtsPermissions.DanhMucs.TinhThanhPhoCreate)]
    public async Task<List<CreateProvinceDto>> CreateBulksAsync(List<CreateProvinceDto> provinces)
    {
        try
        {
            if (provinces == null || provinces.Count == 0)
            {
                throw new ArgumentNullException(nameof(provinces), "Province data cannot be null or empty.");
            }
            var duplicateCodes = provinces
            .GroupBy(p => p.MaTinhThanhPho?.Trim(), StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
            if (duplicateCodes.Any())
            {
                throw new Exception($"Duplicate codes found in input list: {string.Join(", ", duplicateCodes)}");
            }

            foreach (var province in provinces)
            {
                var is_exist_code = await _repository.AnyAsync(x => x.MaTinhThanhPho == province.MaTinhThanhPho);
                if (is_exist_code)
                {
                    throw new Exception($"Province with code {province.MaTinhThanhPho} already exists.");
                }
            }
            var newProvinces = ObjectMapper.Map<List<CreateProvinceDto>, List<TinhThanhPho>>(provinces);
            await _repository.InsertManyAsync(newProvinces);
            return ObjectMapper.Map<List<TinhThanhPho>, List<CreateProvinceDto>>(newProvinces);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the provinces.", ex);
        }
    }

    // update a province
    [Authorize(CtsPermissions.DanhMucs.TinhThanhPhoEdit)]
    public async Task<CreateProvinceDto> UpdateAsync(Guid id,CreateProvinceDto province)
    {
        try
        {
            if (province == null)
            {
                throw new ArgumentNullException(nameof(province), "Province data cannot be null.");
            }
            var existingProvince = await _repository.GetAsync(id);
            if (existingProvince == null)
            {
                throw new Exception($"Province with ID {id} does not exist.");
            }
            province.Id = id;

            var newProvince = ObjectMapper.Map(province, existingProvince);
            await _repository.UpdateAsync(existingProvince);
            return ObjectMapper.Map<TinhThanhPho, CreateProvinceDto>(newProvince);
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the province.", ex);
        }
    }

    // delete a province
    [Authorize(CtsPermissions.DanhMucs.TinhThanhPhoDelete)]
    public async Task DeleteAsync(Guid id)
    {
        try
        {
            var existingProvince = await _repository.GetAsync(id);
            if (existingProvince == null)
            {
                throw new Exception($"Province with ID {id} does not exist.");
            }
            await _repository.DeleteAsync(existingProvince);
            return;
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the province.", ex);
        }
    }
}

