import { Routes } from '@angular/router';
import { CreatePrescriptionComponent } from './create-prescription.component';
import { authGuard } from '../../core/auth/auth.guard';

export const PRESCRIPTION_ROUTES: Routes = [
  { path: '', component: CreatePrescriptionComponent , canActivate: [authGuard]},
];
