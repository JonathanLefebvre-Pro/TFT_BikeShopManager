import { Service } from "@angular/core";

@Service()
export class DateUtilityService {
    public getDate() : Date {
        var date = new Date();
        date.setUTCHours(0, 0, 0, 0);
        return date;
    }
}