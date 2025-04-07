import { Product } from "./Product"

export type Order = {
    id: number,
    orderDate: string,
    quantity: number,
    productId: number,
    product: Product
    statusId: number,
    status: {
      id: number,
      status: string
    }
}