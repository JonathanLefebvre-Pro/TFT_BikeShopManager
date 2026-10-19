export interface UpdateCustomerFormData {
    //Customer
    vatNumber: string;
    //User
    firstName: string;
    lastName: string;
    email: string;
    phoneNumber: string;
    address: string;
    city: string;
    zipCode: string;
    country: string;
    birthDate: Date | null;
    allowMarketing: boolean;
}