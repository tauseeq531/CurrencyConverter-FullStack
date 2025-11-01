
import { Component, signal } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { MatToolbarModule } from '@angular/material/toolbar';
import { MatSlideToggleModule } from '@angular/material/slide-toggle';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet, MatToolbarModule, MatSlideToggleModule],
  template: `
    <mat-toolbar color="primary">
      <span class="font-semibold">Currency Converter</span>
      <span class="flex-1"></span>
      <mat-slide-toggle (change)="toggleDark()">Dark</mat-slide-toggle>
    </mat-toolbar>
    <div class="p-4 max-w-5xl mx-auto">
      <router-outlet></router-outlet>
    </div>
  `
})
export class AppComponent {
  private isDark = signal(false);
  toggleDark() {
    this.isDark.update(v => !v);
    document.body.classList.toggle('dark');
    document.body.classList.toggle('bg-gray-900');
    document.body.classList.toggle('text-gray-100');
  }
}
