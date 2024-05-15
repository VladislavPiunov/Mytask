import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { LayoutFacade } from 'src/app/core/layout/layout.facade';
import { Board } from 'src/app/core/layout/models/board.model';
import { AddUsersDialogComponent } from './dialogs/add-users/add-users.dialog';
import { User } from 'src/app/core/model/user.model';
import { KeycloakApi } from 'src/app/core/api/keycloak.api';
import { tap } from 'rxjs';

@Component({
  selector: 'app-settings',
  templateUrl: './settings.component.html',
  styleUrls: ['./settings.component.scss']
})
export class SettingsComponent implements OnInit{
  isChanged = false;
  selectedBoard!: Board;
  boardSnapshot!: Board;
  users!: User[];

  constructor (
    private layoutFacade: LayoutFacade,
    private dialog: MatDialog,
    private keycloak: KeycloakApi
  ) 
  {
    layoutFacade.getSelectedBoard$().subscribe({
      next: (board) => {
        this.selectedBoard = { ...board };
        this.boardSnapshot = board;
      }
    });
  }

  ngOnInit(): void {
    this.keycloak.getUsers().pipe(
      tap((res) => {
        this.users = res.filter(u => this.selectedBoard.users.includes(u.id));
      })
    ).subscribe();
  }

  removeUser(user: User): void {
    const usersIndex = this.users.indexOf(user, 0);
    if (usersIndex > -1) {
      this.users.splice(usersIndex, 1);
    }

    this.selectedBoard.users = this.selectedBoard.users.filter(x => x != user.id);

    this.isChanged = true;
  } 

  openDialogAddUsers(): void {
    const dialogRef = this.dialog.open(AddUsersDialogComponent, {
      width: '500px',
      height: '700px',
      data: this.selectedBoard,
      panelClass: 'dialog-box'
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result != undefined) {
        this.users = this.users.concat(result);
        this.selectedBoard.users = this.selectedBoard.users.concat(result.map((x: User) => x.id));
        this.isChanged = true;
      }
    });
  }

  onCancel(): void {
    this.selectedBoard = { ...this.boardSnapshot };
    this.keycloak.getUsers().pipe(
      tap((res) => {
        this.users = res.filter(u => this.selectedBoard.users.includes(u.id));
        this.isChanged = false;
      })
    ).subscribe();
  }

  onSave(): void {
    this.layoutFacade.updateBoard(this.selectedBoard);
    this.isChanged = false;
  }
}