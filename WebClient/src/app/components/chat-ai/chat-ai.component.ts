import { Component, inject, signal } from '@angular/core';
import { FormsModule, NgForm } from '@angular/forms';
import { ChatService } from '../../services/chat.service';
import { MarkdownComponent } from 'ngx-markdown';

@Component({
  selector: 'app-chat-ai',
  imports: [FormsModule, MarkdownComponent],
  templateUrl: './chat-ai.component.html',
  styleUrl: './chat-ai.component.css',
})
export class ChatAiComponent {
  chatService = inject(ChatService);

  chats = signal<ChatMessage[]>([]);

  addMessage(message: ChatMessage) {
    this.chats.update((old) => {
      return [...old, message];
    });
  }

  submitMessage(f: NgForm) {
    console.log(f.value);
    const message = f.control.get('userMessage')?.value;

    this.addMessage({ content: message, origin: 'user' });

    this.chatService.sendPrompt(message).subscribe((result) => {
      console.log(result);
      this.addMessage({ content: result.message, origin: 'agent' });
    });
  }
}

type ChatMessage = {
  origin: 'user' | 'agent';
  content: string;
};
