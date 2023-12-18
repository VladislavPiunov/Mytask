import { Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { BoardApi } from "./api/board.api";
import { LayoutState } from "./state/layout.state";
import { Board } from "./models/board.model";

@Injectable({
    providedIn: "root"
})
export class LayoutFacade {
    constructor(
        private boardApi: BoardApi,
        private layoutState: LayoutState
    ) { }

    getSelectedBoard$(): Observable<Board> {
        return this.layoutState.getSelectedBoard$();
    }

    getBoards$(): Observable<Board[]> {
        return this.layoutState.getBoards$();
    }

    loadBoards() {
        return this.boardApi.getBoards()
        .pipe(tap(boards => {
            this.layoutState.setBoards(boards);
            this.layoutState.setSelectedBoard(boards[0]);
        })).subscribe();
    }
}