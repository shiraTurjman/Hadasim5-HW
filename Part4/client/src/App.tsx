import React from 'react';
import logo from './logo.svg';
import './App.css';
import { Route, Router, Routes } from 'react-router-dom';
import Login_SignUp from './Components/Login_SignUp/Login_SignUp';
import Orders from './Components/Orders';
import Header from './Components/Header/Header';
import AddOrder from './Components/AddOrder/AddOrder';
import MyOrders from './Components/MyOrders/MyOrders';

function App() {
  return (
    <div className="App">
      <Routes>
        <Route path='/' element={<Login_SignUp/>}/>
        <Route path='/orders' element={<Orders/>}/>
        <Route path='/admin' element={<Header/>}>
            <Route index element={<Orders/>}/>
            <Route path='myOrder' element={<MyOrders/>}/>
            <Route path='addOrder' element={<AddOrder/>}/>
            
            <Route path='orders/:id' element={<Orders/>}/>
        </Route>
      </Routes>
    </div>
  );
}

export default App;
