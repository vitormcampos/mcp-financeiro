import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { CashFlow } from '../models/cash-flow';

@Injectable({
  providedIn: 'root',
})
export class CashflowStore {
  private readonly cashFlows = new BehaviorSubject<CashFlow[]>([]);

  get() {
    return this.cashFlows.asObservable();
  }

  set(cashFlows: CashFlow[]) {
    this.cashFlows.next(cashFlows);
  }
}
