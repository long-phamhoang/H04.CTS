using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using H04.Cts.DanhMucs.LoaiCTSs.Dtos;
using H04.Cts.DanhMucs.LoaiCTSs.Interfaces;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.DanhMucs.LoaiCTSs
{
    [Authorize(CtsPermissions.DanhMucs.LoaiCTS)]
    public class LoaiCTSAppService : ApplicationService, ILoaiCTSAppService
    {
        private readonly IRepository<LoaiCTS, long> _repository;
        public LoaiCTSAppService(IRepository<LoaiCTS, long> repository)
        {
            _repository = repository;
        }

        [Authorize(CtsPermissions.DanhMucs.LoaiCTSCreate)]
        public async Task<LoaiCTSDto> CreateAsync(CreateUpdateLoaiCTS input)
        {
            var responseInserted = await _repository.InsertAsync(ObjectMapper.Map<CreateUpdateLoaiCTS, LoaiCTS>(input));
            return ObjectMapper.Map<LoaiCTS, LoaiCTSDto>(responseInserted);
        }

        [Authorize(CtsPermissions.DanhMucs.LoaiCTSDelete)]
        public async Task DeleteAsync(long id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(LoaiCTS), id);
            }
            await _repository.HardDeleteAsync(entity);
        }

        public async Task<LoaiCTSDto> GetAsync(long id)
        {
            return ObjectMapper.Map<LoaiCTS, LoaiCTSDto>(await _repository.GetAsync(id));
        }

        public async Task<PagedResultDto<LoaiCTSDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _repository.GetQueryableAsync();
            var query = queryable
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "MaLoaiCTS" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var digitalTypes = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);
            return new PagedResultDto<LoaiCTSDto>(
                totalCount,
                ObjectMapper.Map<List<LoaiCTS>, List<LoaiCTSDto>>(digitalTypes)
            );
        }

        public async Task<PagedResultDto<LoaiCTSDto>> GetListFilterData(LoaiCTSFilterDto input)
        {
            var queryable = await _repository.GetQueryableAsync();
            if(!string.IsNullOrWhiteSpace(input.Keyword))
            {
                input.Keyword = input.Keyword.Trim().ToLower();
                queryable = queryable.Where(x =>
                    x.MaLoaiCTS.ToLower().Contains(input.Keyword)
                    || x.TenLoaiCTS.ToLower().Contains(input.Keyword));
            }
            var query = queryable
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "MaLoaiCTS" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var digitalTypes = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);
            return new PagedResultDto<LoaiCTSDto>(
                totalCount,
                ObjectMapper.Map<List<LoaiCTS>, List<LoaiCTSDto>>(digitalTypes)
            );
        }

        public async Task<bool> IsExistsMaLoaiCTS(string maLoaiCTS, long? id = 0)
        {
            return await _repository.AnyAsync(x => x.MaLoaiCTS.ToLower().Trim() == maLoaiCTS.ToLower().Trim() && x.Id != id);
        }

        [Authorize(CtsPermissions.DanhMucs.LoaiCTSDelete)]
        [HttpDelete("/api/app/loai-cts/soft-delete/{id}")]
        public async Task SoftDeleteAsync(long id)
        {
            if(await _repository.GetAsync(id) == null)
            {
                throw new EntityNotFoundException(typeof(LoaiCTS), id);
            }
            await _repository.DeleteAsync(id);
        }

        [Authorize(CtsPermissions.DanhMucs.LoaiCTSEdit)]
        public async Task<LoaiCTSDto> UpdateAsync(long id, CreateUpdateLoaiCTS input)
        {
            var digitalType = await _repository.GetAsync(id);
            if(digitalType == null)
            {
                throw new EntityNotFoundException(typeof(LoaiCTS), id);
            }
            digitalType.TenLoaiCTS = input.TenLoaiCTS;
            digitalType.TrangThai = input.TrangThai;
            digitalType.GhiChu = input.GhiChu;
            await _repository.UpdateAsync(digitalType);
            return ObjectMapper.Map<LoaiCTS, LoaiCTSDto>(digitalType);
        }
    }
}
