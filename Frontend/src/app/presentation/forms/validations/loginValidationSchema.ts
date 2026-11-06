import { maxLength, minLength, pattern, required, schema } from "@angular/forms/signals";

const loginMinChar: number = 6;
const loginMaxChar: number = 254;
export const loginValidationSchema = schema<string>((login) => {
    //Validation : Login
    required(login, { message: "Email is required." }),
    pattern(login, /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$/, { message: "Enter a valid email address." }),
    minLength(login, loginMinChar, { message: `Email must be at least ${loginMinChar} characters long.` }),
    maxLength(login, loginMaxChar,  { message: `Email can have a maximum of ${loginMaxChar} characters long.` })
});

const paswordMinChar: number = 5;
const passwordMaxChar: number = 100;
export const passwordValidationSchema = schema<string>((password) => {
    //Validation : Password
    required(password, { message: "Password is required." }),
    minLength(password, paswordMinChar, { message: `Password must be at least ${paswordMinChar} characters long.` }),
    maxLength(password, passwordMaxChar,  { message: `Password can have a maximum of ${passwordMaxChar} characters long.` })
    //pattern(password, /^(?=.*\p{Ll})(?=.*\p{Lu})(?=.*\d)(?=.*[^\p{L}\d\s])\S{12,}$/, { message: "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character !" })
});