// mock-location.ts
export interface Province {
  id: number;
  name: string;
  communes: { id: number; name: string }[];
}

export const PROVINCES_MOCK: Province[] = [
  {
    id: 1,
    name: 'TP. Hà Nội',
    communes: [
      { id: 11001, name: 'Ba Đình' },
      { id: 11002, name: 'Hoàn Kiếm' },
      { id: 11003, name: 'Cầu Giấy' },
      { id: 11004, name: 'Đống Đa' },
      { id: 11005, name: 'Hai Bà Trưng' },
      { id: 11006, name: 'Hoàng Mai' },
      { id: 11007, name: 'Tây Hồ' },
      { id: 11008, name: 'Thanh Xuân' },
      { id: 11009, name: 'Nam Từ Liêm' },
      { id: 11010, name: 'Bắc Từ Liêm' },
      { id: 11011, name: 'Hà Đông' },
      { id: 11012, name: 'Long Biên' },
      { id: 11013, name: 'Sơn Tây' },
      { id: 11014, name: 'Ba Vì' },
      { id: 11015, name: 'Chương Mỹ' },
      { id: 11016, name: 'Đan Phượng' },
      { id: 11017, name: 'Đông Anh' },
      { id: 11018, name: 'Gia Lâm' },
      { id: 11019, name: 'Hoài Đức' },
      { id: 11020, name: 'Mê Linh' },
      { id: 11021, name: 'Mỹ Đức' },
      { id: 11022, name: 'Phú Xuyên' },
      { id: 11023, name: 'Phúc Thọ' },
      { id: 11024, name: 'Quốc Oai' },
      { id: 11025, name: 'Sóc Sơn' },
      { id: 11026, name: 'Thạch Thất' },
      { id: 11027, name: 'Thanh Oai' },
      { id: 11028, name: 'Thanh Trì' },
      { id: 11029, name: 'Thường Tín' },
      { id: 11030, name: 'Ứng Hòa' }
    ]
  },
  {
    id: 2,
    name: 'TP. Hồ Chí Minh',
    communes: [
      { id: 21001, name: 'Quận 1' },
      { id: 21002, name: 'Quận 3' },
      { id: 21003, name: 'Quận 4' },
      { id: 21004, name: 'Quận 5' },
      { id: 21005, name: 'Quận 6' },
      { id: 21006, name: 'Quận 7' },
      { id: 21007, name: 'Quận 8' },
      { id: 21008, name: 'Quận 10' },
      { id: 21009, name: 'Quận 11' },
      { id: 21010, name: 'Quận 12' },
      { id: 21011, name: 'Tân Bình' },
      { id: 21012, name: 'Tân Phú' },
      { id: 21013, name: 'Bình Tân' },
      { id: 21014, name: 'Bình Thạnh' },
      { id: 21015, name: 'Gò Vấp' },
      { id: 21016, name: 'Hóc Môn' },
      { id: 21017, name: 'Củ Chi' },
      { id: 21018, name: 'Nhà Bè' },
      { id: 21019, name: 'Thủ Đức' }
    ]
  },
  {
    id: 3,
    name: 'TP. Cần Thơ',
    communes: [
      { id: 31001, name: 'Ninh Kiều' },
      { id: 31002, name: 'Bình Thủy' },
      { id: 31003, name: 'Cái Răng' },
      { id: 31004, name: 'Ô Môn' },
      { id: 31005, name: 'Thốt Nốt' }
    ]
  },
  {
    id: 4,
    name: 'An Giang',
    communes: [
      { id: 41001, name: 'Châu Đốc' },
      { id: 41002, name: 'Long Xuyên' },
      { id: 41003, name: 'Tân Châu' },
      { id: 41004, name: 'Châu Phú' },
      { id: 41005, name: 'Thoại Sơn' }
    ]
  },
  {
    id: 5,
    name: 'Bà Rịa - Vũng Tàu',
    communes: [
      { id: 51001, name: 'Vũng Tàu' },
      { id: 51002, name: 'Bà Rịa' },
      { id: 51003, name: 'Châu Đức' },
      { id: 51004, name: 'Xuyên Mộc' }
    ]
  },
  {
    id: 6,
    name: 'Bắc Giang',
    communes: [
      { id: 61001, name: 'Bắc Giang' },
      { id: 61002, name: 'Yên Thế' },
      { id: 61003, name: 'Lạng Giang' }
    ]
  },
  {
    id: 7,
    name: 'Bắc Kạn',
    communes: [
      { id: 71001, name: 'Bắc Kạn' },
      { id: 71002, name: 'Ba Bể' },
      { id: 71003, name: 'Pác Nặm' }
    ]
  },
  {
    id: 8,
    name: 'Bạc Liêu',
    communes: [
      { id: 81001, name: 'Bạc Liêu' },
      { id: 81002, name: 'Hồng Dân' },
      { id: 81003, name: 'Phước Long' }
    ]
  },
  {
    id: 9,
    name: 'Bắc Ninh',
    communes: [
      { id: 91001, name: 'Bắc Ninh' },
      { id: 91002, name: 'Yên Phong' },
      { id: 91003, name: 'Quế Võ' }
    ]
  },
  {
    id: 10,
    name: 'Bến Tre',
    communes: [
      { id: 101001, name: 'Bến Tre' },
      { id: 101002, name: 'Ba Tri' },
      { id: 101003, name: 'Bình Đại' }
    ]
  },
  {
    id: 11,
    name: 'Bình Định',
    communes: [
      { id: 111001, name: 'Quy Nhơn' },
      { id: 111002, name: 'An Nhơn' },
      { id: 111003, name: 'Tuy Phước' }
    ]
  },
  {
    id: 12,
    name: 'Bình Dương',
    communes: [
      { id: 121001, name: 'Thủ Dầu Một' },
      { id: 121002, name: 'Dĩ An' },
      { id: 121003, name: 'Thuận An' }
    ]
  },
  {
    id: 13,
    name: 'Bình Phước',
    communes: [
      { id: 131001, name: 'Đồng Xoài' },
      { id: 131002, name: 'Bình Long' },
      { id: 131003, name: 'Phước Long' }
    ]
  },
  {
    id: 14,
    name: 'Bình Thuận',
    communes: [
      { id: 141001, name: 'Phan Thiết' },
      { id: 141002, name: 'La Gi' },
      { id: 141003, name: 'Hàm Thuận Bắc' }
    ]
  },
  {
    id: 15,
    name: 'Cà Mau',
    communes: [
      { id: 151001, name: 'Cà Mau' },
      { id: 151002, name: 'Đầm Dơi' },
      { id: 151003, name: 'Năm Căn' }
    ]
  },
  {
    id: 16,
    name: 'Cao Bằng',
    communes: [
      { id: 161001, name: 'Cao Bằng' },
      { id: 161002, name: 'Hòa An' },
      { id: 161003, name: 'Quảng Hòa' }
    ]
  },
  {
    id: 17,
    name: 'Đắk Lắk',
    communes: [
      { id: 171001, name: 'Buôn Ma Thuột' },
      { id: 171002, name: 'Buôn Đôn' },
      { id: 171003, name: 'Ea H\'leo' }
    ]
  },
  {
    id: 18,
    name: 'Đắk Nông',
    communes: [
      { id: 181001, name: 'Gia Nghĩa' },
      { id: 181002, name: 'Đắk Glong' },
      { id: 181003, name: 'Tuy Đức' }
    ]
  },
  {
    id: 19,
    name: 'Điện Biên',
    communes: [
      { id: 191001, name: 'Điện Biên Phủ' },
      { id: 191002, name: 'Mường Chà' },
      { id: 191003, name: 'Mường Lay' }
    ]
  },
  {
    id: 20,
    name: 'Đồng Nai',
    communes: [
      { id: 201001, name: 'Biên Hòa' },
      { id: 201002, name: 'Long Khánh' },
      { id: 201003, name: 'Trảng Bom' }
    ]
  },
  {
    id: 21,
    name: 'Đồng Tháp',
    communes: [
      { id: 211001, name: 'Cao Lãnh' },
      { id: 211002, name: 'Sa Đéc' },
      { id: 211003, name: 'Hồng Ngự' }
    ]
  },
  {
    id: 22,
    name: 'Gia Lai',
    communes: [
      { id: 221001, name: 'Pleiku' },
      { id: 221002, name: 'An Khê' },
      { id: 221003, name: 'Ayun Pa' }
    ]
  },
  {
    id: 23,
    name: 'Hà Giang',
    communes: [
      { id: 231001, name: 'Hà Giang' },
      { id: 231002, name: 'Đồng Văn' },
      { id: 231003, name: 'Mèo Vạc' }
    ]
  },
  {
    id: 24,
    name: 'Hà Nam',
    communes: [
      { id: 241001, name: 'Phủ Lý' },
      { id: 241002, name: 'Bình Lục' },
      { id: 241003, name: 'Lý Nhân' }
    ]
  },
  {
    id: 25,
    name: 'Hà Tĩnh',
    communes: [
      { id: 251001, name: 'Hà Tĩnh' },
      { id: 251002, name: 'Hồng Lĩnh' },
      { id: 251003, name: 'Cẩm Xuyên' }
    ]
  },
  {
    id: 26,
    name: 'Hải Dương',
    communes: [
      { id: 261001, name: 'Hải Dương' },
      { id: 261002, name: 'Chí Linh' },
      { id: 261003, name: 'Kinh Môn' }
    ]
  },
  {
    id: 27,
    name: 'Hậu Giang',
    communes: [
      { id: 271001, name: 'Vị Thanh' },
      { id: 271002, name: 'Ngã Bảy' },
      { id: 271003, name: 'Châu Thành A' }
    ]
  },
  {
    id: 28,
    name: 'Hòa Bình',
    communes: [
      { id: 281001, name: 'Hòa Bình' },
      { id: 281002, name: 'Mai Châu' },
      { id: 281003, name: 'Lương Sơn' }
    ]
  },
  {
    id: 29,
    name: 'Hưng Yên',
    communes: [
      { id: 291001, name: 'Hưng Yên' },
      { id: 291002, name: 'Văn Lâm' },
      { id: 291003, name: 'Khoái Châu' }
    ]
  },
  {
    id: 30,
    name: 'Khánh Hòa',
    communes: [
      { id: 301001, name: 'Nha Trang' },
      { id: 301002, name: 'Cam Ranh' },
      { id: 301003, name: 'Diên Khánh' }
    ]
  },
  {
    id: 31,
    name: 'Kiên Giang',
    communes: [
      { id: 311001, name: 'Rạch Giá' },
      { id: 311002, name: 'Hà Tiên' },
      { id: 311003, name: 'Phú Quốc' }
    ]
  },
  {
    id: 32,
    name: 'Kon Tum',
    communes: [
      { id: 321001, name: 'Kon Tum' },
      { id: 321002, name: 'Đăk Glei' },
      { id: 321003, name: 'Ngọc Hồi' }
    ]
  },
  {
    id: 33,
    name: 'Lai Châu',
    communes: [
      { id: 331001, name: 'Lai Châu' },
      { id: 331002, name: 'Tam Đường' },
      { id: 331003, name: 'Sìn Hồ' }
    ]
  },
  {
    id: 34,
    name: 'Lâm Đồng',
    communes: [
      { id: 341001, name: 'Đà Lạt' },
      { id: 341002, name: 'Bảo Lộc' },
      { id: 341003, name: 'Đạ Huoai' }
    ]
  },
  {
    id: 35,
    name: 'Lạng Sơn',
    communes: [
      { id: 351001, name: 'Lạng Sơn' },
      { id: 351002, name: 'Bình Gia' },
      { id: 351003, name: 'Cao Lộc' }
    ]
  },
  {
    id: 36,
    name: 'Lào Cai',
    communes: [
      { id: 361001, name: 'Lào Cai' },
      { id: 361002, name: 'Bát Xát' },
      { id: 361003, name: 'Sa Pa' },
      { id: 361004, name: 'Yên Bái' }
    ]
  }
];
