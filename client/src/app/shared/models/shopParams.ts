export class ShopParams {
  pageNumber = 1;
  pageSize = 10;

  typeId?: number;
  brandId?: number;
  makeId?: number;
  modelId?: number;

  minKilometers?: number;
  maxKilometers?: number;

  minYear?: number;
  maxYear?: number;

  minPrice?: number;
  maxPrice?: number;

  ownerNumber?: number;
  transmissionType?: string;

  sort = '';
  search = '';
}
