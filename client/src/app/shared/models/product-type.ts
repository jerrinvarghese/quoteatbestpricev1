
import { ProductBrand } from './product-brand';


export interface ProductType {
  id: number;
  typeName: string;
  typeDescription: string;
  isActive: boolean;
  productBrands: any[];
}
 