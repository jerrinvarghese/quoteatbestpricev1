import { HttpClient, HttpParams } from '@angular/common/http';
import { inject,Injectable } from '@angular/core';
import { Product } from '../../shared/models/product';
import { Pagination } from '../../shared/models/pagination';
import { ShopParams } from '../../shared/models/shopParams';
import { ProductType } from '../../shared/models/product-type';
import { ProductBrand } from '../../shared/models/product-brand';
import { ProductMake } from '../../shared/models/product-make';
import { ProductModel } from '../../shared/models/product-model';

@Injectable({
  providedIn: 'root',
})
export class ShopService {
  baseUrl='https://localhost:5001/api/'
  private http=inject(HttpClient)
  types:string[]=[];
  brands:string[]=[];

  getProducts(shopParams:ShopParams) {
    let params=new HttpParams();

    if (shopParams.typeId)
    params = params.append('typeId', shopParams.typeId);

  if (shopParams.brandId)
    params = params.append('brandId', shopParams.brandId);

  if (shopParams.makeId)
    params = params.append('makeId', shopParams.makeId);

  if (shopParams.modelId)
    params = params.append('modelId', shopParams.modelId);

  if (shopParams.minKilometers)
    params = params.append('minKilometers', shopParams.minKilometers);

  if (shopParams.maxKilometers)
    params = params.append('maxKilometers', shopParams.maxKilometers);

  if (shopParams.minYear)
    params = params.append('minYear', shopParams.minYear);

  if (shopParams.maxYear)
    params = params.append('maxYear', shopParams.maxYear);

  if (shopParams.ownerNumber)
    params = params.append('ownerNumber', shopParams.ownerNumber);

  if (shopParams.transmissionType)
    params = params.append('transmissionType', shopParams.transmissionType);

    if(shopParams.sort){
      params=params.append('sort',shopParams.sort);
    }

    if(shopParams.search){
      params=params.append('search',shopParams.search);
    }

    params=params.append('pageSize',shopParams.pageSize);
    params=params.append('pageIndex',shopParams.pageNumber);
    return this.http.get<Pagination<Product>>(this.baseUrl+'products', { params });
  }

  getProduct(id:number){
    return this.http.get<Product>(this.baseUrl+'products/'+id);
  }

  getBrands(){
    if(this.brands.length>0) return;
    return this.http.get<string[]>(this.baseUrl+'products/brands').subscribe({
      next:response=>this.brands=response
    })
  }

  // getTypes(){
  //   if(this.types.length>0) return;
  //   return this.http.get<string[]>(this.baseUrl+'products/types').subscribe({
  //     next:response=>this.types=response
  //   })
  // }

getTypes() {
  return this.http.get<ProductType[]>(this.baseUrl + 'products/product-types');
}

getBrandsByType(typeId: number) {
  return this.http.get<ProductBrand[]>(
`${this.baseUrl}products/brands/${typeId}`
  );
}

getMakesByBrand(brandId: number) {
  return this.http.get<ProductMake[]>(
    `${this.baseUrl}products/makes/${brandId}`
  );
}

getModelsByMake(makeId: number) {
  return this.http.get<ProductModel[]>(
    `${this.baseUrl}products/models/${makeId}`
  );
}
}
