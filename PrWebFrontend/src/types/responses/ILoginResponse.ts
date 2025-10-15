import type IUser from "../models/IUser";

export default interface ILoginResponse{
    readonly token: string;
    readonly user: IUser;
}