import { AuthService } from "@/app/infrastructure/services/auth-service";
import { inject } from "@angular/core";
import { CanActivateFn, Router } from "@angular/router";

export const LoginGuard: CanActivateFn = (route, state) => {

    const routerService = inject(Router);
    const authService = inject(AuthService);

    if(authService.IsAuthenticated())
        return routerService.parseUrl('/main');
    else
        return true;
};