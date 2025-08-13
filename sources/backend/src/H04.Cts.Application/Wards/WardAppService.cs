using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using H04.Cts.Provinces;

namespace H04.Cts.Wards;
public class WardAppService : ApplicationService
{
    private readonly IRepository<Province, Guid> _provinceRepository;
    private readonly IRepository<Ward, Guid> _wardRepository;

    public WardAppService(IRepository<Province, Guid> provinceRepository, IRepository<Ward, Guid> wardRepository)
    {
        _provinceRepository = provinceRepository;
        _wardRepository = wardRepository;
    }

    // Get all wards by province ID
    public async Task<List<Ward>> GetWardsByProvinceIdAsync(Guid provinceId)
    {
        try
        {
            var wards = await _wardRepository.GetListAsync(x => x.ProvinceId == provinceId);
            return wards;
        }
        catch (Exception ex)
        {
            throw new Exception($"An error occurred while retrieving wards for province ID {provinceId}.", ex);
        }
    }

    // Get a ward by ID
    public async Task<Ward> GetWardByIdAsync(Guid id)
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
    public async Task<string> CreateWardAsync(WardDto wardDto)
    {
        try
        {
            if (wardDto == null)
            {
                throw new ArgumentNullException(nameof(wardDto), "Ward data cannot be null.");
            }
            // Check if the province exists
            var provinceExists = await _provinceRepository.AnyAsync(x => x.Id == wardDto.ProvinceId);
            if (!provinceExists)
            {
                throw new Exception($"Province with ID {wardDto.ProvinceId} does not exist.");
            }
            // Check if the ward code already exists
            var isExistCode = await _wardRepository.AnyAsync(x => x.Code == wardDto.Code);
            if (isExistCode)
            {
                throw new Exception($"Ward with code {wardDto.Code} already exists.");
            }
            var ward = ObjectMapper.Map<WardDto, Ward>(wardDto);
            await _wardRepository.InsertAsync(ward);
            return "OK";
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating the ward.", ex);
        }
    }

    // Create bulk wards
    public async Task<string> CreateBulkWardsAsync(List<WardDto> wardDtos)
    {
        try
        {
            if (wardDtos == null || !wardDtos.Any())
            {
                throw new ArgumentNullException(nameof(wardDtos), "Ward data cannot be null or empty.");
            }
            // Check for duplicate codes and names
            var duplicateCodes = wardDtos
                .GroupBy(w => w.Code?.Trim(), StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();
            if (duplicateCodes.Any())
            {
                throw new Exception($"Duplicate codes found in input list: {string.Join(", ", duplicateCodes)}");
            }
            var duplicateNames = wardDtos
                .GroupBy(w => w.Name?.Trim(), StringComparer.OrdinalIgnoreCase)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicateNames.Any())
            {
                throw new Exception($"Duplicate names found in input list: {string.Join(", ", duplicateNames)}");
            }

            foreach (var wardDto in wardDtos)
            {
                // Check if the province exists
                var provinceExists = await _provinceRepository.AnyAsync(x => x.Id == wardDto.ProvinceId);
                if (!provinceExists)
                {
                    throw new Exception($"Province with ID {wardDto.ProvinceId} does not exist.");
                }
                // Check if the ward code already exists
                var isExistCode = await _wardRepository.AnyAsync(x => x.Code == wardDto.Code);
                if (isExistCode)
                {
                    throw new Exception($"Ward with code {wardDto.Code} already exists.");
                }
                // Check if the ward name already exists
                var isExistName = await _wardRepository.AnyAsync(x => x.Name == wardDto.Name);
                if (isExistName)
                {
                    throw new Exception($"Ward with name {wardDto.Name} already exists.");
                }
            }
            var wards = ObjectMapper.Map<List<WardDto>, List<Ward>>(wardDtos);
            await _wardRepository.InsertManyAsync(wards);
            return "OK";
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while creating bulk wards.", ex);
        }
    }

    // Update an existing ward
    public async Task<string> UpdateWardAsync(Guid id, WardDto wardDto)
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
            var provinceExists = await _provinceRepository.AnyAsync(x => x.Id == wardDto.ProvinceId);
            if (!provinceExists)
            {
                throw new Exception($"Province with ID {wardDto.ProvinceId} does not exist.");
            }
            // Check if the ward code already exists
            var isExistCode = await _wardRepository.AnyAsync(x => x.Code == wardDto.Code && x.Id != id);
            if (isExistCode)
            {
                throw new Exception($"Ward with code {wardDto.Code} already exists.");
            }
            // Check if the ward name already exists
            var isExistName = await _wardRepository.AnyAsync(x => x.Name == wardDto.Name && x.Id != id);
            if (isExistName) 
            {
                throw new Exception($"Ward with name {wardDto.Name} already exists.");
            }
            ObjectMapper.Map(wardDto, existingWard);
            await _wardRepository.UpdateAsync(existingWard);
            return "OK";
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while updating the ward.", ex);
        }
    }

    // Delete a ward
    public async Task<string> DeleteWardAsync(Guid id)
    {
        try
        {
            var existingWard = await _wardRepository.GetAsync(id);
            if (existingWard == null)
            {
                throw new Exception($"Ward with ID {id} does not exist.");
            }
            await _wardRepository.DeleteAsync(existingWard);
            return "OK";
        }
        catch (Exception ex)
        {
            throw new Exception("An error occurred while deleting the ward.", ex);
        }
    }
}