using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.Provinces;
public class ProvinceAppService : ApplicationService
{
    private readonly IRepository<Province, Guid> _repository;

    public ProvinceAppService(IRepository<Province, Guid> repository)
    {
        _repository = repository;
    }

    // get all provinces
    public async Task<List<Province>> GetAllAsync()
    {
        try
        {
            return await _repository.GetListAsync();
        }
        catch(Exception ex)
        {
            throw new Exception("An error occurred while retrieving provinces.", ex);
        }
            
    }

    // get province by id
    public async Task<Province> GetAsync(Guid id)
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
    public async Task<String> CreateAsync(CreateProvinceDto province)
    {
        try
        {
            if (province == null)
            {
                throw new ArgumentNullException(nameof(province), "Province data cannot be null.");
            }
            var is_exist_code = await _repository.AnyAsync(x => x.Code == province.Code);
            if (is_exist_code)
            {
                throw new Exception($"Province with code {province.Code} already exists.");
            }
            var is_exist_name = await _repository.AnyAsync(x => x.Name == province.Name);
            if (is_exist_name)
            {
                throw new Exception($"Province with name {province.Name} already exists.");
            }

            var newProvince = ObjectMapper.Map<CreateProvinceDto, Province>(province);
            await _repository.InsertAsync(newProvince);
            return "OK";
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the province.", ex);
        }
    }

    // Create bulks provinces
    public async Task<String> CreateBulksAsync(List<CreateProvinceDto> provinces)
    {
        try
        {
            if (provinces == null || provinces.Count == 0)
            {
                throw new ArgumentNullException(nameof(provinces), "Province data cannot be null or empty.");
            }
            var duplicateCodes = provinces
            .GroupBy(p => p.Code?.Trim(), StringComparer.OrdinalIgnoreCase)
            .Where(g => g.Count() > 1)
            .Select(g => g.Key)
            .ToList();
            if (duplicateCodes.Any())
            {
                throw new Exception($"Duplicate codes found in input list: {string.Join(", ", duplicateCodes)}");
            }

            var duplicateNames = provinces
                .GroupBy(p => p.Name?.Trim(), StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (duplicateNames.Any())
            {
                throw new Exception($"Duplicate names found in input list: {string.Join(", ", duplicateNames)}");
            }
            foreach (var province in provinces)
            {
                var is_exist_code = await _repository.AnyAsync(x => x.Code == province.Code);
                if (is_exist_code)
                {
                    throw new Exception($"Province with code {province.Code} already exists.");
                }
                var is_exist_name = await _repository.AnyAsync(x => x.Name == province.Name);
                if (is_exist_name)
                {
                    throw new Exception($"Province with name {province.Name} already exists.");
                }
            }
            var newProvinces = ObjectMapper.Map<List<CreateProvinceDto>, List<Province>>(provinces);
            await _repository.InsertManyAsync(newProvinces);
            return "OK";
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the provinces.", ex);
        }
    }

    // update a province
    public async Task<String> UpdateAsync(Guid id, CreateProvinceDto province)
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
            
            ObjectMapper.Map(province, existingProvince);
            await _repository.UpdateAsync(existingProvince);
            return "OK";
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the province.", ex);
        }
    }

    // delete a province
    public async Task<String> DeleteAsync(Guid id)
    {
        try
        {
            var existingProvince = await _repository.GetAsync(id);
            if (existingProvince == null)
            {
                throw new Exception($"Province with ID {id} does not exist.");
            }
            await _repository.DeleteAsync(existingProvince);
            return "OK";
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the province.", ex);
        }
    }

}

