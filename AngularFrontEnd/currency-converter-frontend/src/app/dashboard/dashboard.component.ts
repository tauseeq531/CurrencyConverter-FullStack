
import { Component } from '@angular/core';
import { ConverterComponent } from '../features/converter/converter.component';
import { HistoryComponent } from '../features/history/history.component';

@Component({
  standalone: true,
  selector: 'app-dashboard',
  imports: [ConverterComponent, HistoryComponent],
  template: `
    <div class="grid md:grid-cols-2 gap-6">
      <app-converter (converted)="onConverted()"></app-converter>
      <app-history></app-history>
    </div>
  `
})
export class DashboardComponent {
  onConverted(){}
}
