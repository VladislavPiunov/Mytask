import { Component, OnInit } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { StageApi } from './api/stage.api';
import { LayoutFacade } from 'src/app/core/layout/layout.facade';
import { Stage } from './models/stage.model';
import { Board } from 'src/app/core/layout/models/board.model';
import { Task } from './models/task.model';
import { TaskApi } from './api/task.api';
import { MatDialog } from '@angular/material/dialog';
import { CreateTaskDialogComponent } from './dialogs/create-task/create-task.dialog';
import { CdkDragDrop } from '@angular/cdk/drag-drop';
import { EditTaskDialogComponent } from './dialogs/edit-task/edit-task.dialog';
import { DeleteTaskDialogComponent } from './dialogs/delete-task/delete-task.dialog';
import * as moment from 'moment';

@Component({
  selector: 'app-tasktracker',
  templateUrl: './tasktracker.component.html',
  styleUrls: ['./tasktracker.component.scss']
})
export class TasktrackerComponent implements OnInit{
  selectedBoard$: Observable<Board>;
  stages$: Observable<Stage[]> = new Observable<Stage[]>();
  tasks: Task[] =[];
  enteredStageId = "";
  boardId = "";
  users: string[] = [];

  constructor (
    private layoutFacade: LayoutFacade,
    private stageApi: StageApi,
    private taskApi: TaskApi,
    private dialog: MatDialog
  ) 
  {
    this.selectedBoard$ = layoutFacade.getSelectedBoard$();
  }
  
  ngOnInit(): void {
    this.loadBoard();
  }

  loadBoard(): void {
    this.selectedBoard$.subscribe({
      next: (board) => {
        if (board.id == '')
          return;
        this.boardId = board.id;
        this.users = board.users;
        this.stages$ = this.stageApi.getStages(board.id);
        this.taskApi.getTasks(board.id).pipe(
          tap((tasks) => {
            this.tasks = tasks;
          })
        ).subscribe();
      }});
  }

  reloadTasks(): void {
    this.taskApi.getTasks(this.boardId).pipe(
      tap((tasks) => {
        this.tasks = tasks;
      })
    ).subscribe();
  }

  openDialogCreate(stage: string): void {
    const dialogRef = this.dialog.open(CreateTaskDialogComponent, {
      width: '900px',
      height: '600px',
      data: { stages: this.stages$, board: this.selectedBoard$, stageId: stage },
      panelClass: 'dialog-box'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result instanceof Task) {
        this.taskApi.createTask(result).pipe(
          tap(() => {
            this.reloadTasks();
          })
        ).subscribe();
      }
    });
  }

  openDialogEdit(task: Task): void {
    const dialogRef = this.dialog.open(EditTaskDialogComponent, {
      width: '900px',
      height: '600px',
      data: { stages: this.stages$, board: this.selectedBoard$, task: task },
      panelClass: 'dialog-box'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != null)
        this.taskApi.updateTask(result).pipe(
          tap(() => {
              this.reloadTasks();
          })
        ).subscribe();
    });
  }

  deleteTask(taskId: string): void {
    const dialogRef = this.dialog.open(DeleteTaskDialogComponent, {
      width: '350px'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result == true) {
        this.taskApi.deleteTask(taskId).pipe(
          tap(res => {
            if (res == true) {
              this.reloadTasks();
            }
          })
        ).subscribe();
      }
    })
  }

  drop(event: CdkDragDrop<Task[]>) {
    if (this.enteredStageId == '')
      return;

    const item = event.item.data as Task;
    item.stageId = this.enteredStageId;
    this.taskApi.updateTask(item).pipe(
      tap((response) => {
        if (response instanceof Task) {
          this.loadBoard();
          this.enteredStageId = '';
        }
      })
    ).subscribe();
  }

  enter(stageId: string) {
    this.enteredStageId = stageId;
  }

  transformDate(date: Date): string {
    return (moment(date)).format('DD.MM.YYYY');
  }
}
