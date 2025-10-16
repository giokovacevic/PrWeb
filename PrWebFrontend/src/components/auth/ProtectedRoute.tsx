import { useEffect, type JSX } from "react";
import { isAuthenticated } from "../../services/AuthService";
import { useAuth } from "../../contexts/AuthContext";
import { Navigate } from "react-router-dom";
import Loading from "../loading/Loading";

type ProtectedRouteProps = {
    children: JSX.Element;
    requiredRole: string;
};

const ProtectedRoute = ({children, requiredRole}:ProtectedRouteProps) => {
    const {token, user, handleLogout, loading} = useAuth();

    useEffect(() => {
        if(!isAuthenticated() && token) {
            handleLogout();
        }
    }, []);

    if(loading) {
        return <Loading></Loading>;
    }

    if(!isAuthenticated() || !token) {
        return (<Navigate to="/" replace/>);

    }

    if(user?.role.name !== requiredRole) {
        return (<Navigate to={"/" + user?.role.name} replace/>);
    }

    return children;
}
export default ProtectedRoute;