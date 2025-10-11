import { inject, Injectable } from '@angular/core';
import { Subject } from 'rxjs';

import * as signalR from '@microsoft/signalr';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private hubConnection!: signalR.HubConnection;

  private tokenSubject = new Subject<string>();
  private promptSubject = new Subject<string>();
  private completedSubject = new Subject<void>();

  token$ = this.tokenSubject.asObservable();
  prompt$ = this.promptSubject.asObservable();
  completed$ = this.completedSubject.asObservable();

  startConnection(): void {
    this.hubConnection = new signalR.HubConnectionBuilder()
      .withUrl(import.meta.env['NG_APP_PUBLIC_URL'] + '/chat', {
        skipNegotiation: true,
        transport: signalR.HttpTransportType.WebSockets,
      }) // 🔧 Ajuste para sua URL real
      .withAutomaticReconnect()
      .build();

    this.hubConnection
      .start()
      .then(() => console.log('Conectado ao SignalR'))
      .catch((err) => console.error('Erro ao conectar ao SignalR:', err));

    this.hubConnection.on('ReceiveToken', (token: string) => {
      this.tokenSubject.next(token);
    });

    this.hubConnection.on('ReceivePrompt', (prompt: string) => {
      this.promptSubject.next(prompt);
    });

    this.hubConnection.on('Completed', () => {
      this.completedSubject.next();
    });
  }

  stopConnection(): void {
    this.hubConnection?.stop().then(() => console.log('Conexão encerrada'));
  }

  sendPrompt(message: string) {
    if (
      this.hubConnection &&
      this.hubConnection.state === signalR.HubConnectionState.Connected
    ) {
      this.hubConnection
        .invoke('SendPrompt', message)
        .catch((err) => console.error('Erro ao enviar prompt:', err));
    } else {
      console.warn('Conexão com SignalR não está ativa.');
    }
  }
}
