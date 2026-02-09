import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';
import { ShopComponent } from './features/shop/shop.component';
import { ProductDetailsComponent } from './features/shop/product-details/product-details.component';
import { CreateProductComponent } from './features/shop/create-product/create-product.component';

export const routes: Routes = [
    {path: '',component:HomeComponent},
    {path: 'shop',component:ShopComponent},
    {path: 'shop/:id',component:ProductDetailsComponent},
    {path: 'sell',component: CreateProductComponent},
    {path: '**',redirectTo:'',pathMatch: 'full'}
];
