import { Component, inject, signal, TemplateRef } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { NgbDatepickerModule, NgbModal } from '@ng-bootstrap/ng-bootstrap';
import { CashFlowService } from '../../../services/cash-flow.service';
import { CashflowStore } from '../../../stores/cashflow.store';

@Component({
  selector: 'app-filter',
  imports: [FormsModule, NgbDatepickerModule],
  templateUrl: './filter.component.html',
  styleUrl: './filter.component.css',
})
export class FilterComponent {
  private readonly cashFlowService = inject(CashFlowService);
  private readonly cashFlowStore = inject(CashflowStore);
  private readonly modalService = inject(NgbModal);

  showForm = signal(false);

  formFields: FormField[] = [
    { name: 'description', label: 'Descrição', type: 'text' },
    {
      name: 'minValue',
      label: 'Valor minimo',
      type: 'number',
      columnsWidth: 'col-6',
    },
    {
      name: 'maxValue',
      label: 'Valor maximo',
      type: 'number',
      columnsWidth: 'col-6',
    },
    { name: 'date', label: 'Data', type: 'date' },
    {
      name: 'status',
      label: 'Status',
      type: 'select',
      options: [
        { label: 'Pendente', value: 'PENDING' },
        { label: 'Pago', value: 'PAID' },
      ],
      columnsWidth: 'col-6',
    },
    {
      name: 'type',
      label: 'Tipo',
      type: 'select',
      options: [
        { label: 'Receita', value: 'INCOME' },
        { label: 'Despesa', value: 'EXPENSE' },
        { label: 'Investimento', value: 'INVESTMENT' },
      ],
      columnsWidth: 'col-6',
    },
  ];

  toggleForm() {
    this.showForm.set(!this.showForm());
  }

  submit(formElement: NgForm) {
    console.log(formElement.value);
    const { description, minValue, maxValue, date, status, type } =
      formElement.value;
    const month = date?.month;
    const year = date?.year;

    this.cashFlowService
      .getAll(description, minValue, maxValue, month, year, status, type)
      .subscribe((response) => {
        this.cashFlowStore.set(response);
        formElement.reset();
      });
  }

  open(content: TemplateRef<any>) {
    this.modalService.open(content, { ariaLabelledBy: 'Novo Registro' });
  }

  close() {
    this.modalService.dismissAll();
  }
}

type FormField = {
  name: string;
  label: string;
  type: 'text' | 'number' | 'date' | 'select';
  options?: { label: string; value: string }[];
  columnsWidth?: string;
};
