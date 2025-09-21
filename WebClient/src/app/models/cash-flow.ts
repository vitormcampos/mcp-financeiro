export class CashFlow {
  description?: string;
  amount: number = 0;
  type?: 'INCOME' | 'EXPENSE' | 'INVESTMENT';
  status?: 'PENDING' | 'PAID';
}
