import { Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { BoardApi } from "./api/board.api";
import { BoardState } from "./state/board.state";
import { Board } from "./models/board.model";

@Injectable({
    providedIn: "root"
})
export class LayoutFacade {
    constructor(
        private boardApi: BoardApi,
        private boardState: BoardState
    ) { }

    getSelectedBoard$(): Observable<Board> {
        return this.boardState.getSelectedBoard$();
    }

    getBoards$(): Observable<Board[]> {
        return this.boardState.getBoards$();
    }

    loadBoards(): void {
        this.boardApi.getBoards().pipe(
            tap(boards => {
                this.boardState.setBoards(boards);
                this.boardState.setSelectedBoard(boards[0]);
            })
        ).subscribe();
    }

    updateBoard(updatedBoard: Board): void {
        this.boardApi.updateBoard(updatedBoard).pipe(
            tap(board => {
                this.boardState.updateBoard(board);
            })
        ).subscribe();
    }
}