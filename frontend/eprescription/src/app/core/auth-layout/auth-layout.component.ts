import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { MaterialModule } from '../../shared/material.module';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-auth-layout',
  imports: [RouterOutlet, MaterialModule, CommonModule, RouterLink],
  templateUrl: './auth-layout.component.html',
  styleUrl: './auth-layout.component.css'
})
export class AuthLayoutComponent {
logout() {
    localStorage.clear();
    location.href = '/login';
  }
}
