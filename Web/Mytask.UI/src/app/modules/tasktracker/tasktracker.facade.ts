import { Injectable } from "@angular/core";
import { Observable, tap } from "rxjs";
import { BoardApi } from "./api/board.api";
import { TasktrackerState } from "./state/tasktracker.state";
import { Board } from "./models/board.model";

@Injectable({
    providedIn: "root"
})
export class TasktrackerFacade {
    constructor(
        private boardApi: BoardApi,
        private tasktrackerState: TasktrackerState
    ) { }

    getBoards$(): Observable<Board[]> {
        return this.tasktrackerState.getBoards$();
    }

    loadBoards() {
        return this.boardApi.getBoards()
        .pipe(tap(boards => this.tasktrackerState.setBoards(boards))).subscribe();
    }
}