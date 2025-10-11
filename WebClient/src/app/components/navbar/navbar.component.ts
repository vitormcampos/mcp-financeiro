import { Component, computed, inject } from '@angular/core';
import { CashflowStore } from '../../stores/cashflow.store';
import { toSignal } from '@angular/core/rxjs-interop';
import { CurrencyPipe } from '@angular/common';
import { ChatAiStore } from '../../stores/chat-ai.store';

@Component({
  selector: 'app-navbar',
  imports: [CurrencyPipe],
  templateUrl: './navbar.component.html',
  styleUrl: './navbar.component.css',
})
export class NavbarComponent {
  private readonly cashFlowStore = inject(CashflowStore);
  private readonly chatAiStore = inject(ChatAiStore);

  cashFlows = toSignal(this.cashFlowStore.get(), { initialValue: [] });

  totalCashFlow = computed(() => {
    const income = this.cashFlows().filter((cf) => cf.type === 'INCOME');
    const totalIncome = income.reduce((acc, curr) => acc + curr.amount, 0);

    const expense = this.cashFlows().filter((cf) => cf.type === 'EXPENSE');
    const totalExpense = expense.reduce((acc, curr) => acc + curr.amount, 0);

    const investment = this.cashFlows().filter(
      (cf) => cf.type === 'INVESTMENT',
    );
    const totalInvestment = investment.reduce(
      (acc, curr) => acc + curr.amount,
      0,
    );

    return totalIncome - totalExpense - totalInvestment;
  });

  showChatAi = toSignal(this.chatAiStore.get(), { initialValue: false });

  toggleChatAi() {
    this.chatAiStore.set(!this.showChatAi());
  }
}
