import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { CreateDialogData } from "../../models/dialog.data.model";
import { User } from "src/app/core/model/user.model";
import { Observable, tap } from "rxjs";
import { KeycloakApi } from "src/app/core/api/keycloak.api";
import { FormControl, FormGroup, Validators } from "@angular/forms";
import { Task } from "../../models/task.model";

@Component({
    selector: 'app-task-dialog',
    templateUrl: 'create-task.dialog.html',
    styleUrls: ['create-task.dialog.scss']
  })
  export class CreateTaskDialogComponent {
    users$!: Observable<User[]>;
    boardUsers: string[] = [];
    boardId = "";
    form: FormGroup = new FormGroup({
      name: new FormControl('', Validators.required),
      stageId: new FormControl('', Validators.required),
      description: new FormControl(''),
      deadline: new FormControl(''),
      executor: new FormControl('')
    });

    constructor(
      public dialogRef: MatDialogRef<CreateTaskDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: CreateDialogData,
      private keycloak: KeycloakApi
    ) {
      this.users$ = keycloak.getUsers();
      this.form.controls['stageId'].setValue(data.stageId);
      data.board.pipe(
        tap(res => {
          this.boardId = res.id;
          this.boardUsers = this.boardUsers.concat(res.users);
          this.boardUsers.push(res.ownerId);
        })
      ).subscribe();
    }
  
    onClick(): void {
      if(this.form.valid) {
        const val = this.form.value;
        this.dialogRef.close(new Task('', val.name, this.boardId, val.stageId, val.description, val.deadline, val.executor));
      }
    }

    onNoClick(): void {
      this.dialogRef.close();
    }
  }