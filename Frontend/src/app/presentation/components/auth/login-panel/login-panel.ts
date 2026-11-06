import { LoginFormData } from '@/app/presentation/forms/models/auth/loginFormData';
import { loginValidationSchema, passwordValidationSchema } from '@/app/presentation/forms/validations/loginValidationSchema';
import { Component, output, signal } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { FormRoot, FormField, form, apply } from '@angular/forms/signals';
import { NgIcon, provideIcons } from '@ng-icons/core';
import { matEmail, matLock } from '@ng-icons/material-icons/baseline';

@Component({
  imports: [FormsModule, FormRoot, FormField, NgIcon],
  selector: 'app-login-panel',
  styleUrl: './login-panel.scss',
  templateUrl: './login-panel.html',
  providers: [provideIcons({matEmail, matLock})]
})
export class LoginPanel {

  protected onConnectButtonClicked = output<{login: string, password: string}>();

  private readonly INITIAL_MODEL = {
    login: '',
    password: ''
  };
  protected loginModel = signal<LoginFormData>({...this.INITIAL_MODEL});

  protected loginForm = form(this.loginModel, (schemaPath) => {
    //Validation : Login
    apply(schemaPath.login, loginValidationSchema),
    //Validation : Password
    //TODO : Add validation with password best practices (NIST 800-63B)
    apply(schemaPath.password, passwordValidationSchema)
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
