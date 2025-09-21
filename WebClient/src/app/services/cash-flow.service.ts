import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';
import { CashFlow } from '../models/cash-flow';

@Injectable({
  providedIn: 'root',
})
export class CashFlowService {
  private readonly httpClient = inject(HttpClient);

  getAll() {
    return this.httpClient.get<CashFlow[]>(
      `${import.meta.env['NG_APP_PUBLIC_URL']}/api/conta`
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
