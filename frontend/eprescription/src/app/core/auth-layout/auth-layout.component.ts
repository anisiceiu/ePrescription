import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MaterialModule } from '../../shared/material.module';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';


@Component({
  selector: 'app-auth-layout',
  imports: [RouterOutlet, MaterialModule, CommonModule,FormsModule, RouterLink],
  templateUrl: './auth-layout.component.html',
  styleUrl: './auth-layout.component.css'
})
export class AuthLayoutComponent {
  sidelistType: string = 'patient';

  logout() {
    localStorage.clear();
    location.href = '/login';
  }
}
