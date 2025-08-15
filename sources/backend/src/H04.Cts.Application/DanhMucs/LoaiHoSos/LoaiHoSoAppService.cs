using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using H04.Cts.DanhMucs.LoaiCTSs.Dtos;
using H04.Cts.DanhMucs.LoaiHoSos.Dtos;
using H04.Cts.DanhMucs.LoaiHoSos.Interfaces;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Entities;
using Volo.Abp.Domain.Repositories;

namespace H04.Cts.DanhMucs.LoaiHoSos
{
    [Authorize(CtsPermissions.DanhMucs.LoaiHoSo)]
    public class LoaiHoSoAppService : ApplicationService, ILoaiHoSoAppService
    {
        private readonly IRepository<LoaiHoSo, long> _repository;
        public LoaiHoSoAppService(IRepository<LoaiHoSo, long> repository)
        {
            _repository = repository;
        }

        [Authorize(CtsPermissions.DanhMucs.LoaiHoSoCreate)]
        public async Task<LoaiHoSoDto> CreateAsync(CreateUpdateLoaiHoSoDto input)
        {
            var responseInserted = await _repository.InsertAsync(ObjectMapper.Map<CreateUpdateLoaiHoSoDto, LoaiHoSo>(input));
            return ObjectMapper.Map<LoaiHoSo, LoaiHoSoDto>(responseInserted);
        }

        [Authorize(CtsPermissions.DanhMucs.LoaiHoSoDelete)]
        public async Task DeleteAsync(long id)
        {
            var entity = await _repository.GetAsync(id);
            if (entity == null)
            {
                throw new EntityNotFoundException(typeof(LoaiHoSo), id);
            }
            await _repository.HardDeleteAsync(entity);
        }

        [Authorize(CtsPermissions.DanhMucs.LoaiHoSoDelete)]
        [HttpDelete("/api/app/loai-ho-so/soft-delete/{id}")]
        public async Task SoftDeleteAsync(long id)
        {
            if (await _repository.GetAsync(id) == null)
            {
                throw new EntityNotFoundException(typeof(LoaiCTS), id);
            }
            await _repository.DeleteAsync(id);
        }

        public async Task<LoaiHoSoDto> GetAsync(long id)
        {
            return ObjectMapper.Map<LoaiHoSo, LoaiHoSoDto>(await _repository.GetAsync(id));
        }

        public async Task<PagedResultDto<LoaiHoSoDto>> GetListAsync(PagedAndSortedResultRequestDto input)
        {
            var queryable = await _repository.GetQueryableAsync();
            var query = queryable
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "MaLoaiHoSo" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var documentTypes = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            return new PagedResultDto<LoaiHoSoDto>
            (
                totalCount,
                ObjectMapper.Map<List<LoaiHoSo>, List<LoaiHoSoDto>>(documentTypes)
            );
        }

        public async Task<PagedResultDto<LoaiHoSoDto>> GetListFilterData(LoaiCTSFilterDto input)
        {
            var queryable = await _repository.GetQueryableAsync();
            if (!string.IsNullOrWhiteSpace(input.Keyword))
            {
                input.Keyword = input.Keyword.Trim().ToLower();
                queryable = queryable.Where(x =>
                    x.MaLoaiHoSo.ToLower().Contains(input.Keyword)
                    || x.TenLoaiHoSo.ToLower().Contains(input.Keyword));
            }
            var query = queryable
                .OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "MaLoaiHoSo" : input.Sorting)
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount);

            var documentTypes = await AsyncExecuter.ToListAsync(query);
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            return new PagedResultDto<LoaiHoSoDto>
            (
                totalCount,
                ObjectMapper.Map<List<LoaiHoSo>, List<LoaiHoSoDto>>(documentTypes)
            );
        }

        [Authorize(CtsPermissions.DanhMucs.LoaiHoSoEdit)]
        public async Task<LoaiHoSoDto> UpdateAsync(long id, CreateUpdateLoaiHoSoDto input)
        {
            var documentType = await _repository.GetAsync(id);
            if (documentType == null)
            {
                throw new EntityNotFoundException(typeof(LoaiHoSo), id);
            }
            documentType.TenLoaiHoSo = input.TenLoaiHoSo;
            documentType.TrangThai = input.TrangThai;
            documentType.GhiChu = input.GhiChu;
            var responseUpdated = await _repository.UpdateAsync(documentType);
            return ObjectMapper.Map<LoaiHoSo, LoaiHoSoDto>(documentType);
        }
    }
}
