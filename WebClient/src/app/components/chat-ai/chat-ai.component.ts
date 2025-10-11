import { Component, inject, OnDestroy, OnInit, signal } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ChatService } from '../../services/chat.service';
import { MarkdownComponent } from 'ngx-markdown';

@Component({
  selector: 'app-chat-ai',
  imports: [FormsModule, MarkdownComponent],
  templateUrl: './chat-ai.component.html',
  styleUrl: './chat-ai.component.css',
})
export class ChatAiComponent implements OnInit, OnDestroy {
  chatService = inject(ChatService);

  tokenSub = this.chatService.token$.subscribe((token) => {
    this.resposta.update((r) => (r += token + ' '));
  });

  completedSub = this.chatService.completed$.subscribe(() => {
    this.resposta.update((r) => (r += '\n[Resposta concluída]'));
  });

  promptSub = this.chatService.prompt$.subscribe((prompt) => {
    this.addMessage({ content: prompt, origin: 'agent' });
  });

  resposta = signal('');

  ngOnInit() {
    this.chatService.startConnection();
  }

  chats = signal<ChatMessage[]>([]);

  addMessage(message: ChatMessage) {
    this.chats.update((old) => {
      return [...old, message];
    });
  }

  submitMessage(f: NgForm) {
    const message = f.control.get('userMessage')?.value;

    this.addMessage({ content: message, origin: 'user' });

    this.chatService.sendPrompt(message);

    // this.chatService.sendPrompt(message).subscribe((result) => {
    //   console.log(result);
    //   this.addMessage({ content: result.message, origin: 'agent' });
    // });
  }

  ngOnDestroy(): void {
    this.tokenSub?.unsubscribe();
    this.completedSub?.unsubscribe();
    this.chatService.stopConnection();
  }
}

type ChatMessage = {
  origin: 'user' | 'agent';
  content: string;
};
