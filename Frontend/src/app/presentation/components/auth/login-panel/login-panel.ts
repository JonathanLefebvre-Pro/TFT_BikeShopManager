import { LoginFormData } from '@/app/presentation/forms/auth/loginFormData';
import { Component, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FormRoot, FormField, form, required, email, minLength, maxLength, validate, pattern } from '@angular/forms/signals';

@Component({
  imports: [FormsModule, FormRoot, FormField],
  selector: 'app-login-panel',
  styleUrl: './login-panel.scss',
  templateUrl: './login-panel.html',
})
export class LoginPanel {

  protected onConnectButtonClicked = output<{login: string, password: string}>();

  private readonly INITIAL_MODEL = {
    login: '',
    password: ''
  };
  protected loginModel = signal<LoginFormData>({...this.INITIAL_MODEL});

  protected loginForm = form(this.loginModel, (schemaPath) => {
    required(schemaPath.login, {message: "The login is required !"}),
    pattern(schemaPath.login, /^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9-]+(\.[a-zA-Z0-9-]+)*\.[a-zA-Z]{2,}$/, {message: "Enter a valid email !"}),
    minLength(schemaPath.login, 6, {message: "Login min length : 6 !"}),
    maxLength(schemaPath.login, 254,  {message: "Login max length : 254 !"}),
    required(schemaPath.password, {message: "The password is required !"}),
    minLength(schemaPath.password, 5, {message: "Password min length : 5 !"}),
    maxLength(schemaPath.password, 100,  {message: "Password max length : 100 !"})
  },
  {
    submission: {
      action: async (model) => {
        this.onConnectButtonClicked.emit({login: model().value().login, password: model().value().password});
        model().reset({...this.INITIAL_MODEL});
      }
    }
  });
}
