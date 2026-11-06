import { Role } from "./role";
import { User } from "./user";

export interface Employee
{
    id: number;
    userId: number;

    hiredAt: Date;
    contractEndAt: Date | null; 
    isActive: boolean;

    user: User;
    role: Role;
}