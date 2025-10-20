import type IUser from "../types/models/IUser";
import type ILoginRequest from "../types/requests/ILoginRequest";
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
     console.log(data.token);
    return data;
} 

export const register = async (formData: FormData):Promise<IRegisterResponse> => {
    try {
        const response = await fetch(`${API_URL}/auth/register`, {
            method: 'POST',
            body: formData
        });
        const data:IRegisterResponse = await response.json();
        return data;
    } catch (error) {
        throw new Error('Error in AuthService: register | ' + error);
    }
}

export const authenticate = (token: string | null, user: IUser | null):boolean => {
    if(token){
        localStorage.setItem('jwtToken', token);
        localStorage.setItem('user', JSON.stringify(user));
        return true;
    }
    return false;
}

export const isAuthenticated = ():boolean => {
    return !!localStorage.getItem('jwtToken');
}

export const unauthenticate = () => {
    localStorage.removeItem('jwtToken');
    localStorage.removeItem('user');
}

export const getToken = () => {
    return localStorage.getItem('jwtToken');
}

export const getUser = (): IUser|null => {
    const user = localStorage.getItem('user');
    return user ? JSON.parse(user) : null;
}