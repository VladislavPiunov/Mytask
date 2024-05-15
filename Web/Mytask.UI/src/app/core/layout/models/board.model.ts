export class Board {
  constructor(
    public id: string, 
    public name: string,
    public ownerId: string,
    public stages: string[],
    public users: string[] 
  ) {}

}