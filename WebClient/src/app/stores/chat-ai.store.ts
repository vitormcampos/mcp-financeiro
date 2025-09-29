import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';

@Injectable({
  providedIn: 'root',
})
export class ChatAiStore {
  private showChatAi = new BehaviorSubject(false);

  get() {
    return this.showChatAi.asObservable();
  }

  set(showChatAi: boolean) {
    this.showChatAi.next(showChatAi);
  }
}
