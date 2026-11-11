import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { AuthService } from "../services/auth-service";

export const RoleGuard: CanActivateFn = (route, state) => {
    //const authUserService = inject(AuthUserService);
    const authService = inject(AuthService);
    const router = inject(Router);

    //Get role and permissions of the current auth user
    const expectedRoles = route.data['roles'] as string[];

    //Check if the auth user has any of these roles
    if(authService.IsAuthenticated() && authService.hasAnyRoles<string>(expectedRoles))
        return true;

    return router.createUrlTree(['error'])
};