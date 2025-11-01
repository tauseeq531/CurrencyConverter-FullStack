
import { Component, OnInit, signal } from '@angular/core';
import { CommonModule } from '@angular/common';
import { MatTableModule } from '@angular/material/table';
import { MatPaginatorModule } from '@angular/material/paginator';
import { ApiService, ConversionResponse } from '../../shared/api.service';

@Component({
  standalone: true,
  selector: 'app-history',
  imports: [CommonModule, MatTableModule, MatPaginatorModule],
  templateUrl: './history.component.html'
})
export class HistoryComponent implements OnInit {
  cols = ['time','pair','amounts'];
  rows = signal<ConversionResponse[]>([]);
  constructor(private api: ApiService){}
  ngOnInit(){
    this.api.getHistory().subscribe(r => this.rows.set(r));
  }
}
