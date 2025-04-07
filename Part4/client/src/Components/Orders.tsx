import { Button, Card, CardActions, CardContent, CardMedia, Grid, Typography } from "@mui/material"
import { Supplier } from "../Types/Supplier"
import { useEffect, useState } from "react";
import axios from "axios";
import { Order } from "../Types/Order";
import { useLocation, useNavigate, useParams } from "react-router-dom";

const colorMap: Record<string, string> = {
    error: '#f44336',
    success: '#4caf50',
    primary: '#1976d2',
    inherit: 'inherit',
  };

export default function Orders(){

    const [orders, setOrders] = useState<Order[]>([]);
    const [loading, setLoading] = useState(false);
    const location = useLocation();
    const [supplier,setSupplier]=useState(location.state?.supplier as Supplier);
    const prams = useParams();
    const [statusId,setStatusId] = useState(prams.id);
    const navigate =useNavigate();

    const getOrders = () =>{
        setLoading(true);
        if(supplier){
            // console.log("supplier get orders");
        axios.get(`https://localhost:44388/api/Order/GetOrderBySupplier/${supplier?.id}`)
          .then(res => {
            const orders = res.data;
            // console.log("supplier get orders..."+orders);
            setOrders(orders);
             setLoading(false);
          })
        }
        else if(prams.id){
            // console.log("status get orders");
            axios.get(`https://localhost:44388/api/Order/GetOrderByStatus/${prams.id}`)
          .then(res => {
            const orders = res.data;
            // console.log("status get orders...."+orders);
            setOrders(orders);
             setLoading(false);
          })
        }else{
            // console.log("all get orders");
            axios.get(`https://localhost:44388/api/Order/GetAllOrder`)
          .then(res => {
            const orders = res.data;
            // console.log("all get orders..."+orders);
            setOrders(orders);
             setLoading(false);
          })
        }
    }

    useEffect(() => {
        console.log("use effect");
        getOrders();
      }, [])

      useEffect(() => { 
        // setStatusId(prams.id);
        console.log("use effect prams"+prams.id);
        getOrders();
      }, [prams])

    //   useEffect(() => {
    //      console.log("Updated state:", orders);
    //     setLoading(false);
    // }, [orders]);
      const OrderDisplay = (orderDate : string ) => {
        const date = new Date(orderDate);
      
        return date.toLocaleDateString('he-IL');
      };

      const getColor = (id:number) => {
        switch (id) {
          case 1:
            return 'error'; // אדום
          case 2:
            return 'success'; // ירוק
          case 3:
            return 'primary'; // כחול
          default:
            return 'inherit'; // ברירת מחדל
        }
      };

      const updateStatuse = (id:number,statusId:number) => {
          
        axios.put(`https://localhost:44388/api/Order/UpdateOrder/${id}/${statusId}`)
        .then(res =>{
            console.log(res)
            getOrders();
        }).catch(err=>{
            console.log(err)
        });

      }

    return (
    <>
    {supplier && <>
        <div className="btn-navbar mouse-cursor " style={{width: 100}} onClick={()=>{navigate("/")}}>
                        Logout
                    </div>
    <h1>Wellcome {supplier && supplier.name} - {supplier.agent}</h1>
    </>}
        
        <h3>Orders:</h3>
        {(orders && orders.length > 0 ) ? 
        <>
        
        <Grid container spacing={3} justifyContent="center">
        {orders.map((order) => (
        //   <Grid item xs={12} sm={6} md={4}>
            <Card
              key={order.id}
              sx={{
                border: "1px solid blue",
                borderRadius: "16px",
                boxShadow: "0px 0px 10px rgba(0, 0, 255, 0.2)",
                textAlign: "center",
                p: 2,
              }}
            >
              <CardContent>
                <Typography variant="h6" fontWeight="bold">
                  {order.product.name}
                </Typography>
                {!supplier &&
                <Typography
                  variant="body1"
                  color="text.secondary"
                  sx={{ my: 1 }}
                >
                  Supplier: {order.product.supplier?.name} - {order.product.supplier?.agent}
                </Typography>
                    }
                 
                <Typography
                  variant="body2"
                  color="text.secondary"
                  sx={{ my: 1 }}
                >
                 Date: {OrderDisplay(order.orderDate)}
                </Typography>
                <Typography variant="body1" fontWeight="bold">
                    Quantity: {order.quantity}
                </Typography>
              </CardContent>
              <CardActions sx={{ justifyContent: "center" }}>
                {/* <Button variant="contained" size="small" onClick={()=>{setBuyDialogOpen(true); SetSelectedProduct(product);}}>
                  Buy
                </Button> */}
                 <Button variant="contained" color={getColor(order.statusId)} disabled={(supplier && order.statusId!=1) || (!supplier && order.statusId !=2)} onClick={() => { updateStatuse(order.id,order.statusId+1)}} 
                    sx={{ 
                                mt: 1, 
                                borderRadius: "20px",
                                '&.Mui-disabled': {
                                    color:  colorMap[getColor(order.statusId)], 
                                    },
                            }} >
                    {order.status.status}
                    </Button>
              </CardActions>
            </Card>
        //   </Grid>
        ))}
      </Grid>
        

        
        </>
          
         : 
         loading ? 
         <>
         loading....
         </> 
         :
        <>
        <h5>You currently have no orders.... </h5>
        <button onClick={()=>getOrders()}>Check again</button>
        </>}

    </>
    )
}