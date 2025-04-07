import {
    Alert,
    Button,
    Card,
    CardActions,
    CardContent,
    Dialog,
    DialogContent,
    DialogTitle,
    Grid,
    Snackbar,
    Typography,
} from "@mui/material";
import axios from "axios";
import { useEffect, useState } from "react";
import { Product } from "../../Types/Product";

export default function AddOrder() {
    const [products, setProducts] = useState<Product[]>([]);
    const [loading, setLoading] = useState(false);
    const [buyDialogOpen, setBuyDialogOpen] = useState(false);
    const [quantity, setQuantity] = useState(0);
    const [errorQuantity, setErrorQuantity] = useState('');
    const [selectedProduct, SetSelectedProduct] = useState<Product>();
    const [showSuccess, setShowSuccess] = useState(false);
    const [selectedSupplier, setSelectedSupplier] = useState<string>('all');

    useEffect(() => {
        setLoading(true);
        axios
            .get(`https://localhost:44388/api/Product/GetAllProduct`)
            .then((res) => {
                setProducts(res.data);
                setLoading(false);
            });
    }, []);

    const addOrder = () => {
        if (!selectedProduct || quantity <= 0 || quantity < selectedProduct.minimumQuantity) {

            alert('Error: No product selected or quantity does not meet the minimum quantity.');

        }
        else {
            const OrderToAdd = {
                quantity: quantity,
                productId: selectedProduct?.id
            }
            axios.post("https://localhost:44388/api/Order/AddOrder", OrderToAdd).then(res => {
                setBuyDialogOpen(false);
                setShowSuccess(true);
            })
        }

        setQuantity(0);
        SetSelectedProduct(undefined);
    }

    const uniqueSuppliers = Array.from(new Set(products.map(p => p.supplier?.name).filter(Boolean)));

    return (
        <>
            <div style={{ display: 'flex', justifyContent: 'center', alignItems: 'center', gap: 10, marginBottom: 20 }}>
                <label style={{ fontWeight: 'bold' }}>Filter by Supplier:</label>
                <select
                    value={selectedSupplier}
                    onChange={(e) => setSelectedSupplier(e.target.value)}
                    style={{
                        padding: '8px 12px',
                        borderRadius: '8px',
                        border: '1px solid #ccc',
                        backgroundColor: '#f9f9f9',
                        cursor: 'pointer',
                        maxWidth:200
                    }}
                >
                    <option value="all">All</option>
                    {uniqueSuppliers.map((supplierName, index) => (
                        <option key={index} value={supplierName}>{supplierName}</option>
                    ))}
                </select>
            </div>

            <Grid container spacing={3} justifyContent="center">
                {products.filter(p => selectedSupplier === 'all' || p.supplier?.name === selectedSupplier)
                    .map((product) => (
                        //   <Grid item xs={12} sm={6} md={4}>
                        <Card
                            key={product.id}
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
                                    {product.name}
                                </Typography>
                                <Typography
                                    variant="body1"
                                    color="text.secondary"
                                    sx={{ my: 1 }}
                                >
                                    Supplier: {product.supplier?.name} - {product.supplier?.agent}
                                </Typography>
                                <Typography
                                    variant="body2"
                                    color="text.secondary"
                                    sx={{ my: 1 }}
                                >
                                    Minimum Quantity: {product.minimumQuantity}
                                </Typography>
                                <Typography variant="body1" fontWeight="bold">
                                    ₪{product.price}
                                </Typography>
                            </CardContent>
                            <CardActions sx={{ justifyContent: "center" }}>
                                <Button variant="contained" size="small" onClick={() => { setBuyDialogOpen(true); SetSelectedProduct(product); }}>
                                    Buy
                                </Button>
                            </CardActions>
                        </Card>
                        //   </Grid>
                    ))}
            </Grid>
            <Dialog open={buyDialogOpen} onClose={() => { setBuyDialogOpen(false); setErrorQuantity(''); setQuantity(0) }}>
                <DialogTitle>Buy {selectedProduct?.name}</DialogTitle>
                <DialogContent>

                    <label htmlFor="quantity">Quantity</label>
                    <input className="form-control mb-2" id="quantity" type="number" placeholder="quantity" value={quantity} onChange={(e) => { setQuantity(Number(e.target.value)); }} />
                    {errorQuantity.length > 0 && <Typography color="red" variant="body1">{errorQuantity}</Typography>}
                    <Button variant="contained" sx={{ mt: 2 }} onClick={() => {
                        // if(password=="1234"){
                        //     setAdminDialogOpen(false);
                        //     setErrorPasword('');
                        //     setPassword('');
                        //     navigate('/admin');

                        // }
                        // else {
                        //     setErrorPasword("Incorrect password - Access denied");

                        // }
                        if (selectedProduct?.minimumQuantity && quantity >= selectedProduct?.minimumQuantity) {
                            addOrder();
                            setErrorQuantity('');

                            // setSelectedProduct();
                        }
                        else {
                            setErrorQuantity('The quantity does not meet the minimum quantity.')
                        }
                    }}>
                        Buy
                    </Button>

                </DialogContent>
            </Dialog>
            <Snackbar
                open={showSuccess}
                autoHideDuration={3000} // כמה זמן להציג (במילישניות)
                onClose={() => setShowSuccess(false)}
                anchorOrigin={{ vertical: 'top', horizontal: 'center' }} // מיקום על המסך
            >
                <Alert onClose={() => setShowSuccess(false)} severity="success" sx={{ width: '100%' }}>
                    The order was placed successfully!
                </Alert>
            </Snackbar>
        </>
    );
}
