import {Supplier} from "./Supplier"

export type Product = {
    id?: number,
    name: string,
    price: number,
    minimumQuantity: number
    supplierId?: number
    supplier?: Supplier
}