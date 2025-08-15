import { UserAccount } from "./UserAccount";

export interface ILoginResponse {
    message: string;
    cookie: string;
    //cookieValue: string;
    role: string;
    user: UserAccount;
}