import { useFormik } from "formik"
import { useState } from "react";
import { Product } from "../../Types/Product";
import { SupplierToAdd } from "../../Types/SupplierToAdd";
import DeleteOutlineIcon from '@mui/icons-material/DeleteOutline';

import './Login_SignUp.css'
import axios from "axios";
import { useNavigate } from "react-router-dom";
import { Supplier } from "../../Types/Supplier";
import { Button, Dialog, DialogContent, DialogTitle, Typography } from "@mui/material";

export default function Login_SignUp() {
    const [spinning, setSpinning] = useState(false);
    const [adminDialogOpen,setAdminDialogOpen] =  useState(false);
    const [password, setPassword] = useState('');
    const [errorPassword,setErrorPasword] = useState('');
    const [newProduct, setNewProduct] = useState<Product>({
        name: '',
        price: 0,
        minimumQuantity: 1,
    });
    const navigate = useNavigate();
    const LoginCheckValidate = (values: any) => {
        const errors: any = {};
        if (values.phone == '' || !values.phone)
            errors.phone = 'Required'
        else if (values.phone.length != 10 || !/^\d{1,10}$/.test(values.phone))
            errors.phone = 'Phone Number Invalid'
        return errors;

    }

    const LoginSubmit = (value: any) => {
        const res = axios.post("https://localhost:44388/api/Supplier/Login", LoginFormik.values.phone, {
            headers: {
                "Content-Type": "application/json"
            }
        }).then(
            function (response) {
                console.log(response);
                const supplier = response.data;
                navigate("/orders", { state: { supplier } });
            }
        ).catch(
            function (error) {
                console.log(error);
            }
        );


    }

    const LoginFormik = useFormik(
        {
            initialValues: { phone: '' },
            validate: LoginCheckValidate,
            onSubmit: LoginSubmit,
        })

    const SignUpCheckValidate = (values: any) => {
        const errors: any = {};


        if (values.name == '' || !values.name)

            errors.name = "Required"
        else if (values.name.length < 2)
            errors.name = 'must by 2 char'

        if (values.agent == '' || !values.agent)
            errors.agent = 'Required'
        else if (values.agent.length < 2)
            errors.agent = 'must by 2 char'

        if (values.phone == '' || !values.phone)
            errors.phone = 'Required'
        else if (values.phone.length != 10 || !/^\d{1,10}$/.test(values.phone))
            errors.phone = 'phone number invalid'
        return errors;

    }

    const SignUpSubmit = (values: any) => {

        const supplier: SupplierToAdd = {
            name: SignUpFormik.values.name,
            agent: SignUpFormik.values.agent,
            phone: SignUpFormik.values.phone,
            product: SignUpFormik.values.product
        };

        const res = axios.post("https://localhost:44388/api/Supplier/AddSupplier", supplier, {
            headers: {
                "Content-Type": "application/json"
            }
        }).then(
            function (response) {
                console.log(response);
                const newSupplier :Supplier = {
                    id : response.data.id,
                    name: supplier.name,
                    agent:supplier.agent,
                    phone:supplier.phone,
                }

                navigate("/orders", { state: { supplier } });
            }
        ).catch(
            function (error) {
                console.log(error);
            }
        );

    }


    const SignUpFormik = useFormik<SupplierToAdd>({
        initialValues: { name: '', agent: '', phone: '', product: [] },
        validate: SignUpCheckValidate,
        onSubmit: SignUpSubmit,
    })

    return (
        <>

            <div className="spinning-curtain" style={{ display: spinning ? "flex" : "none" }}>
                <div className="lds-dual-ring" />
            </div>
            <div>
                <div style={{ marginTop: 20, display: 'flex', justifyContent: 'flex-start', marginLeft: 20 }}>
                    <Button onClick={() => {setAdminDialogOpen(true) }} variant="contained">
                        Administrator login
                    </Button>
                </div>

                <h6>Login / Sign Up</h6>
                <div className="forms-grid">
                    <div className="form-wrapper cont template d-flex justify-content-center align-items-center my-5 cascading-right" style={{ background: 'hsla(0, 0%, 100%, 0.55)', backdropFilter: 'blur(30px)' ,display: 'flex', justifyContent: 'center', alignItems: 'center'}}>
                        <br></br>
                        <div className='form_container rounded p-2 shadow-5 text-center'>
                            <h1 className='text-center'> Login </h1>
                            <br></br>

                            <form onSubmit={LoginFormik.handleSubmit} className="was-validated">


                                <div className='col-md'>
                                    <div className="form-group">
                                        <label htmlFor="phone">Phone</label>
                                        <input className="form-control"
                                            id="phone"
                                            name="phone"
                                            type="phone"
                                            required
                                            onChange={LoginFormik.handleChange}
                                            value={LoginFormik.values.phone} />
                                    </div>
                                    {LoginFormik.errors.phone ? <div className="alert alert-danger p-1 fs-6">{LoginFormik.errors.phone}</div> : ''}
                                </div>


                                <br />
                                <div className="form-group">
                                    <button type="submit" className="btn btn-primary">Login</button>
                                </div>

                            </form>
                            <br></br>

                        </div>
                    </div>
                    <div className="form-wrapper cont template d-flex justify-content-center align-items-center my-5 cascading-right" style={{ background: 'hsla(0, 0%, 100%, 0.55)', backdropFilter: 'blur(30px)',display: 'flex', justifyContent: 'center', alignItems: 'center' }}>
                        <br></br>
                        <div className='form_container rounded p-2 shadow-5 text-center'>
                            <h1 className='text-center'> Sign Up</h1>
                            <br></br>

                            <form onSubmit={SignUpFormik.handleSubmit} className="was-validated">

                                <div className='row'>
                                    <div className='col-md'>
                                        <div className="form-group">
                                            <label htmlFor="name">Company Name</label>
                                            <input className="form-control"
                                                placeholder='Enter Company Name'
                                                id="name"
                                                name="name"
                                                type="name"
                                                required
                                                minLength={2}
                                                onChange={SignUpFormik.handleChange}
                                                value={SignUpFormik.values.name} />
                                        </div>
                                        {SignUpFormik.errors.name ? <div className="alert alert-danger p-1 fs-6">{SignUpFormik.errors.name}</div> : ''}
                                    </div>
                                    <div className='col-md'>
                                        <div className="form-group">
                                            <label htmlFor="agent">Agent</label>
                                            <input className="form-control"
                                                placeholder='Enter Agent'
                                                id="agent"
                                                name="agent"
                                                type="agent"
                                                required
                                                minLength={2}
                                                onChange={SignUpFormik.handleChange}
                                                value={SignUpFormik.values.agent} />
                                        </div>
                                        {SignUpFormik.errors.agent ? <div className="alert alert-danger p-1 fs-6">{SignUpFormik.errors.agent}</div> : ''}
                                    </div>
                                </div>
                                {/* <div className='row'> */}

                                <div className='col-md'>
                                    <div className="form-group ">
                                        <label htmlFor="phone">Phone</label>
                                        <input className="form-control"
                                            id="phone"
                                            name="phone"
                                            type="phone"
                                            // country="IL"

                                            required
                                            onChange={SignUpFormik.handleChange}
                                            value={SignUpFormik.values.phone} />
                                    </div>
                                    {SignUpFormik.errors.phone ? <div className="alert alert-danger p-1 fs-6">{SignUpFormik.errors.phone}</div> : ''}

                                    {/* </div> */}
                                </div>
                                <hr />
                                <h5>Add Products</h5>
                                <div className='row'>
                                    <div className='col-md-4'>
                                        <label htmlFor="name">Product Name</label>
                                        <input className="form-control mb-2" id="name" placeholder="Product Name" value={newProduct.name} onChange={(e) => setNewProduct({ ...newProduct, name: e.target.value })} />
                                    </div>
                                    <div className='col-md-4'>
                                        <label htmlFor="price">Price</label>
                                        <input className="form-control mb-2" id="price" placeholder="Price" type="number" value={newProduct.price} onChange={(e) => setNewProduct({ ...newProduct, price: parseFloat(e.target.value) })} />
                                    </div>
                                    <div className='col-md-4'>
                                        <label htmlFor="minimumQuantity">Minimum Quantity</label>
                                        <input className="form-control mb-2" id="minimumQuantity" placeholder="Minimum Quantity" type="number" value={newProduct.minimumQuantity} onChange={(e) => setNewProduct({ ...newProduct, minimumQuantity: parseInt(e.target.value) })} />
                                    </div>
                                </div>
                                <br />
                                <div className="text-end mb-3">
                                    <button type="button" className="btn btn-success" onClick={() => {
                                        if (newProduct.name && newProduct.price > 0) {
                                            SignUpFormik.setFieldValue('product', [...SignUpFormik.values.product, newProduct]);
                                            setNewProduct({ name: '', price: 0, minimumQuantity: 1 });
                                        }
                                    }}>
                                        + Add Product
                                    </button>
                                </div>

                                {SignUpFormik.values.product.length > 0 && (
                                    <div className="table-responsive">
                                        <table className="table table-bordered">
                                            <thead className="table-light">
                                                <tr>
                                                    <th>Name</th>
                                                    <th>Price</th>
                                                    <th>Min Qty</th>
                                                </tr>
                                            </thead>
                                            <tbody>
                                                {SignUpFormik.values.product.map((p, index) => (
                                                    <tr key={index}>
                                                        <td>{p.name}</td>
                                                        <td>{p.price}</td>
                                                        <td>{p.minimumQuantity}</td>
                                                        <td>
                                                            <button type="button" className="btn btn-danger btn-sm" onClick={() => SignUpFormik.setFieldValue('product', SignUpFormik.values.product.filter((_, i) => i !== index))}>
                                                                <DeleteOutlineIcon />
                                                            </button>
                                                        </td>
                                                    </tr>
                                                ))}
                                            </tbody>
                                        </table>
                                    </div>
                                )}

                                <br />
                                <div className="form-group">
                                    <button type="submit" className="btn btn-primary">Sing Up</button>
                                </div>

                            </form>
                            <br></br>
                        </div>
                    </div>
                </div>
            </div>
             <Dialog open={adminDialogOpen} onClose={() => {setAdminDialogOpen(false); setErrorPasword(''); setPassword('')}}>
                            <DialogTitle>Administrator login</DialogTitle>
                            <DialogContent>
                                
                                        <label htmlFor="password">Password</label>
                                        <input className="form-control mb-2" id="password" type="password" placeholder="password" value={password} onChange={(e) => {setPassword( e.target.value ); }} />
                                        {errorPassword.length>0 && <Typography color="red" variant="body1">{errorPassword}</Typography>}
                                        <Button variant="contained"  sx={{ mt: 2 }} onClick={() => {
                                            if(password=="1234"){
                                                setAdminDialogOpen(false);
                                                setErrorPasword('');
                                                setPassword('');
                                                navigate('/admin');

                                            }
                                            else {
                                                setErrorPasword("Incorrect password - Access denied");
                                                
                                            }
                                        }}>
                                            Login
                                        </Button>
                                    
                            </DialogContent>
                        </Dialog>
        </>
    )
}