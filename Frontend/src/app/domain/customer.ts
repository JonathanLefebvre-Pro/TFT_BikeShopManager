import { User } from "./user";

export interface Customer {
    id: number;
    userId: number;
    vatNumber: string;
    isActive: boolean;

    user: User;
}
