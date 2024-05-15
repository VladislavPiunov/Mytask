import { Component, Inject } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { KeycloakApi } from 'src/app/core/api/keycloak.api';
import { Observable, tap } from 'rxjs';
import { User } from 'src/app/core/model/user.model';
import { EditDialogData } from '../../models/dialog.data.model';
import { Task } from '../../models/task.model';

@Component({
  selector: 'app-edit-task',
  templateUrl: './edit-task.dialog.html',
  styleUrls: ['./edit-task.dialog.scss']
})
export class EditTaskDialogComponent {
  users$!: Observable<User[]>;
  boardUsers: string[] = [];
  task!: Task;

  constructor(
    public dialogRef: MatDialogRef<EditTaskDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: EditDialogData,
    private keycloak: KeycloakApi
  ) {
    this.users$ = keycloak.getUsers();
    this.task = Object.create(data.task);
    data.board.pipe(
      tap(res => {
        this.boardUsers = this.boardUsers.concat(res.users);
        this.boardUsers.push(res.ownerId);
      })
    ).subscribe();
  }

  onClick(): void {
    if (this.task.equals(this.data.task))
      this.dialogRef.close(null);
    else
      this.dialogRef.close(this.task);
  }

  onNoClick(): void {
    this.dialogRef.close(null);
  }
}
