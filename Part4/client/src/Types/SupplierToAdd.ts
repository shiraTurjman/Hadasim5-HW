import {Product} from './Product'

export type SupplierToAdd = {
    id?:number,
    name: string,
    phone: string,
    agent: string,
    product: Product[]
}