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
    hiredAt: Date;
    contractEndAt: Date | null; 
    isActive: boolean;

    user: User;

    role: Role;
}