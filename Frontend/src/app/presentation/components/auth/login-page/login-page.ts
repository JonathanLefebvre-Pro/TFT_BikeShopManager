import { Component, inject, OnInit, signal } from '@angular/core';
import { LoginPanel } from '../login-panel/login-panel';
import { Router } from '@angular/router';
import { AuthService } from '@/app/infrastructure/services/auth-service';
import { LoginRequestDto } from '@/app/appCore/dtos/auth/loginRequestDto';

@Component({
  imports: [LoginPanel],
  selector: 'app-login-page',
  styleUrl: './login-page.scss',
  templateUrl: './login-page.html',
})
export class LoginPage implements OnInit {

  protected routerService = inject(Router);
  private readonly authService = inject(AuthService);
  protected readonly displayErrorMessage = signal<boolean>(false);

  private timeoutId: number = 0;

  public ngOnInit(): void {
    clearTimeout(this.timeoutId);
  }

  protected OnLogin(value: {login: string, password: string}) {

    //Call the API to check if the login and password are valid !
    const dto: LoginRequestDto = {
      login: value.login, 
      password: value.password
    }

    this.authService.login(dto).subscribe({
      next: (res) => {
        //console.log(res);
        
        this.displayErrorMessage.set(false);
        this.routerService.navigate(['/main']);
      },
      error: (error) => {
        console.error(error);

        //Display error message : Login or password are not valid !
        this.displayErrorMessage.set(true);
        this.timeoutId = setTimeout(() => {this.displayErrorMessage.set(false)}, 5000);
      },
    });
  }
}
