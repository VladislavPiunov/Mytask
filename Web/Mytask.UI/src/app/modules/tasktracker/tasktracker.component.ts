import { Component, OnInit } from '@angular/core';
import { Observable } from 'rxjs';
import { StageApi } from './api/stage.api';
import { LayoutFacade } from 'src/app/core/layout/layout.facade';
import { Stage } from './models/stage.model';
import { Board } from 'src/app/core/layout/models/board.model';
import { Task } from './models/task.model';
import { TaskApi } from './api/task.api';

@Component({
  selector: 'app-tasktracker',
  templateUrl: './tasktracker.component.html',
  styleUrls: ['./tasktracker.component.scss']
})
export class TasktrackerComponent implements OnInit{
  selectedBoard$: Observable<Board>;
  stages$!: Observable<Stage[]>;
  tasks$!: Observable<Task[]>

  constructor (
    private layoutFacade: LayoutFacade,
    private stageApi: StageApi,
    private taskApi: TaskApi
  ) 
  {
    this.selectedBoard$ = layoutFacade.getSelectedBoard$();
  }
  
  ngOnInit(): void {
    this.selectedBoard$.subscribe({
      next: (board) => {
        if (board.id == '')
          return;
        this.stages$ = this.stageApi.getStages(board.id);
        this.tasks$ = this.taskApi.getTasks(board.id);
      }});
  }

}
