// import { Component,inject } from '@angular/core';
 import { ShopService } from '../../../core/services/shop.service';
 import { MatDivider } from '@angular/material/divider';
 import {MatListOption, MatSelectionList} from '@angular/material/list';
 import { MatButton } from '@angular/material/button';
// import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
 import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ProductType } from '../../../shared/models/product-type';
import { ProductBrand } from '../../../shared/models/product-brand';
import { ProductMake } from '../../../shared/models/product-make';
import { ProductModel } from '../../../shared/models/product-model';


// @Component({
//   selector: 'app-filters-dialog',
//   imports: [
//     MatDivider,
//     MatSelectionList,
//     MatListOption,
//     MatButton,
//     FormsModule
//   ],
//   templateUrl: './filters-dialog.component.html',
//   styleUrls: ['./filters-dialog.component.scss']
// })
// export class FiltersDialogComponent {
//   shopService = inject(ShopService);
//   private dialog = inject(MatDialogRef<FiltersDialogComponent>);
//   data = inject(MAT_DIALOG_DATA);

//   selectedBrands: string[] = this.data.selectedBrands;
//   selectedTypes: string[] = this.data.selectedTypes;

//   applyFilters() {
//     this.dialog.close({
//       selectedBrands: this.selectedBrands,
//       selectedTypes: this.selectedTypes
//     });
//   }
// }

import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup } from '@angular/forms';
import { MatFormField, MatLabel } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { CommonModule } from '@angular/common';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatDialogRef } from '@angular/material/dialog';
import { ShopParams } from '../../../shared/models/shopParams';
@Component({
  selector: 'app-filters-dialog',
  imports: [
     MatDivider,
    MatSelectionList,
     MatListOption,
    MatButton,
    FormsModule,
    MatFormField,
    MatLabel,
    MatSelectModule,
    MatOptionModule,
    ReactiveFormsModule,
    CommonModule,
    MatFormFieldModule,
    MatInputModule
  ],
  templateUrl: './filters-dialog.component.html',
  styleUrls: ['./filters-dialog.component.scss']
})
export class FiltersDialogComponent implements OnInit {
  filtersForm!: FormGroup;

  types: ProductType[] = [];  
  brands: ProductBrand[] = [];
  makes: ProductMake[] = [];
  models: ProductModel[] = [];
  years: number[] = [];

  constructor(private fb: FormBuilder, 
    private shopService: ShopService,
  private dialogRef: MatDialogRef<FiltersDialogComponent>) {}

  ngOnInit() {
   this.filtersForm = this.fb.group({
  type: [null],
  brand: [{ value: null, disabled: true }],
  make: [{ value: null, disabled: true }],
  model: [{ value: null, disabled: true }],
  minKilometers: [''],
  maxKilometers: [''],
  minYear: [''],
  maxYear: [''],
  ownerNumber: [''],
  transmissionType: ['']
});


    this.shopService.getTypes().subscribe({
  next: (response) => {
    console.log('Types API response:', response);
    this.types = response;}
});
    this.years = this.getYearsList();
  }

  onTypeChange(selectedType: ProductType) {
  if (!selectedType) return;

  this.brands = [];
  this.makes = [];
  this.models = [];

  this.filtersForm.patchValue({
    brand: null,
    make: null,
    model: null
  });

  this.filtersForm.get('brand')?.disable();

  this.shopService.getBrandsByType(selectedType.id).subscribe({
    next: (brands) => {
      console.log('Brands loaded:', brands);
      this.brands = brands;

      if (brands.length > 0) {
        this.filtersForm.get('brand')?.enable();
      }
    },
    error: (err) => {
      console.error('Error loading brands', err);
    }
  });
}


  onBrandChange(brandId: number) {
  if (!brandId) return;

  // Reset dependent dropdowns
  this.makes = [];
  this.models = [];

  this.filtersForm.patchValue({
    make: null,
    model: null
  });

  this.filtersForm.get('make')?.disable();

  this.shopService.getMakesByBrand(brandId).subscribe({
    next: (makes) => {
      console.log('Makes loaded:', makes);
      this.makes = makes;

      if (makes.length > 0) {
        this.filtersForm.get('make')?.enable();
      }
    },
    error: (err) => {
      console.error('Error loading makes', err);
    }
  });
}


  onMakeChange(makeId: number) {
  if (!makeId) return;

  // Reset Model dropdown
  this.models = [];
  this.filtersForm.patchValue({ model: null });
  this.filtersForm.get('model')?.disable();

  this.shopService.getModelsByMake(makeId).subscribe({
    next: (models) => {
      console.log('Models loaded:', models);
      this.models = models;

      if (models.length > 0) {
        this.filtersForm.get('model')?.enable();
      }
    },
    error: (err) => {
      console.error('Error loading models', err);
    }
  });
}


  getYearsList(): number[] {
    const currentYear = new Date().getFullYear();
    const years = [];
    for (let year = 1980; year <= currentYear; year++) {
      years.push(year);
    }
    return years.reverse(); // Most recent year first
  }

  applyFilters() {
  const formValue = this.filtersForm.value;

  const params = new ShopParams();

  params.typeId = formValue.type?.id;
  params.brandId = formValue.brand;
  params.makeId = formValue.make;
  params.modelId = formValue.model;

  params.minKilometers = formValue.minKilometers;
  params.maxKilometers = formValue.maxKilometers;

  params.minYear = formValue.minYear;
  params.maxYear = formValue.maxYear;

  params.ownerNumber = formValue.ownerNumber;
  params.transmissionType = formValue.transmissionType;

  console.log('Final filter params:', params);

  this.dialogRef.close(params);
}
}
