import React, { useContext, useEffect, useState } from "react";

export const AuthContext = React.createContext();
export const useAuth = () => useContext(AuthContext);
export const AuthProvider = ({ children }) => {

    const [isAuthenticated, setIsAuthenticated] = useState(false);

    useEffect(() => {
        const checkAuthentication = async () => {
            try {
                const response = await fetch("/account/me");
                if (response.ok) {
                    setIsAuthenticated(true);
                } else {
                    setIsAuthenticated(false);
                }
            } catch {
                setIsAuthenticated(false)
            }
        };

        checkAuthentication();
    }, [])

    const fetchUser = () => {
        window.location.href = '/account/me';
    }
    const login = () => {
        window.location.href = '/account/login';
    }

    const logout = () => {
        window.location.href = '/account/end-session';
    }

    return (
        <AuthContext.Provider
            value={{
                isAuthenticated,
                fetchUser,
                login,
                logout
            }}
        >
            {children}
        </AuthContext.Provider>
    );
};