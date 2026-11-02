import { User } from "./user";

export interface Customer {
    id: number;
    userId: number;
    vatNumber: string | null;
    isActive: boolean;

    user: User;
}
