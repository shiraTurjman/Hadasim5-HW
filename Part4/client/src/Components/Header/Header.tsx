
import { useEffect, useState } from "react";
import { Link, Outlet, useNavigate } from "react-router-dom";
import ArrowDropDownRoundedIcon from '@mui/icons-material/ArrowDropDownRounded';
import axios from "axios";

export default function Header() {
    const [scrolling, setScrolling] = useState(false);
    const navigate = useNavigate();
    const [status, setStatus] = useState<{ id: number, status: string }[]>([]);

    useEffect(() => {
        axios.get("https://localhost:44388/api/Status/GetAllStatus").then(res => {
            const status = res.data;
            setStatus(status);
            console.log(status);
        })
    }, [])

    return(
        <>
    <div
            style={{
                zIndex: 3000,
                position: "absolute",
            }}
        >
            <div className={`header-body ${scrolling ? "white" : ""}`}>
            
                <div className="justify-center">
             
                <div className="btn-navbar mouse-cursor" onClick={()=>{navigate("/")}}>
                        Logout
                    </div>
                    <div className="flex-center">

                        <>

                            <Link className="btn-underLine" to="/admin/addOrder">
                                <div className="btn-navbar mouse-cursor">
                                    Add Order
                                </div>
                            </Link>

                            <div className="btn-navbar mouse-cursor dropdown" >
                                <div onClick={() => navigate('/admin')}>
                                    <span>My Orders</span>
                                    <ArrowDropDownRoundedIcon />
                                </div>
                                <div className="dropdown-content">
                                    {status.map((item) => {
                                        return <Link key={item.id} className="btn-underLine" to={"/admin/orders/" + item.id}>
                                            <div className="menu-txt mouse-cursor">
                                                {item.status}
                                            </div>
                                        </Link>
                                    })}

                                </div>
                            </div>
                        </>
                    </div>

                </div>
            </div>
        </div>

        <div style={{ marginTop: '100px' }}>
                <Outlet />
        </div>
        </>

)
}