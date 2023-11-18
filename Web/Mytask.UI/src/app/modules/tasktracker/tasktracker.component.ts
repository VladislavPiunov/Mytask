import { Component, OnInit } from '@angular/core';
import { Board } from './models/board.model';
import { Observable } from 'rxjs';
import { TasktrackerFacade } from './tasktracker.facade';
import { TasktrackerState } from './state/tasktracker.state';

@Component({
  selector: 'app-tasktracker',
  templateUrl: './tasktracker.component.html',
  styleUrls: ['./tasktracker.component.scss']
})
export class TasktrackerComponent implements OnInit {
  boards$: Observable<Board[]>;

  constructor( 
    private tasktrackerFacade: TasktrackerFacade
  ) 
  {
    this.boards$ = tasktrackerFacade.getBoards$();
  }

  ngOnInit(): void {
    this.tasktrackerFacade.loadBoards();
  }
}
