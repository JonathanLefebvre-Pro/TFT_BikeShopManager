import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";

export const AuthUser_AddEmployee_Guard: CanActivateFn = (route, state) => {

    const routerService = inject(Router);

    //TODO : Get role and permissions of the current auth user

    return false;
};