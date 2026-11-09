import { AuthUserService } from "@/app/infrastructure/services/auth-user-service";
import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";
import { map } from "rxjs";

export const RoleGuard: CanActivateFn = (route, state) => {
    const authUserService = inject(AuthUserService);
    const router = inject(Router);

    //Get role and permissions of the current auth user
    const expectedRoles = route.data['roles'] as string[];

    //Check if the auth user has any of these roles
    return authUserService.getRole().pipe(
        map(role => expectedRoles.includes(role.name) ? true : router.createUrlTree(['error']))
        //TODO : Catch error (Is this necessary ??)
    );
};