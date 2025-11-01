import { Component, EventEmitter, Output, OnInit, computed, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormBuilder, FormGroup, ReactiveFormsModule, Validators } from '@angular/forms';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatSelectModule } from '@angular/material/select';
import { MatButtonModule } from '@angular/material/button';
import { MatProgressSpinnerModule } from '@angular/material/progress-spinner';
import { ApiService, ConversionRequest, ConversionResponse } from '../../shared/api.service';

@Component({
  standalone: true,
  selector: 'app-converter',
  imports: [
    CommonModule,
    ReactiveFormsModule,
    MatFormFieldModule,
    MatInputModule,
    MatSelectModule,
    MatButtonModule,
    MatProgressSpinnerModule
  ],
  templateUrl: './converter.component.html',
  styleUrls: ['./converter.component.css']
})
export class ConverterComponent implements OnInit {
  @Output() converted = new EventEmitter<void>();
  form!: FormGroup;
  private _currencies = signal<string[]>([]);
  loading = signal(false);
  result = signal<ConversionResponse | null>(null);
  currencies = computed(() => this._currencies());

  constructor(private fb: FormBuilder, private api: ApiService) {}

  ngOnInit() {
    this.form = this.fb.group({
      amount: [100, [Validators.required, Validators.min(0)]],
      fromCurrency: ['USD', Validators.required],
      toCurrency: ['SAR', Validators.required],
    });

    this.api.getCurrencies().subscribe(cs => this._currencies.set(cs));
  }

  submit() {
  if (this.form.invalid) return;
  this.loading.set(true);
  const req: ConversionRequest = this.form.value;
  this.api.convert(req).subscribe({
    next: r => {
      this.result.set(r);
      this.converted.emit();
    },
    error: _ => {},
    complete: () => this.loading.set(false)
  });
  }
}
