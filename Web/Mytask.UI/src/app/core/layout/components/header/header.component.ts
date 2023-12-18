import { Component, Input, OnInit } from '@angular/core';
import { Board } from '../../models/board.model';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-header',
  templateUrl: './header.component.html',
  styleUrls: ['./header.component.scss']
})
export class HeaderComponent implements OnInit{

  @Input() selectedBoard$!: Observable<Board>;

  board!: Board;

  ngOnInit() {
    this.selectedBoard$.subscribe(val => {
      this.board = val;
    });
  }
}
