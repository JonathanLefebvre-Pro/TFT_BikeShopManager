import { maxLength, minLength, pattern, required, schema } from "@angular/forms/signals";

export const loginValidationSchema = schema<string>((login) => {
    //Validation : Login
    required(login, { message: "The login is required !" }),
    pattern(login, /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$/, { message: "Enter a valid email !" }),
    minLength(login, 6, { message: "Login min length : 6 !" }),
    maxLength(login, 254,  { message: "Login max length : 254 !" })
});

export const passwordValidationSchema = schema<string>((password) => {
    //Validation : Password
    required(password, { message: "The password is required !" }),
    minLength(password, 5, { message: "Password min length : 5 !" }),
    maxLength(password, 100,  { message: "Password max length : 100 !" })
    //pattern(password, /^(?=.*\p{Ll})(?=.*\p{Lu})(?=.*\d)(?=.*[^\p{L}\d\s])\S{12,}$/, { message: "Password must contain at least one lowercase letter, one uppercase letter, one digit, and one special character !" })
});