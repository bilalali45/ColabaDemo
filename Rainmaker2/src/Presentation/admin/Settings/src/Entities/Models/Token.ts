export class Tokens {
    public id?: number;
    public name?: string;
    public symbol?: string;
    public description?: string;

    constructor(id?: number, name?: string, symbol?: string, description?: string){
      this.id = id;
      this.name = name;
      this.symbol = symbol;
      this.description = description;
    }
}