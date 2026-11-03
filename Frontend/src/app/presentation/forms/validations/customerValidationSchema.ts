import { maxLength, pattern, schema } from "@angular/forms/signals";

export const vatNumberValidationSchema = schema<string>((vatNumber) => {
    //Validation : VAT Number
    maxLength(vatNumber, 20, { message: "Vat number max length : 20 !" }),
    pattern(vatNumber, /^[A-Z]{2}[A-Z0-9+*]{2,}$/, { message: "Invalid VAT Number !" })
});