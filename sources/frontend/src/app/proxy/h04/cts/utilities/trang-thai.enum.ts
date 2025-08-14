import { mapEnumToOptions } from '@abp/ng.core';

export enum TrangThai {
  HoatDong = 1,
  KhongHoatDong = 2,
}

export const trangThaiOptions = mapEnumToOptions(TrangThai);
