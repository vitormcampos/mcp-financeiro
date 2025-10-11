import { Component, computed, inject } from '@angular/core';

import { CurrencyPipe } from '@angular/common';
import { toSignal } from '@angular/core/rxjs-interop';
import { ListComponent } from '../../components/cashflow/list/list.component';
import { UpsertComponent } from '../../components/cashflow/upsert/upsert.component';
import { CashflowStore } from '../../stores/cashflow.store';
import { ChatAiStore } from '../../stores/chat-ai.store';
import { ChatAiComponent } from '../../components/chat-ai/chat-ai.component';
import { FilterComponent } from '../../components/cashflow/filter/filter.component';

@Component({
  selector: 'app-home',
  imports: [
    ListComponent,
    CurrencyPipe,
    UpsertComponent,
    ChatAiComponent,
    FilterComponent,
  ],
  templateUrl: './home.component.html',
  styleUrl: './home.component.css',
})
export class HomeComponent {
  private readonly cashFlowStore = inject(CashflowStore);
  private readonly chatAiStore = inject(ChatAiStore);

  cashFlows = toSignal(this.cashFlowStore.get(), { initialValue: [] });

  income = computed(() => {
    return this.cashFlows().filter((cf) => cf.type === 'INCOME');
  });

  expense = computed(() => {
    return this.cashFlows().filter((cf) => cf.type === 'EXPENSE');
  });

  investment = computed(() => {
    return this.cashFlows().filter((cf) => cf.type === 'INVESTMENT');
  });

  totalIncome = computed(() => {
    return this.income().reduce((acc, curr) => acc + curr.amount, 0);
  });

  totalExpense = computed(() => {
    return this.expense().reduce((acc, curr) => acc + curr.amount, 0);
  });

  totalInvestment = computed(() => {
    return this.investment().reduce((acc, curr) => acc + curr.amount, 0);
  });

  showChatAi = toSignal(this.chatAiStore.get(), { initialValue: false });
}
