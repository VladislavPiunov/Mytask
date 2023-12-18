import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { StageApi } from './api/stage.api';
import { LayoutFacade } from 'src/app/core/layout/layout.facade';
import { Stage } from './models/stage.model';
import { Board } from 'src/app/core/layout/models/board.model';

@Component({
  selector: 'app-tasktracker',
  templateUrl: './tasktracker.component.html',
  styleUrls: ['./tasktracker.component.scss']
})
export class TasktrackerComponent implements OnInit{
  stages$!: Observable<Stage[]>;
  selectedBoard$: Observable<Board>;

  constructor (
    private layoutFacade: LayoutFacade,
    private stageApi: StageApi
  ) 
  {
    this.selectedBoard$ = layoutFacade.getSelectedBoard$();
  }
  
  ngOnInit(): void {
    this.selectedBoard$.subscribe({
      next: (board) => {
        this.stages$ = this.stageApi.getStages(board.id);
      }});
  }

}
