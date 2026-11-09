import { Component, DestroyRef, inject, viewChild } from '@angular/core';
import { LoginPanel } from '../login-panel/login-panel';
import { Router } from '@angular/router';
import { AuthService } from '@/app/infrastructure/services/auth-service';
import { LoginRequestDto } from '@/app/appCore/dtos/auth/loginRequestDto';
import { takeUntilDestroyed } from '@angular/core/rxjs-interop';

@Component({
  imports: [LoginPanel],
  selector: 'app-login-page',
  styleUrl: './login-page.scss',
  templateUrl: './login-page.html',
})
export class LoginPage {

  protected routerService = inject(Router);
  private readonly authService = inject(AuthService);
  protected readonly destroyRef = inject(DestroyRef);
  protected readonly loginPanelChild = viewChild(LoginPanel);

  protected OnLogin(value: {login: string, password: string}) {

    //Call the API to check if the login and password are valid !
    const dto: LoginRequestDto = {
      login: value.login, 
      password: value.password
    }

    this.authService.login(dto).pipe(takeUntilDestroyed(this.destroyRef)).subscribe({
      next: (res) => {
        this.loginPanelChild()?.HideErrorMessage();
        this.routerService.navigate(['/main']);
      },
      error: (error) => {
        //console.error('Error from login page : ',error);
        this.loginPanelChild()?.DisplayErrorMessage();
      },
    });
  }
}
