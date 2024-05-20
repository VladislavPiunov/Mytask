import { Component, Inject } from "@angular/core";
import { MAT_DIALOG_DATA, MatDialogRef } from "@angular/material/dialog";
import { User } from "src/app/core/model/user.model";
import { Observable } from "rxjs";
import { KeycloakApi } from "src/app/core/api/keycloak.api";
import { FormControl, FormGroup, Validators } from "@angular/forms";

@Component({
    selector: 'app-meeting-dialog',
    templateUrl: 'create-meeting.dialog.html',
    styleUrls: ['create-meeting.dialog.scss']
  })
  export class CreateMeetingDialogComponent {
    users$!: Observable<User[]>;
    selectedUsers: User[] = [];
    boardUsers: string[] = [];
    boardId = "";
    form: FormGroup = new FormGroup({
      name: new FormControl('', Validators.required),
    });
    hours = [0,1,2,3,4,5,6,7,8,9,10,11,12,13,14,15,16,17,18,19,20,21,22,23];
    minutes = ["00","05","10","15","20","25","30","35","40","45","50","55"]

    constructor(
      public dialogRef: MatDialogRef<CreateMeetingDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: any,
      private keycloak: KeycloakApi
    ) {
      this.users$ = keycloak.getUsers();
    }
  
    removeUser(user: User): void {
      const usersIndex = this.selectedUsers.indexOf(user, 0);
      if (usersIndex > -1) {
        this.selectedUsers.splice(usersIndex, 1);
      }
    } 

    onClick(): void {
      if(this.form.valid) {
        const val = this.form.value;
        this.dialogRef.close();
      }
    }

    onNoClick(): void {
      this.dialogRef.close();
    }
  }