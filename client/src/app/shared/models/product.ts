export type Product = {
    id: number;
    name: string;
    description: string;
    price: number;
    pictureUrl: string;
    type: string;
    brand: string;
    model: string;
    make:string;
    quantityInStock: number;
    year: number;
    fuelType: string;
    transmissionType: string;
    kilometers:number;
    ownerNumber:number;
    location:string;
    postingDate: Date;
    brandId: number;
    typeId: number;
    makeId: number;
    modelId: number;
}