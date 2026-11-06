import { maxLength, minLength, pattern, required, schema } from "@angular/forms/signals";

const firstNameMinChar: number = 2;
const firstNameMaxChar: number = 100;
export const firstnameValidationSchema = schema<string>((firstname) => {
    //Validation : Firstname
    required(firstname, { message: "Firstname is required !" }),
    minLength(firstname, firstNameMinChar, { message: `Firstname min length : ${firstNameMinChar} !` }),
    maxLength(firstname, firstNameMaxChar,  { message: `Firstname max length : ${firstNameMaxChar} !` }),
    pattern(firstname, /^(?=.{2,100}$)\p{L}+(?:[ '’-]\p{L}+)*$/u, { message: "Invalid Firstname !" })
});
const lastNameMinChar: number = 2;
const lastNameMaxChar: number = 100;
export const lastnameValidationSchema = schema<string>((lastname) => {
    //Validation : Lastname
    required(lastname, { message: "LastName is required !" }),
    minLength(lastname, lastNameMinChar, { message: `LastName min length : ${lastNameMinChar} !` }),
    maxLength(lastname, lastNameMaxChar,  { message: `LastName max length : ${lastNameMaxChar} !` }),
    pattern(lastname, /^(?=.{2,100}$)\p{L}+(?:[ '’-]\p{L}+)*$/u, { message: "Invalid LastName !" })
});
const emailMaxChar: number = 254;
export const emailValidationSchema = schema<string>((email) => {
    //Validation : Email
    pattern(email, /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$/, { message: "Enter a valid email !" }),
    maxLength(email, emailMaxChar, { message: `Email max length : ${emailMaxChar} !`})
});
const phoneNumberMaxChar: number = 20;
export const phoneNumberValidationSchema = schema<string>((phoneNumber) => {
    //Validation : Phone Number
    maxLength(phoneNumber, phoneNumberMaxChar, { message: `Phone number max length : ${phoneNumberMaxChar} !` }),
    pattern(phoneNumber, /^(?:(?:\+|00)32|0)\s?(?:[1-9]\d?)(?:[\s./-]?\d{2,3}){3,4}$/, { message: "Invalid phone number !" })
});
const addressMaxChar: number = 200;
export const addressValidationSchema = schema<string>((address) => {
    //Validation : Address
    maxLength(address, addressMaxChar, { message: `Address max length : ${addressMaxChar} !` })
});
const cityMaxChar: number = 50;
export const cityValidationSchema = schema<string>((city) => {
    //Validation : City
    maxLength(city, cityMaxChar, { message: `City max length : ${cityMaxChar} !` }),
    pattern(city, /^\p{L}+\.?(?:[ '’\-]\p{L}+\.?)*$/u, { message: "Invalid city name !" })
});
const zipCodeMaxChar: number = 10;
export const zipCodeValidationSchema = schema<string>((zipCode) => {
    //Validation : ZipCode
    maxLength(zipCode, zipCodeMaxChar, { message: `ZipCode max length : ${zipCodeMaxChar} !` }),
    pattern(zipCode, /^[A-Z0-9]+(?:[ -][A-Z0-9]+)*$/u, { message: "Invalid ZipCode number !" })
});
const countryMaxChar: number = 50;
export const countryValidationSchema = schema<string>((country) => {
    //Validation : Country
    maxLength(country, countryMaxChar, { message: `Country max length : ${countryMaxChar} !` }),
    pattern(country, /^\p{L}+(?:[ '’\-.]\p{L}+)*$/u, { message: "Invalid country name !" })
});