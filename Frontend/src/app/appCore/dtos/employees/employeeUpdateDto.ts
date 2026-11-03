export interface EmployeeUpdateDto {
    //Employee
    hiredAt: string;
    contractEndAt: string | null;

    //User
    firstName: string;
    lastName:string;
    email: string | null;
    phoneNumber: string | null;
    address: string | null;
    city: string | null;
    zipCode: string | null;
    country: string | null;
    birthDate: string | null;
    // allowMarketing: boolean;

    //Role
    roleId: number;
}