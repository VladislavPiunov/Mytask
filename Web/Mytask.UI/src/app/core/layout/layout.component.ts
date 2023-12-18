import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from './models/board.model';
import { LayoutFacade } from './layout.facade';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent implements OnInit {
  boards$: Observable<Board[]>;
  selectedBoard$: Observable<Board>;

  constructor( 
    private layoutFacade: LayoutFacade,
    ) 
    {
      this.boards$ = layoutFacade.getBoards$();
      this.selectedBoard$ = layoutFacade.getSelectedBoard$();
    }
  
    ngOnInit(): void {
      this.layoutFacade.loadBoards();
    }
}
