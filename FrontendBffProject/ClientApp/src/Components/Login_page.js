import React from 'react';
import LoginLogo from './Login_logo';
import './Login_page.css';
import { useAuth } from '../Context/AuthContext';
import { useNavigate } from 'react-router-dom';

const Login_page = () => {
    const { login } = useAuth();
    const navigate = useNavigate();

    const handleLogin = async () => {
        await login();
        navigate("/main")
    };



    return (
        <div className="login-page">
            <div className='login-page-header'>
                <LoginLogo />
                <h1 className="title">sipster</h1>
            </div>
            <div className='login-page-input'>
                <button className='login-button' onClick={handleLogin}>Login</button>
            </div>
        </div>
    );
};

export default Login_page;
