import { Employee } from '@/app/domain/employee';
import { Component, inject, input, output, signal } from '@angular/core';
import { EmptyStringPlaceholderPipe } from '@/app/appCore/pipes/empty-string-placeholder-pipe';
import { DatePipe } from '@angular/common';
import { FormTextInput } from '@presentation/components/shared/form-text-input/form-text-input';
import { DateUtilityService } from '@/app/infrastructure/utilities/date-utility-service';
import { apply, form, FormRoot } from '@angular/forms/signals';
import { passwordValidationSchema } from '@/app/presentation/forms/validations/loginValidationSchema';

@Component({
  imports: [EmptyStringPlaceholderPipe, DatePipe, FormTextInput, FormRoot],
  selector: 'app-employee-profile-edit-panel',
  styleUrl: './employee-profile-edit-panel.scss',
  templateUrl: './employee-profile-edit-panel.html',
})
export class EmployeeProfileEditPanel {

  protected readonly dateUtilityService = inject(DateUtilityService);

  public readonly employee = input.required<Employee | null>()

  public onSubmitFormEvent = output<{currentPassword: string, newPassword: string}>();

  protected readonly errorMessage: string = 'Something went wrong !';
  protected readonly displayErrorMessage = signal<boolean>(false);

  private readonly INITIAL_MODEL = {
    currentPassword: '',
    newPassword: ''
  };
  protected employeeModel = signal({...this.INITIAL_MODEL});

  protected employeeProfileForm = form(this.employeeModel, (schemaPath) => {
    apply(schemaPath.currentPassword, passwordValidationSchema),
    apply(schemaPath.newPassword, passwordValidationSchema)
  },
  {
    submission: {
      action: async (model) => {
        this.onSubmitFormEvent.emit({...model().value()});
      }
    }
  });

  protected handleClear(event: Event) {
    event.preventDefault();
    this.ResetForm();
  }

  public DisplayErrorMessage() {
    this.displayErrorMessage.set(true);
  }

  public ResetForm() {
    this.employeeProfileForm().reset({...this.INITIAL_MODEL}); 
  }
}
