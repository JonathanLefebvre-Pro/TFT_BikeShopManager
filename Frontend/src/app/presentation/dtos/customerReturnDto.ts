export interface CustomerReturnDto {
    //Customer
    customerId: number;
    vatNumber: string | null;
    isActive: boolean;

    //User
    userId: number;
    firstName: string;
    lastName:string;
    email: string | null;
    phoneNumber: string | null;
    address: string | null;
    city: string | null;
    zipCode: string | null;
    country: string | null;
    birthDate: Date | string | null;
    allowMarketing: boolean;
}