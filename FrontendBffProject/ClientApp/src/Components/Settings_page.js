import React from 'react';
import LoginLogo from './Login_logo';
import './Login_page.css';
import { useAuth } from '../Context/AuthContext';
import { useNavigate } from 'react-router-dom';

const SettingsPage = () => {

    const { logout, isAuthenticated } = useAuth();
    const navigate = useNavigate();

    const handleLogout = async () => {
        await logout();
        navigate("/")
    };

    const handleLogoutClick = () => {
        handleLogout();
    };

    return (
        <div className="login-page">
            <div className='login-page-header'>
                <LoginLogo />
                <h1 className="title">sipster</h1>
            </div>
            {isAuthenticated &&
                <div className='login-page-input'>
                    <button className='login-button' onClick={handleLogoutClick}>Logout</button>
                </div>
            }
        </div>
    );
};

export default SettingsPage;