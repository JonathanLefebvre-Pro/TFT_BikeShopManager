import { Component, inject } from '@angular/core';
import { LoginPanel } from '../login-panel/login-panel';
import { Router } from '@angular/router';

@Component({
  imports: [LoginPanel],
  selector: 'app-login-page',
  styleUrl: './login-page.scss',
  templateUrl: './login-page.html',
})
export class LoginPage {

  protected routerService = inject(Router);

  protected OnLogin(value: {login: string | undefined, password: string | undefined}) {
      //TODO : Call the API to check if the login and password are ok !

      this.routerService.navigate(['/main']);
  }
}
