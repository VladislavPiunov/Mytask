import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { Task } from "../models/task.model";

@Component({
    selector: 'app-task-dialog',
    templateUrl: 'task.dialog.html',
    styleUrls: ['task.dialog.scss']
  })
  export class TaskDialogComponent {
    constructor(
      public dialogRef: MatDialogRef<TaskDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public task: Task,
    ) {}
  
    onNoClick(): void {
      this.dialogRef.close();
    }
  }