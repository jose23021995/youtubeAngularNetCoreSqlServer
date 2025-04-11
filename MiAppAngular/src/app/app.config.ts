import { ApplicationConfig, importProvidersFrom } from '@angular/core';
/*
  ApplicationConfig: es una interfaz que define la configuración de arranque de la aplicación.
  importProvidersFrom: permite importar módulos como proveedores (por ejemplo, HttpClientModule) cuando se usan componentes standalone.
*/
import { provideRouter, withComponentInputBinding } from '@angular/router';
/*
  provideRouter(routes): configura el enrutamiento principal usando las rutas definidas.
  withComponentInputBinding(): permite que los parámetros de la URL se enlacen directamente a las propiedades (@Input()) del componente.
*/
import { routes } from './app.routes';
//  Aquí se importan las rutas definidas de la aplicación (normalmente en un archivo app.routes.ts).
import { provideAnimationsAsync } from '@angular/platform-browser/animations/async';
//provideAnimationsAsync(): activa las animaciones de Angular pero de forma asíncrona, lo cual mejora el rendimiento inicial al hacer lazy load del módulo de animaciones (BrowserAnimationsModule).
import { HttpClientModule,provideHttpClient, withFetch } from '@angular/common/http';
/*
  HttpClientModule: tradicional módulo de Angular para hacer peticiones HTTP.
  provideHttpClient(): alternativa standalone moderna para usar HttpClient.
  withFetch(): le dice a Angular que use la API nativa de fetch() en lugar de XMLHttpRequest. Es una forma más moderna de hacer solicitudes HTTP.
*/
export const appConfig: ApplicationConfig = {
  providers: [
    provideRouter(routes,withComponentInputBinding()), 
    //withComponentInputBinding permitimos que las url reciban parametros
    provideAnimationsAsync(),
    //importProvidersFrom(HttpClientModule
    provideHttpClient(withFetch()),
  ]
};
/*
  Este objeto define qué proveedores se van a usar en la app:
  Ruteo con binding automático de inputs desde parámetros de URL.
  Animaciones asincrónicas para mejorar rendimiento.
  Cliente HTTP con API fetch() nativa.
*/