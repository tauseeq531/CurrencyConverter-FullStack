
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { environment } from '../../environments/environment';
import { Observable } from 'rxjs';

export interface ConversionRequest { amount: number; fromCurrency: string; toCurrency: string; }
export interface ConversionResponse { convertedAmount: number; rate: number; fromCurrency: string; toCurrency: string; timeUtc: Date; }

@Injectable({ providedIn: 'root' })
export class ApiService {
  private base = environment.apiBaseUrl;
  constructor(private http: HttpClient){}

  getCurrencies(): Observable<string[]> {
    return this.http.get<string[]>(`${this.base}/currencies`);
    }

  convert(req: ConversionRequest): Observable<ConversionResponse> {
    return this.http.post<ConversionResponse>(`${this.base}/conversion`, req);
  }

  getHistory(): Observable<ConversionResponse[]> {
    return this.http.get<ConversionResponse[]>(`${this.base}/conversion/history?page=1&pageSize=100`);
  }

  getRates(base: string): Observable<any> {
    return this.http.get<any>(`${this.base}/rates/${base}`);
  }
}
