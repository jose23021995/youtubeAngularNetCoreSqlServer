import { HttpClient } from '@angular/common/http';
import { Injectable, inject } from '@angular/core';
import { appsettings } from '../Settings/appsetting';
import { Empleado } from '../Models/Empleado';
import { ResponseAPI } from '../Models/ResponseAPI';
/*
  HttpClient: permite hacer peticiones HTTP (GET, POST, PUT, etc.).
  inject: una forma moderna (Angular >=14) de inyectar dependencias sin usar constructor.
  @Injectable: indica que esta clase se puede inyectar como dependencia en otras partes de la app.
  appsettings: archivo de configuración donde está la URL base de la API.
  Empleado: modelo de datos de un empleado.
  ResponseAPI: estructura que devuelve la API al hacer crear, editar o eliminar.
*/
@Injectable({
  providedIn: 'root'
})
/*
  Esto significa que el servicio se va a cargar automáticamente en toda la aplicación (singleton), no necesitas agregarlo a providers.
*/
export class EmpleadoService {
//Se define la clase del servicio, donde estarán todas las funciones que interactúan con la API.

  private http = inject(HttpClient);
  //Aquí estás inyectando el HttpClient sin usar constructor, usando el nuevo API de inject().
  private apiUrl:string = appsettings.apiUrl + "Empleado";
  //Si appsettings.apiUrl = "http://localhost:5277/api/", entonces:
  //apiUrl = "http://localhost:5277/api/Empleado"
  constructor() { }

  lista(){
    return this.http.get<Empleado[]>(this.apiUrl);
  }
  //Hace un GET para traer todos los empleados (sp_listaEmpleados). Devuelve un arreglo de empleados.

  obtener(id:number){
    return this.http.get<Empleado>(`${this.apiUrl}/${id}`);
  }
  /*
    Hace un GET a una URL como:
    http://localhost:5277/api/Empleado/3
    Devuelve un empleado individual por ID (sp_obtenerEmpleado).
  */
  crear(objeto:Empleado){
    return this.http.post<ResponseAPI>(this.apiUrl,objeto);
  }
  //Hace un POST a la API con el objeto empleado. Devuelve un ResponseAPI con el resultado.

  editar(objeto:Empleado){
    return this.http.put<ResponseAPI>(this.apiUrl,objeto);
  }
  //Hace un PUT para actualizar datos de un empleado. También espera que la API devuelva un ResponseAPI.

  eliminar(id:number){
    return this.http.delete<ResponseAPI>(`${this.apiUrl}/${id}`);
  }
  //Hace un DELETE a http://localhost:5277/api/Empleado/3 y borra el empleado.
}