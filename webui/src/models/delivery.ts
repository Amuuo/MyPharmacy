export type Delivery = {
    Id: number;
    WarehouseId: number;
    PharmacyId: number;
    DrugName: string;
    UnitCount: number;
    UnitPrice: number;
    TotalPrice?: number;
    DeliveryDate: Date;    
}