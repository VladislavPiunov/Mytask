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
}