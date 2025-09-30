import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideTranslateService, TranslateLoader } from '@ngx-translate/core';
import { TranslateHttpLoader } from '@ngx-translate/http-loader';
import { HttpClient, provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';

// Factory function for the TranslateHttpLoader
export function httpLoaderFactory(http: HttpClient) {
  return new TranslateHttpLoader(http, 'i18n/', '.json');
}

import { APP_ROUTES } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
     provideHttpClient(withInterceptorsFromDi()),
     provideTranslateService({
      loader: {
        provide: TranslateLoader,
        useFactory: httpLoaderFactory,
        deps: [HttpClient],
      },
      defaultLanguage: 'en', // Set the default language
    }),
    provideZoneChangeDetection({ eventCoalescing: true }),
     provideRouter(APP_ROUTES),
  ]
};
