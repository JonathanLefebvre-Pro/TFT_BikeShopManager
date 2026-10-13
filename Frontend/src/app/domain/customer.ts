import { User } from "./user";

export interface Customer {
    Id: number;
    UserId: number;
    VatNumber: string;
    IsActive: boolean;

    User: User;
}
