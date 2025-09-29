import { HttpClient } from '@angular/common/http';
import { inject, Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root',
})
export class ChatService {
  private readonly httpClient = inject(HttpClient);

  sendPrompt(message: string) {
    return this.httpClient.post<{ message: string }>(
      `${import.meta.env['NG_APP_PUBLIC_URL']}/api/chat`,
      { message }
    );
  }
}
