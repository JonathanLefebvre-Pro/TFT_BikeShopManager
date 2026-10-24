import { Role } from "./role";
import { User } from "./user";

export interface Employee
{
    id: number;
    userId: number;
    passwordHash: string;
    securityStamp: string; 
    //accessFailedCount: number | null;
    //lockoutEnd: Date | null;
    hiredAt?: Date | string;
    contractEndAt?: Date | string | null; 
    isActive: boolean;

    user: User;

    role: Role;
}