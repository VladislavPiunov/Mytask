import { Component } from '@angular/core';
import { Observable } from 'rxjs';
import { Board } from './models/board.model';
import { LayoutFacade } from './layout.facade';

@Component({
  selector: 'app-layout',
  templateUrl: './layout.component.html',
  styleUrls: ['./layout.component.scss']
})
export class LayoutComponent {
  boards$: Observable<Board[]>;
  selectedBoard$: Observable<Board>;

  constructor( 
    private layoutFacade: LayoutFacade,
    ) 
    {
      this.layoutFacade.loadBoards();
      this.boards$ = layoutFacade.getBoards$();
      this.selectedBoard$ = layoutFacade.getSelectedBoard$();
    }
}
