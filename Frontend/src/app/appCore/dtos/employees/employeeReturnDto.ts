export interface EmployeeReturnDto {
    //Employee
    employeeId: number;
    passwordHash: string;
    securityStamp: string;
    hiredAt: Date | string;
    contractEndAt: Date | string | null;
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
    birthDate: Date | null;
    allowMarketing: boolean;

    //Role
    roleId: number;
    roleName: string;
    normalizedRoleName: string;
}