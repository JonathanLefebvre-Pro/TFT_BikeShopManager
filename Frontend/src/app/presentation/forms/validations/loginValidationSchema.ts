import { maxLength, minLength, pattern, required, schema } from "@angular/forms/signals";

const loginMinChar: number = 6;
const loginMaxChar: number = 254;
export const loginValidationSchema = schema<string>((login) => {
    //Validation : Login
    required(login, { message: "The login is required !" }),
    pattern(login, /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$/, { message: "Enter a valid email !" }),
    minLength(login, loginMinChar, { message: `Login min length : ${loginMinChar} !` }),
    maxLength(login, loginMaxChar,  { message: `Login max length : ${loginMaxChar} !` })
});

const paswordMinChar: number = 5;
const passwordMaxChar: number = 100;
export const passwordValidationSchema = schema<string>((password) => {
    //Validation : Password
    required(password, { message: "The password is required !" }),
    minLength(password, paswordMinChar, { message: `Password min length : ${paswordMinChar} !` }),
    maxLength(password, passwordMaxChar,  { message: `Password max length : ${passwordMaxChar} !` })
    //pattern(password, /^(?=.*\p{Ll})(?=.*\p{Lu})(?=.*\d)(?=.*[^\p{L}\d\s])\S{12,}$/, { message: "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character !" })
});