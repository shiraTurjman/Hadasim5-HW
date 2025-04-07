import { Order } from "./Order"


export type Supplier = {
    id?:number,
    name: string,
    phone: string,
    agent: string,
    // orders: Order[]
}