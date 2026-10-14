import { CustomerDto } from "./customerDto";
import { UserDto } from "./userDto";

export interface AddCustomerDto {
    user: UserDto;
    customer: CustomerDto;
}