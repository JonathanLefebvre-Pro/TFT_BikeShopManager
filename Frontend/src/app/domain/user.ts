export interface User {
    id: number;
    firstName: string;
    lastName:string;
    email: string | null;
    phoneNumber: string | null;
    address: string | null;
    city: string | null;
    zipCode: string | null;
    country : string | null;
    birthDate?: Date | null;
    allowMarketing: boolean;
}
