import { LoginFormData } from '@/app/presentation/forms/auth/loginFormData';
import { Component, model, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FormRoot, FormField, form, required, email, minLength, maxLength } from '@angular/forms/signals';

@Component({
  imports: [FormsModule, FormRoot, FormField],
  selector: 'app-login-panel',
  styleUrl: './login-panel.scss',
  templateUrl: './login-panel.html',
})
export class LoginPanel {

  protected loginInputModel = model<string>('Your email');
  protected passwordInputModel = model<string>('password');
  protected onConnectButtonClicked = output<{login: string | undefined, password: string | undefined}>();

  private readonly INITIAL_MODEL = {
    login: '',
    password: ''
  };
  protected loginModel = signal<LoginFormData>({...this.INITIAL_MODEL});

  protected handleOnConnectBtnClicked() {
    this.onConnectButtonClicked.emit({login: this.loginInputModel(), password: this.passwordInputModel()});
  }

  protected loginForm = form(this.loginModel, (schemaPath) => {
    required(schemaPath.login, {message: "The login is required !"}),
    email(schemaPath.login, {message: "Enter a valid email !"}),
    minLength(schemaPath.login, 5, {message: "Login min length : 5 !"}),
    maxLength(schemaPath.login, 100,  {message: "Login max length : 100 !"}),
    required(schemaPath.password, {message: "The password is required !"}),
    minLength(schemaPath.password, 5, {message: "Password min length : 5 !"}),
    maxLength(schemaPath.password, 100,  {message: "Password max length : 100 !"})
  },
  {
    submission: {
      action: async (model) => {
        this.onConnectButtonClicked.emit({login: this.loginInputModel(), password: this.passwordInputModel()});
        model().reset({...this.INITIAL_MODEL});
      }
    }
  });
}
