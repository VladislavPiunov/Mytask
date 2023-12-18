import { Injectable } from "@angular/core";
import { BehaviorSubject } from "rxjs";
import { Board } from "../models/board.model";

@Injectable({
  providedIn: "root"
})
export class LayoutState {
 private updating$ = new BehaviorSubject(false);
 private boards$ = new BehaviorSubject([] as Board[]);
 private selectedBoard$ = new BehaviorSubject(new Board("","Mytask","",[],[]))

 isUpdating$() {
   return this.updating$.asObservable();
 }

 setUpdating(isUpdating: boolean) {
   this.updating$.next(isUpdating);
 }

 getSelectedBoard$() {
  return this.selectedBoard$.asObservable();
 }

 setSelectedBoard(board: Board) {
  this.selectedBoard$.next(board);
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