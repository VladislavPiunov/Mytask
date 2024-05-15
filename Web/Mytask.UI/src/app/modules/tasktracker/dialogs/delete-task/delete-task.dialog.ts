import { Component } from '@angular/core';
import { MatDialogRef } from '@angular/material/dialog';

@Component({
  selector: 'app-delete-task',
  templateUrl: './delete-task.dialog.html',
  styleUrls: ['./delete-task.dialog.scss']
})
export class DeleteTaskDialogComponent {
  constructor(
    public dialogRef: MatDialogRef<DeleteTaskDialogComponent>,
  ) { }

  onClose(val: boolean): void {
    this.dialogRef.close(val);
  }
}
