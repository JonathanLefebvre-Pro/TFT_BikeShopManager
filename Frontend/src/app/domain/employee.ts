import { User } from "./user";

export interface Employee
{
    id: number;
    userId: number;
    passwordHash: string;
    securityStamp: string; 
    accessFailedCount: number;
    lockoutEnd: Date;
    hiredAt: Date;
    contractEndAt: Date; 
    isActive: boolean;

    user: User;
}