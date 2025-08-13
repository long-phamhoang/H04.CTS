export enum TrangThai {
  HoatDong = 1,
  KhongHoatDong = 2,
}

export const trangThaiOptions = [
  { value: TrangThai.HoatDong, key: 'Hoạt động' },
  { value: TrangThai.KhongHoatDong, key: 'Không hoạt động' },
];

export function getTrangThaiLabel(value: TrangThai | number | null | undefined): string {
  const opt = trangThaiOptions.find(o => o.value === value);
  return opt ? opt.key : '';
}
