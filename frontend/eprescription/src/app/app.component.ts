import { Component } from '@angular/core';
import { RouterLink, RouterOutlet } from '@angular/router';
import { TranslateModule, TranslateService } from '@ngx-translate/core';
import { MaterialModule } from './shared/material.module';
import { FormsModule } from '@angular/forms';
import { NgxPrintModule } from 'ngx-print';

@Component({
  selector: 'app-root',
    standalone: true,
  imports: [RouterOutlet,TranslateModule,MaterialModule,FormsModule,NgxPrintModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'eprescription';
  languages = ['en', 'bn'];
  currentLang = 'en';
  isEnglish = true;

  constructor(private translate: TranslateService) {}

  ngOnInit(): void {
    this.translate.addLangs(this.languages);
    this.translate.setDefaultLang('en');

    const savedLang = localStorage.getItem('language') ||
      this.translate.getBrowserLang() || 'en';

    this.toggleLanguage(true);
  }

   toggleLanguage(isEnglish: boolean) {
    this.currentLang = isEnglish ? 'en' : 'bn';
    this.isEnglish = isEnglish;
    this.translate.use(isEnglish ? 'en' : 'bn');
    localStorage.setItem('language', this.currentLang);
  }

   logout() {
    localStorage.clear();
    location.href = '/login';
  }
}
