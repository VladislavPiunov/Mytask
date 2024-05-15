import { Observable } from "rxjs";
import { Stage } from "./stage.model";
import { Task } from "./task.model";
import { Board } from "src/app/core/layout/models/board.model";

export interface CreateDialogData {
    stages: Observable<Stage[]>,
    board: Observable<Board>,
    stageId: string
}

export interface EditDialogData {
    stages: Observable<Stage[]>,
    board: Observable<Board>,
    task: Task
}