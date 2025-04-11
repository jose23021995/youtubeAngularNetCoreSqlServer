import { Routes } from '@angular/router';
// Routes: es un tipo que define un arreglo de objetos de ruta, cada uno especificando un path y qué componente se debe renderizar.
import { InicioComponent } from './pages/inicio/inicio.component';
import { EmpleadoComponent } from './pages/empleado/empleado.component';
/*
*componentes importados
Se importan los componentes que se van a usar en las rutas.
Se asume que están en la carpeta pages.
*/
export const routes: Routes = [
    {path:'',component:InicioComponent},
    {path:'inicio',component:InicioComponent},
    {path:'empleado/:id',component:EmpleadoComponent},
];
/**
Este array de rutas define cómo Angular debe manejar la navegación:
🔹 path: ''
Ruta vacía (root o /).

Renderiza el InicioComponent.

Es útil para cuando el usuario entra directamente a la raíz de la app.

🔹 path: 'inicio'
Ruta /inicio.

También renderiza InicioComponent.

Es redundante con la ruta anterior, pero permite navegar a /inicio directamente.

🔹 path: 'empleado/:id'
Ruta con parámetro dinámico :id. Ejemplo: /empleado/1, /empleado/25, etc.

Renderiza EmpleadoComponent.

El valor de :id se puede capturar dentro del componente con ActivatedRoute.
 */