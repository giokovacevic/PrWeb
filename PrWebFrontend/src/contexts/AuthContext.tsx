import { createContext, useContext, type ReactNode } from "react"
import type IUser from "../types/models/IUser"

type AuthContextType = {
    token: string,
    user: IUser,
    //handleLogin: (token: string, user: IUser) => void,
    //handleLogout: () => void;
 }

 const AuthContext = createContext<AuthContextType | undefined>(undefined);

 export const AuthProvider = ({children}:{children: ReactNode}) => {

 }

 export const useAuth = () => {
    const context = useContext(AuthContext);
    if(!context) {
        throw new Error("useAuth must be used inside AuthProvider.");
    }
    return context;
 }