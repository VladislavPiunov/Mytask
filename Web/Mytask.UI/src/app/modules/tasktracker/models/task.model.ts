export class Task {
    constructor(
        public id: string,
        public name: string,
        public boardId: string,
        public stageId: string,
        public description: string,
        public deadline: Date,
        public executor: string
    ) { }

    equals(other: Task): boolean {
        return this.id == other.id && this.name == other.name 
        && this.boardId == other.boardId && this.stageId == other.stageId
        && this.description == other.description && this.deadline == other.deadline
        && this.executor == other.executor;
    }
}