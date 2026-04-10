import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule, FormBuilder, FormGroup, Validators } from '@angular/forms';
import { ActivatedRoute, Router } from '@angular/router';
import { ApiService } from '../../../../services/api.service';

@Component({
  selector: 'app-client-form',
  standalone: true,
  imports: [CommonModule, FormsModule, ReactiveFormsModule],
  templateUrl: './client-form.component.html',
  styleUrl: './client-form.component.scss'
})
export class ClientFormComponent implements OnInit {
  clientForm: FormGroup;
  isEditMode = false;
  clientId: number | null = null;
  isLoading = false;
  error: string | null = null;
  success: string | null = null;

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router,
    private route: ActivatedRoute
  ) {
    this.clientForm = this.createClientForm();
  }

  ngOnInit(): void {
    const id = this.route.snapshot.paramMap.get('id');
    if (id) {
      this.isEditMode = true;
      this.clientId = Number(id);
      this.loadClient(this.clientId);
    }
  }

  private createClientForm(): FormGroup {
    return this.fb.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern('^[0-9+\-\s()]*$')]],
      address: ['', Validators.required],
      status: ['Active', Validators.required],
      dateOfBirth: ['', Validators.required],
      nationality: ['', Validators.required],
      businessActivity: ['', Validators.required],
      sourceOfIncome: ['', Validators.required],
      notes: ['']
    });
  }

  async loadClient(clientId: number): Promise<void> {
    try {
      this.isLoading = true;
      this.error = null;
      
      const response = await this.apiService.getClientById(clientId).toPromise();
      const client = response?.data;
      
      if (client) {
        this.clientForm.patchValue({
          name: client.name,
          email: client.email,
          phone: client.phone,
          address: client.address,
          status: client.status,
          dateOfBirth: client.dateOfBirth,
          nationality: client.nationality,
          businessActivity: client.businessActivity,
          sourceOfIncome: client.sourceOfIncome,
          notes: client.notes
        });
      }
    } catch (error) {
      console.error('Error loading client:', error);
      this.error = 'Failed to load client. Please try again.';
    } finally {
      this.isLoading = false;
    }
  }

  async onSubmit(): Promise<void> {
    if (this.clientForm.invalid) {
      this.markFormGroupTouched(this.clientForm);
      return;
    }

    try {
      this.isLoading = true;
      this.error = null;
      this.success = null;

      const clientData = this.clientForm.value;

      if (this.isEditMode && this.clientId) {
        const updateData = { ...clientData, id: this.clientId };
        await this.apiService.updateClient(updateData);
        this.success = 'Client updated successfully!';
      } else {
        await this.apiService.addClient(clientData);
        this.success = 'Client added successfully!';
      }

      // Redirect to client list after 2 seconds
      setTimeout(() => {
        this.router.navigate(['/clients']);
      }, 2000);

    } catch (error) {
      console.error('Error saving client:', error);
      this.error = 'Failed to save client. Please try again.';
    } finally {
      this.isLoading = false;
    }
  }

  onCancel(): void {
    this.router.navigate(['/clients']);
  }

  private markFormGroupTouched(formGroup: FormGroup): void {
    Object.values(formGroup.controls).forEach(control => {
      control.markAsTouched();
    });
  }

  getFormControl(name: string) {
    return this.clientForm.get(name);
  }

  isFieldInvalid(name: string): boolean {
    const control = this.getFormControl(name);
    return !!(control && control.invalid && (control.touched || control.dirty));
  }

  getErrorMessage(name: string): string {
    const control = this.getFormControl(name);
    if (control && control.errors) {
      if (control.errors['required']) {
        return 'This field is required';
      }
      if (control.errors['email']) {
        return 'Please enter a valid email address';
      }
      if (control.errors['minlength']) {
        return `Minimum length is ${control.errors['minlength'].requiredLength} characters`;
      }
      if (control.errors['pattern']) {
        return 'Please enter a valid phone number';
      }
    }
    return '';
  }
}
