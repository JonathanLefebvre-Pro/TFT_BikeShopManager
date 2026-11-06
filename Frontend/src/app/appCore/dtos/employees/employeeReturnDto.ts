export interface EmployeeReturnDto {
    //Employee
    employeeId: number;
    passwordHash: string;
    securityStamp: string;
    hiredAt: Date;
    contractEndAt: Date | null;
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

    //Role
    roleId: number;
    roleName: string;
    normalizedRoleName: string;
}