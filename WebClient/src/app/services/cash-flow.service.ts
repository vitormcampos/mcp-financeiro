import { HttpClient, HttpParams } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CashFlow } from '../models/cash-flow';

@Injectable({
  providedIn: 'root',
})
export class CashFlowService {
  private readonly httpClient = inject(HttpClient);

  getAll(
    description = '',
    minValue = 0,
    maxValue = 0,
    month = 0,
    year = 0,
    status = '',
    type = ''
  ) {
    const params = new HttpParams()
      .set('Description', description || '')
      .set('MinValue', minValue || 0)
      .set('MaxValue', maxValue || 0)
      .set('Month', month || 0)
      .set('Year', year || 0)
      .set('Status', status || '')
      .set('Type', type || '');

    return this.httpClient.get<CashFlow[]>(
      `${import.meta.env['NG_APP_PUBLIC_URL']}/api/conta`,
      { params }
    );
  }

  create(cashFlow: {
    description: string;
    amount: number;
    type: string;
    status: string;
  }) {
    return this.httpClient.post<CashFlow>(
      `${import.meta.env['NG_APP_PUBLIC_URL']}/api/conta`,
      cashFlow
    );
  }
}
