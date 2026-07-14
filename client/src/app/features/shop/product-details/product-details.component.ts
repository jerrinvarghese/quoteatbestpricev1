import { Component, inject, OnInit } from '@angular/core';
import { CommonModule, CurrencyPipe, DatePipe } from '@angular/common';
import { ShopService } from '../../../core/services/shop.service';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { Product } from '../../../shared/models/product';
import { MatButton } from '@angular/material/button';
import { MatIcon } from '@angular/material/icon';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatInput } from '@angular/material/input';
import { MatDivider } from '@angular/material/divider';

@Component({
  selector: 'app-product-details',
  standalone: true,
  imports: [
    CommonModule,
    CurrencyPipe,
    MatButton,
    MatIcon,
    MatFormField,
    MatInput,
    MatLabel,
    MatDivider,
    RouterModule,
    DatePipe
  ],
  templateUrl: './product-details.component.html',
  styleUrl: './product-details.component.scss',
})
export class ProductDetailsComponent implements OnInit {
private shopService=inject(ShopService);
private activatedRoute=inject(ActivatedRoute);
product?:Product;
 currentImageIndex = 0;

 get imageUrls(): string[] {
   if (!this.product) return [];
   return [
     this.product.imagePathOne,
     this.product.imagePathTwo,
     this.product.imagePathThree,
     this.product.imagePathFour,
     this.product.imagePathFive,
   ].filter(p => p && p.trim().length > 0) as string[];
 }

 prevImage() {
   const imgs = this.imageUrls;
   if (!imgs.length) return;
   this.currentImageIndex = (this.currentImageIndex - 1 + imgs.length) % imgs.length;
 }

 nextImage() {
   const imgs = this.imageUrls;
   if (!imgs.length) return;
   this.currentImageIndex = (this.currentImageIndex + 1) % imgs.length;
 }

 setImage(index: number) {
   const imgs = this.imageUrls;
   if (index < 0 || index >= imgs.length) return;
   this.currentImageIndex = index;
 }

ngOnInit() :void {
  this.loadProduct();
}

loadProduct(){
  const id=this.activatedRoute.snapshot.paramMap.get('id');
  if(!id) return;
  this.shopService.getProduct(+id).subscribe({
    next:product=>{
      console.log('Product response:', product); // ✅ log here
      console.table(product); // ✅ log in table format
      this.product=product;
      this.currentImageIndex = 0;
    },
    error:error=>console.log(error)
  })
}
}
