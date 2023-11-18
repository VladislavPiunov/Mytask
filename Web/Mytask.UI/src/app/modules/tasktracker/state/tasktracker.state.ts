import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Board } from "../models/board.model";

@Injectable({
  providedIn: "root"
})
export class TasktrackerState {
 private updating$ = new BehaviorSubject(false);
 private boards$ = new BehaviorSubject([] as Board[]);

 isUpdating$() {
   return this.updating$.asObservable();
 }

 setUpdating(isUpdating: boolean) {
   this.updating$.next(isUpdating);
 }

 getBoards$() {
   return this.boards$.asObservable();
 }

 setBoards(boards: Board[]) {
   this.boards$.next(boards);
 }

 addBoard(board: Board) {
   const currentValue = this.boards$.getValue();
   this.boards$.next([...currentValue, board]);
 }

 updateBoard(updatedBoard: Board) {
   const boards = this.boards$.getValue();
   const indexOfUpdated = boards.findIndex(board => board.id === updatedBoard.id);
   boards[indexOfUpdated] = updatedBoard;
   this.boards$.next([...boards]);
 }

 removeBoard(boardRemove: Board) {
   const currentValue = this.boards$.getValue();
   this.boards$.next(currentValue.filter(board => board !== boardRemove));
 }
}