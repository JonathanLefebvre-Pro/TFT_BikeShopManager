import { schema, validate } from "@angular/forms/signals";

export const hiredAtValidationSchema = schema<Date | null>((hiredAt) => {
    validate(hiredAt, ({value}) => {
      const hiredAt = value();
      if(!hiredAt) return { kind:'required', message:"Hired date is required !" };
      return undefined;
    })
});