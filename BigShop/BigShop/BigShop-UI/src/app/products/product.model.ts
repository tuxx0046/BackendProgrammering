export class Product {
    public id?: number;
    public name: string;
    public price: number;
    public ean: string;
    public weightGram: number;
    public manufacturer_Id: number;
    public category_Id: number;


    constructor(name: string, price: number, ean: string, weightGram: number, manufacturerId: number, categoryId: number, id?: number){
        this.id = id;
        this.name = name;
        this.price = price;
        this.ean = ean;
        this.weightGram = weightGram;
        this.manufacturer_Id = manufacturerId;
        this.category_Id = categoryId;
    }
}
