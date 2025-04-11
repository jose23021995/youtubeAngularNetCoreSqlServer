
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

using MiWebAPI.Data;
//llama la carpeta donde esta nuestra conecion a la bd
using MiWebAPI.Models;
//llama la carpeta modelos
namespace MiWebAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmpleadoController : ControllerBase
    {
        private readonly EmpleadoData _empleadoData;
        //obtenemos la variable de conexion a los procedimientos almacenados
        public EmpleadoController(EmpleadoData empleadoData)
        {
            _empleadoData = empleadoData;
        }
        /*Inyección de Dependencias 
            Se declara una variable privada readonly de tipo EmpleadoData, la cual contiene la lógica para acceder a la base de datos a través de procedimientos almacenados.
            El constructor del controlador recibe una instancia de EmpleadoData (inyectada por el contenedor de dependencias) y la asigna a la variable _empleadoData. 
            Esto facilita llamar métodos para obtener, crear, editar y eliminar empleados.
         */

        [HttpGet] //a) Obtener la lista completa de empleados
        public async Task<IActionResult> Lista()
        {
            List<Empleado> Lista = await _empleadoData.Lista();
            return StatusCode(StatusCodes.Status200OK, Lista);
        }
        /*
            [HttpGet]: Define que este método responde a solicitudes GET.
            Lista(): Método asincrónico que llama a _empleadoData.Lista() para obtener una lista de empleados.
            Retorna el resultado con un HTTP 200 (OK) y la lista de empleados.
         */

        [HttpGet("{id}")] //devuelve un empleado
        public async Task<IActionResult> Obtener(int id)
        {
            Empleado objeto = await _empleadoData.Obtener(id);
            return StatusCode(StatusCodes.Status200OK, objeto);
        }

        [HttpPost] //crea un empleado
        public async Task<IActionResult> Crear([FromBody] Empleado objeto)
        {
            bool respuesta = await _empleadoData.Crear(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            /*
                [HttpPost]: Este método responde a solicitudes POST.
                [FromBody] Empleado objeto: Indica que el objeto Empleado se debe leer del cuerpo de la solicitud.
                Se llama a _empleadoData.Crear(objeto), que devuelve un valor booleano indicando el éxito de la operación.
                Retorna un objeto JSON (por ejemplo, { isSuccess: true }) junto con un código 200.
             */
        }

        [HttpPut] //edita un empleado
        public async Task<IActionResult> Editar([FromBody] Empleado objeto)
        {
            bool respuesta = await _empleadoData.Editar(objeto);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            /*
                [HttpPut]: Maneja solicitudes PUT, generalmente utilizadas para actualizar recursos.
                Similar al método Crear, recibe un objeto Empleado desde el cuerpo y llama a _empleadoData.Editar(objeto).
                Retorna el resultado en formato JSON y código 200.
             */
        }

        [HttpDelete("{id}")] //elimina un empleado
        public async Task<IActionResult> Eliminar(int id)
        {
            bool respuesta = await _empleadoData.Eliminar(id);
            return StatusCode(StatusCodes.Status200OK, new { isSuccess = respuesta });
            /*
                [HttpDelete("{id}")]: Define que el método responde a solicitudes DELETE, y recibe el id como parámetro en la URL.
                El método llama a _empleadoData.Eliminar(id) para borrar el empleado.
                Devuelve un resultado indicando si la eliminación fue exitosa.
             */
        }
    }
}
