import { Component, inject } from '@angular/core';
import { AsyncPipe, CurrencyPipe } from '@angular/common';
import { CashFlowService } from '../../../services/cash-flow.service';
import { tap } from 'rxjs';
import { CashflowStore } from '../../../stores/cashflow.service';

@Component({
  selector: 'app-list',
  imports: [AsyncPipe, CurrencyPipe],
  templateUrl: './list.component.html',
  styleUrl: './list.component.css',
})
export class ListComponent {
  private readonly cashFlowService = inject(CashFlowService);
  private readonly cashFlowStore = inject(CashflowStore);

  constructor() {
    this.cashFlowService
      .getAll()
      .pipe(tap((result) => this.cashFlowStore.set(result)))
      .subscribe();
  }

  cashFlows$ = this.cashFlowStore.get();
}
