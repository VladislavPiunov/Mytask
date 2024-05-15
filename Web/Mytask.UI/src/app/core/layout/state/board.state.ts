import { Injectable } from "@angular/core";
import { BehaviorSubject, Observable } from "rxjs";
import { Board } from "../models/board.model";

@Injectable({
  providedIn: "root"
})
export class BoardState {
 private updating$ = new BehaviorSubject(false);
 private boards$ = new BehaviorSubject([] as Board[]);
 private selectedBoard$ = new BehaviorSubject(new Board("","Mytask","",[],[]))

 isUpdating$(): Observable<boolean> {
   return this.updating$.asObservable();
 }

 setUpdating(isUpdating: boolean): void {
   this.updating$.next(isUpdating);
 }

 getSelectedBoard$(): Observable<Board> {
  return this.selectedBoard$.asObservable();
 }

 setSelectedBoard(board: Board): void {
  this.selectedBoard$.next(board);
 }

 getBoards$(): Observable<Board[]> {
   return this.boards$.asObservable();
 }

 setBoards(boards: Board[]): void {
   this.boards$.next(boards);
 }

 addBoard(board: Board): void {
   const currentValue = this.boards$.getValue();
   this.boards$.next([...currentValue, board]);
 }

 updateBoard(updatedBoard: Board): void {
   const boards = this.boards$.getValue();
   const indexOfUpdated = boards.findIndex(board => board.id === updatedBoard.id);
   boards[indexOfUpdated] = updatedBoard;
   this.boards$.next([...boards]);
 }

 removeBoard(boardRemove: Board): void {
   const currentValue = this.boards$.getValue();
   this.boards$.next(currentValue.filter(board => board !== boardRemove));
 }
}