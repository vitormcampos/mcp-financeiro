import { Component, inject, TemplateRef } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { CashFlowService } from '../../../services/cash-flow.service';
import { NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { concatMap, tap } from 'rxjs';
import { CashFlow } from '../../../models/cash-flow';
import { CashflowStore } from '../../../stores/cashflow.store';

@Component({
  selector: 'app-upsert',
  imports: [FormsModule],
  templateUrl: './upsert.component.html',
  styleUrl: './upsert.component.css',
})
export class UpsertComponent {
  private readonly cashFlowService = inject(CashFlowService);
  private readonly cashFlowStore = inject(CashflowStore);
  private readonly modalService = inject(NgbModal);

  expectedStatuses = Object.entries({ PENDING: 'Pendente', PAID: 'Pago' });

  expectedTypes = Object.entries({
    INCOME: 'Entrada',
    EXPENSE: 'Saída',
    INVESTMENT: 'Investimento',
  });

  submit(formElement: NgForm) {
    if (formElement.invalid) {
      return;
    }

    this.modalService.dismissAll();

    this.cashFlowService
      .create(formElement.value)
      .pipe(
        tap(() => {
          formElement.resetForm();
        }),
        concatMap(() => {
          return this.cashFlowService.getAll();
        }),
      )
      .subscribe((cashFlows: CashFlow[]) => this.cashFlowStore.set(cashFlows));
  }

  open(content: TemplateRef<any>) {
    this.modalService.open(content, { ariaLabelledBy: 'Novo Registro' });
  }

  close() {
    this.modalService.dismissAll();
  }
}
