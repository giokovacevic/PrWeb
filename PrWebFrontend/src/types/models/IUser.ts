import type IRole from "./IRole";

export default interface IUser{
    readonly id: number;
    readonly username: string;
    readonly email: string;
    readonly imageUrl: string | null;
    readonly role: IRole;
}