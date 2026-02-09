import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators, ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ShopService } from '../../../core/services/shop.service';

import { ProductType } from '../../../shared/models/product-type';
import { ProductBrand } from '../../../shared/models/product-brand';
import { ProductMake } from '../../../shared/models/product-make';
import { ProductModel } from '../../../shared/models/product-model';

import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { Observable, startWith, map } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, filter } from 'rxjs/operators';
@Component({
  selector: 'app-create-product',
  standalone: true,
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatAutocompleteModule
  ],
  templateUrl: './create-product.component.html',
  styleUrl: './create-product.component.scss',
})
export class CreateProductComponent implements OnInit {

  productForm!: FormGroup;

  types: ProductType[] = [];
  brands: ProductBrand[] = [];
  makes: ProductMake[] = [];
  models: ProductModel[] = [];
filteredLocations$!: Observable<string[]>;

  constructor(
    private fb: FormBuilder,
    private shopService: ShopService
  ) {}

  ngOnInit(): void {
    this.productForm = this.fb.group({
      name: ['', Validators.required],
      title: ['', Validators.required],
      description: ['', Validators.required],
      price: ['', Validators.required],
      year: ['', Validators.required],
      color: [''],
      kilometers: [''],

      ownerNumber: ['', Validators.required],
      fuelType: ['', Validators.required],
      transmissionType: ['', Validators.required],
      location: ['', Validators.required],

      type: [null, Validators.required],
      brand: [{ value: null, disabled: true }, Validators.required],
      make: [{ value: null, disabled: true }, Validators.required],
      model: [{ value: null, disabled: true }, Validators.required],

      images: [[]] // File[]
    });

    this.loadTypes();
    this.filteredLocations$ = this.productForm.get('location')!.valueChanges.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      filter(value => value && value.length >= 3),
      switchMap(value =>
        this.shopService.getLocations(value)
      )
    );
  }

  loadTypes() {
    this.shopService.getTypes().subscribe({
      next: types => this.types = types
    });
  }

  onTypeChange(type: ProductType) {
    if (!type) return;

    this.brands = [];
    this.makes = [];
    this.models = [];

    this.productForm.patchValue({
      brand: null,
      make: null,
      model: null
    });

    this.productForm.get('brand')?.disable();

    this.shopService.getBrandsByType(type.id).subscribe({
      next: brands => {
        this.brands = brands;
        this.productForm.get('brand')?.enable();
      }
    });
  }

  onBrandChange(brandId: number) {
    if (!brandId) return;

    this.makes = [];
    this.models = [];

    this.productForm.patchValue({
      make: null,
      model: null
    });

    this.productForm.get('make')?.disable();

    this.shopService.getMakesByBrand(brandId).subscribe({
      next: makes => {
        this.makes = makes;
        this.productForm.get('make')?.enable();
      }
    });
  }

  onMakeChange(makeId: number) {
    if (!makeId) return;

    this.models = [];
    this.productForm.patchValue({ model: null });
    this.productForm.get('model')?.disable();

    this.shopService.getModelsByMake(makeId).subscribe({
      next: models => {
        this.models = models;
        this.productForm.get('model')?.enable();
      }
    });
  }

  onFilesSelected(event: any) {
    const files: File[] = Array.from(event.target.files);

    if (files.length > 4) {
      alert('Maximum 4 images allowed');
      return;
    }

    this.productForm.patchValue({ images: files });
  }

  submit() {
  if (this.productForm.invalid) {
    this.productForm.markAllAsTouched();
    return;
  }

  const v = this.productForm.value;
  const fd = new FormData();

  fd.append('Name', v.name);
  fd.append('Title', v.title);
  fd.append('Description', v.description);
  fd.append('Price', v.price.toString());
  fd.append('Year', v.year.toString());

  if (v.color) fd.append('Color', v.color);
  if (v.kilometers) fd.append('kilometers', v.kilometers.toString());

  fd.append('OwnerNumber', v.ownerNumber.toString());
  fd.append('FuelType', v.fuelType);
  fd.append('TransmissionType', v.transmissionType);
  fd.append('Location', v.location);

  fd.append('TypeId', v.type.id.toString());
  fd.append('BrandId', v.brand.toString());
  fd.append('MakeId', v.make.toString());
  fd.append('ModelId', v.model.toString());

  fd.append('UserId', '1');

  v.images.forEach((file: File) => {
    fd.append('Images', file);
  });

  this.shopService.createProduct(fd).subscribe({
    next: () => alert('Product created successfully'),
    error: err => console.error('API Error:', err)
  });
}

}
