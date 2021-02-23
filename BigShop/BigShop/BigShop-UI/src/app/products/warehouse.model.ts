export class Warehouse {
    public id: number;
    public name: string;
    public addressLane: string;
    public zip_Id: number;


    constructor(id: number, name: string, addressLane: string, zipId: number){
        this.id = id;
        this.name = name;
        this.addressLane = addressLane;
        this.zip_Id = zipId;
    }
}