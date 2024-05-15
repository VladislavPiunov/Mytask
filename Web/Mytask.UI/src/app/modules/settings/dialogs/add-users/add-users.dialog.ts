import { Component, Inject, OnInit } from '@angular/core';
import { MAT_DIALOG_DATA, MatDialogRef } from '@angular/material/dialog';
import { MatListOption } from '@angular/material/list';
import { tap } from 'rxjs';
import { KeycloakApi } from 'src/app/core/api/keycloak.api';
import { Board } from 'src/app/core/layout/models/board.model';
import { User } from 'src/app/core/model/user.model';

@Component({
  selector: 'app-add-users',
  templateUrl: './add-users.dialog.html',
  styleUrls: ['./add-users.dialog.scss']
})
export class AddUsersDialogComponent implements OnInit{
  users!: User[]

  constructor(
    public dialogRef: MatDialogRef<AddUsersDialogComponent>,
      @Inject(MAT_DIALOG_DATA) public data: Board,
      private keycloak: KeycloakApi
  ) {

  }

  ngOnInit(): void {
    this.keycloak.getUsers().pipe(
      tap((res) => {
        this.users = res.filter(u => u.id != this.data.ownerId && !this.data.users.includes(u.id));
      })
    ).subscribe();
  }

  onClick(selected: MatListOption[]): void {
    this.dialogRef.close(selected.map(v => v.value));
  }

  onNoClick(): void {
    this.dialogRef.close();
  }
}

