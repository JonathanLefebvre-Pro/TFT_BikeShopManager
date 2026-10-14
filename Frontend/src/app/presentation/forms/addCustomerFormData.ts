export interface AddCustomerFormData {
    firstName: string;
    lastName: string;
    vatNumber: string;
    email: string;
    phoneNumber: string;
    address: string;
    city: string;
    zipCode: string;
    country: string;
    birthDate: Date | null;
    allowMarketing: boolean;
}