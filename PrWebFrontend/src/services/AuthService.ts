import type IUser from "../types/models/IUser";
import type ILoginRequest from "../types/requests/ILoginRequest";
import type IRegisterRequest from "../types/requests/IRegisterRequest";
import type ILoginResponse from "../types/responses/ILoginResponse";
import type IRegisterResponse from "../types/responses/IRegisterResponse";
import { API_URL } from "../utils/Config";

export const login = async (loginRequest:ILoginRequest):Promise<ILoginResponse> => {
    const response = await fetch(`${API_URL}/auth/login`, {
        method: 'POST',
        headers: {'Content-Type':'application/json'},
        body: JSON.stringify(loginRequest)
    });

    if(!response.ok) {
        throw new Error("Invalid Credentials");
    }

    const data:ILoginResponse = await response.json();
    return data;
} 
export const register = async (registerRequest: IRegisterRequest):Promise<IRegisterResponse> => {
    const response = await fetch(`${API_URL}/auth/register`, {
        method: 'POST',
        headers: {'Content-Type':'application/json'},
        body: JSON.stringify(registerRequest)
    });

    const data:IRegisterResponse = await response.json();
    return data;
}
export const authenticate = (token: string | null, user: IUser | null):boolean => {
    if(token){
        localStorage.setItem('token', token);
        localStorage.setItem('user', JSON.stringify(user));
        return true;
    }
    return false;
}
export const isAuthenticated = ():boolean => {
    return !!localStorage.getItem('token');
}
export const unauthenticate = () => {
    localStorage.removeItem('token');
    localStorage.removeItem('user');
}
export const getToken = () => {
    return localStorage.getItem('token');
}
export const getUser = (): IUser|null => {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
}