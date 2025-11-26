import { Injectable, signal } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class PopupService {

    message = signal<string | null>(null);
    closing = signal(false);

    show(message: string) {
        this.closing.set(false);
        this.message.set(message);

        setTimeout(() => this.startClosing(), 2500);
    }

    startClosing() {
        this.closing.set(true);

        setTimeout(() => {
            this.message.set(null);
            this.closing.set(false);
        }, 450);
    }
}
