export interface AddEmployeeFormData {
    //Employee
    password: string;
    hiredAt: Date | string;
    contractEndAt: Date | string | null;
    
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