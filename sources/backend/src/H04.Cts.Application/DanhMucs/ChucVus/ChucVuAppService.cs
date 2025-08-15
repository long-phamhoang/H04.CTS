using H04.Cts.DanhMucs.ChucVucs.Dtos;
using H04.Cts.DanhMucs.ChucVucs.Interfaces;
using H04.Cts.Dtos.DanhMucs;
using H04.Cts.Entities.DanhMucs;
using H04.Cts.Permissions;
using Microsoft.AspNetCore.Authorization;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;
using Volo.Abp.Application.Dtos;
using Volo.Abp.Application.Services;
using Volo.Abp.Domain.Repositories;
using Microsoft.EntityFrameworkCore;

namespace H04.Cts.DanhMucs.ChucVus
{
    [Authorize(CtsPermissions.DanhMucs.ChucVu)]
    public class ChucVuAppService : ApplicationService, IChucVuAppService
    {
        private readonly IRepository<ChucVu, long> _repository;

        public ChucVuAppService(IRepository<ChucVu, long> repository)
        {
            _repository = repository;
        }

        public async Task<ChucVuDto> GetAsync(long id)
        {
            var book = await _repository.GetAsync(id);
            return ObjectMapper.Map<ChucVu, ChucVuDto>(book);
        }

        public async Task<PagedResultDto<ChucVuDto>> GetListAsync(GetListChucVuInput input)
        {
            var queryable = await _repository.GetQueryableAsync();

            // filter
            if (!string.IsNullOrWhiteSpace(input.FilterInput))
            {
                queryable = queryable.Where(x =>
                    x.TenChucVu.ToLower().Contains(input.FilterInput.ToLower()) ||
                    x.MaChucVu.ToLower().Contains(input.FilterInput.ToLower()));
            }

            // Sắp xếp
            queryable = queryable.OrderBy(input.Sorting.IsNullOrWhiteSpace() ? "TenChucVu" : input.Sorting);

            // Lấy tổng số bản ghi sau khi filter
            var totalCount = await AsyncExecuter.CountAsync(queryable);

            // Lấy dữ liệu theo phân trang
            var items = await AsyncExecuter.ToListAsync(queryable
                .Skip(input.SkipCount)
                .Take(input.MaxResultCount));

            return new PagedResultDto<ChucVuDto>(
                totalCount,
                ObjectMapper.Map<List<ChucVu>, List<ChucVuDto>>(items)
            );
        }

        [Authorize(CtsPermissions.DanhMucs.ChucVuCreate)]
        public async Task<ChucVuDto> CreateAsync(CreateUpdateChucVuDto input)
        {
            var book = ObjectMapper.Map<CreateUpdateChucVuDto, ChucVu>(input);
            await _repository.InsertAsync(book);
            return ObjectMapper.Map<ChucVu, ChucVuDto>(book);
        }

        [Authorize(CtsPermissions.DanhMucs.ChucVuEdit)]
        public async Task<ChucVuDto> UpdateAsync(long id, CreateUpdateChucVuDto input)
        {
            var book = await _repository.GetAsync(id);
            ObjectMapper.Map(input, book);
            await _repository.UpdateAsync(book);
            return ObjectMapper.Map<ChucVu, ChucVuDto>(book);
        }

        [Authorize(CtsPermissions.DanhMucs.ChucVuDelete)]
        public async Task SoftDeleteAsync(long id)
        {
            await _repository.DeleteAsync(id);
        }

        [Authorize(CtsPermissions.DanhMucs.ChucVuDelete)]
        public async Task DeleteAsync(long id)
        {
            var entity = await _repository.GetAsync(id);
            await _repository.HardDeleteAsync(entity);
        }

        [AllowAnonymous]
        public async Task<bool> IsMaChucVuUniqueAsync(string maChucVu)
        {
            if (string.IsNullOrWhiteSpace(maChucVu))
                return false;

            maChucVu = maChucVu.Trim().ToUpper();

            var queryable = await _repository.GetQueryableAsync();

            var list = await queryable
                .Where(e => e.MaChucVu.ToUpper() == maChucVu)
                .ToListAsync(); // Dùng ToListAsync thay vì AnyAsync

            return !list.Any();
        }

        [AllowAnonymous]
        public async Task<List<ChucVuDto>> GetChucVuForDropDown()
        {
            var queryable = await _repository.GetQueryableAsync();


            return queryable
                .Where(x => x.TrangThai == Utilities.TrangThai.HoatDong && !x.IsDeleted)
                .Select(x => new ChucVuDto
                {
                    Id = x.Id,
                    TenChucVu = x.TenChucVu
                }).ToList();
        }
    }
}