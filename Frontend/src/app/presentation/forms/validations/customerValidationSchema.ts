import { maxLength, pattern, schema } from "@angular/forms/signals";

const vatMaxChar: number = 20;
export const vatNumberValidationSchema = schema<string>((vatNumber) => {
    //Validation : VAT Number
    maxLength(vatNumber, vatMaxChar, { message: `Vat number max length : ${vatMaxChar} !` }),
    pattern(vatNumber, /^[A-Z]{2}[A-Z0-9+*]{2,}$/, { message: "Invalid VAT Number !" })
});