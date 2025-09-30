import { bootstrapApplication } from '@angular/platform-browser';
import { provideRouter } from '@angular/router';
import { HttpClient, provideHttpClient, withInterceptors } from '@angular/common/http';
import { AppComponent } from './app/app.component';
import { APP_ROUTES } from './app/app.routes';
import { authInterceptor } from './app/core/auth/auth.interceptor';
import { provideTranslateService, TranslateLoader } from '@ngx-translate/core';
import { httpLoaderFactory } from './app/app.config';

bootstrapApplication(AppComponent, {
  providers: [
    provideRouter(APP_ROUTES),
    provideHttpClient(withInterceptors([authInterceptor])), // 👈 This provides HttpClient
    provideTranslateService({
          loader: {
            provide: TranslateLoader,
            useFactory: httpLoaderFactory,
            deps: [HttpClient],
          },
          defaultLanguage: 'en', // Set the default language
        }),
  ],
});