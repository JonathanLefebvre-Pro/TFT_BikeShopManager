import { maxLength, minLength, pattern, required, schema } from "@angular/forms/signals";

export const firstnameValidationSchema = schema<string>((firstname) => {
    //Validation : Firstname
    required(firstname, { message: "Firstname is required !" }),
    minLength(firstname, 5, { message: "Firstname min length : 5 !" }),
    maxLength(firstname, 100,  { message: "Firstname max length : 100 !" }),
    pattern(firstname, /^(?=.{2,100}$)\p{L}+(?:[ '’-]\p{L}+)*$/u, { message: "Invalid Firstname !" })
});
export const lastnameValidationSchema = schema<string>((lastname) => {
    //Validation : Lastname
    required(lastname, { message: "LastName is required !" }),
    minLength(lastname, 5, { message: "LastName min length : 5 !" }),
    maxLength(lastname, 100,  { message: "LastName max length : 100 !" }),
    pattern(lastname, /^(?=.{2,100}$)\p{L}+(?:[ '’-]\p{L}+)*$/u, { message: "Invalid LastName !" })
});
export const emailValidationSchema = schema<string>((email) => {
    //Validation : Email
    pattern(email, /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$/, { message: "Enter a valid email !" }),
    maxLength(email, 254, { message: "Email max length : 256 !"})
});
export const phoneNumberValidationSchema = schema<string>((phoneNumber) => {
    //Validation : Phone Number
    maxLength(phoneNumber, 20, { message: "Phone number max length : 20 !"}),
    pattern(phoneNumber, /^(?:(?:\+|00)32|0)\s?(?:[1-9]\d?)(?:[\s./-]?\d{2,3}){3,4}$/, { message: "Invalid phone number !" })
});
export const addressValidationSchema = schema<string>((address) => {
    //Validation : Address
    maxLength(address, 200, { message: "Address max length : 200 !" })
});
export const cityValidationSchema = schema<string>((city) => {
    //Validation : City
    maxLength(city, 50, { message: "City max length : 50 !" }),
    pattern(city, /^\p{L}+\.?(?:[ '’\-]\p{L}+\.?)*$/u, { message: "Invalid city name !" })
});
export const zipCodeValidationSchema = schema<string>((zipCode) => {
    //Validation : ZipCode
    maxLength(zipCode, 10, { message: "ZipCode max length : 10 !" }),
    pattern(zipCode, /^[A-Z0-9]+(?:[ -][A-Z0-9]+)*$/u, { message: "Invalid ZipCode number !" })
});
export const countryValidationSchema = schema<string>((country) => {
    //Validation : Country
    maxLength(country, 50, { message: "Country max length : 50 !" }),
    pattern(country, /^\p{L}+(?:[ '’\-.]\p{L}+)*$/u, { message: "Invalid country name !" })
});