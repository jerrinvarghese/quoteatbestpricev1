import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule } from '@angular/forms';
import { MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { MatDivider } from '@angular/material/divider';
import { MatButton } from '@angular/material/button';
import { MatFormField, MatLabel, MatFormFieldModule } from '@angular/material/form-field';
import { MatSelectModule } from '@angular/material/select';
import { MatOptionModule } from '@angular/material/core';
import { MatInputModule } from '@angular/material/input';
import { MatAutocompleteModule } from '@angular/material/autocomplete';
import { MatListOption, MatSelectionList } from '@angular/material/list';
import { CommonModule } from '@angular/common';

import { Observable } from 'rxjs';
import { debounceTime, distinctUntilChanged, switchMap, filter } from 'rxjs/operators';

import { ShopService } from '../../../core/services/shop.service';
import { ProductType } from '../../../shared/models/product-type';
import { ProductBrand } from '../../../shared/models/product-brand';
import { ProductMake } from '../../../shared/models/product-make';
import { ProductModel } from '../../../shared/models/product-model';

@Component({
  standalone: true,
  imports: [
    ReactiveFormsModule,
    FormsModule,
    CommonModule,

    MatDialogModule,
    MatDivider,
    MatButton,
    MatFormField,
    MatLabel,
    MatSelectModule,
    MatOptionModule,
    MatFormFieldModule,
    MatInputModule,
    MatAutocompleteModule,
    MatSelectionList,
    MatListOption
  ],
  templateUrl: './notify-me-dialog.component.html'
})
export class NotifyMeDialogComponent implements OnInit {

  notifyForm!: FormGroup;

  types: ProductType[] = [];
  brands: ProductBrand[] = [];
  makes: ProductMake[] = [];
  models: ProductModel[] = [];
  years: number[] = [];

  filteredLocations$!: Observable<string[]>;

  constructor(
    private fb: FormBuilder,
    private shopService: ShopService,
    private dialogRef: MatDialogRef<NotifyMeDialogComponent>
  ) {}

  ngOnInit(): void {
    this.notifyForm = this.fb.group({
      email: ['test@email.com'],          // ✅ new field
      userId: ['1234'],
      type: [null],
  brand: [{ value: null, disabled: true }],
  make: [{ value: null, disabled: true }],
  model: [{ value: null, disabled: true }],

      minKilometers: [''],
      maxKilometers: [''],
      minYear: [''],
      maxYear: [''],

      minPrice: [null],
      maxPrice: [null],

      ownerNumber: [''],
      transmissionType: [''],
      fuelType: [''],
      location: ['']
    });

    // 🔁 Location autocomplete (same as filters)
    this.filteredLocations$ = this.notifyForm.get('location')!.valueChanges.pipe(
      debounceTime(300),
      distinctUntilChanged(),
      filter(value => value && value.length >= 3),
      switchMap(value => this.shopService.getLocations(value))
    );

    // 🔽 Load dropdown data
    this.shopService.getTypes().subscribe({
      next: response => this.types = response
    });

    this.years = this.getYearsList();
  }

  // ---------------------------
  // Dependent dropdown logic
  // ---------------------------

  onTypeChange(typeId: number) {
  if (!typeId) return;

  this.brands = [];
  this.makes = [];
  this.models = [];

  this.notifyForm.patchValue({
    brand: null,
    make: null,
    model: null
  });

  this.notifyForm.get('brand')?.disable();

  this.shopService.getBrandsByType(typeId).subscribe({
    next: brands => {
      this.brands = brands;
      if (brands.length) {
        this.notifyForm.get('brand')?.enable();
      }
    }
  });
}
  onBrandChange(brandId: number) {
    if (!brandId) return;

    this.makes = [];
    this.models = [];

    this.notifyForm.patchValue({
      make: null,
      model: null
    });

    this.notifyForm.get('make')?.disable();

    this.shopService.getMakesByBrand(brandId).subscribe({
      next: makes => {
        this.makes = makes;
        if (makes.length) {
          this.notifyForm.get('make')?.enable();
        }
      }
    });
  }

  onMakeChange(makeId: number) {
    if (!makeId) return;

    this.models = [];
    this.notifyForm.patchValue({ model: null });
    this.notifyForm.get('model')?.disable();

    this.shopService.getModelsByMake(makeId).subscribe({
      next: models => {
        this.models = models;
        if (models.length) {
          this.notifyForm.get('model')?.enable();
        }
      }
    });
  }

  // ---------------------------
  // Helpers
  // ---------------------------

  getYearsList(): number[] {
    const currentYear = new Date().getFullYear();
    const years: number[] = [];
    for (let year = 1980; year <= currentYear; year++) {
      years.push(year);
    }
    return years.reverse();
  }

  // ---------------------------
  // Submit Notify Me
  // ---------------------------

  // submit() {
  //   if (this.notifyForm.invalid) return;

  //   this.shopService
  //     .createNotification(this.notifyForm.value)
  //     .subscribe(() => this.dialogRef.close());
  // }

  submit() {
  if (this.notifyForm.invalid) return;

  const formValue = this.notifyForm.value;

  const payload = {
    userId: formValue.userId,
    email: formValue.email,

    typeId: formValue.type,
    brandId: formValue.brand,
    makeId: formValue.make,
    modelId: formValue.model,

    minKilometers: formValue.minKilometers,
    maxKilometers: formValue.maxKilometers,
    minYear: formValue.minYear,
    maxYear: formValue.maxYear,

    ownerNumber: formValue.ownerNumber,
    transmissionType: formValue.transmissionType,
    fuelType: formValue.fuelType,
    location: formValue.location,

    minPrice: formValue.minPrice,
    maxPrice: formValue.maxPrice
  };

  console.log('Payload sent:', payload); // 🔍 debug

  this.shopService
    .createNotification(payload)
    .subscribe(() => this.dialogRef.close());
}
}
