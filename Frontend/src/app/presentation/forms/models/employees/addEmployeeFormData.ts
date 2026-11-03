export interface AddEmployeeFormData {
    //Employee
    password: string;
    passwordConfirm: string;
    hiredAt: Date;
    contractEndAt: Date | null;
    
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

    //Role
    roleId: number;
}