using MiWebAPI.Models;
//👉 Sirve para importar el espacio de nombres MiWebAPI.Models, que típicamente contiene las clases modelo utilizadas en la aplicación.
//Estas clases representan las entidades o estructuras de datos con las que trabajarás.
using System.Data;
/*
 👉 Proporciona clases y interfaces generales para trabajar con datos, como:

DataTable: Una tabla en memoria que puede contener datos de una base de datos.

DataSet: Un conjunto de tablas relacionadas.

IDataReader: Una interfaz para leer datos de forma más eficiente desde una base de datos.
 */
using System.Data.SqlClient;
/*
 👉 Específicamente para trabajar con bases de datos SQL Server.
Esta librería contiene clases necesarias para conectarse a una base de datos SQL Server y ejecutar consultas, comandos y procedimientos almacenados.
 */

namespace MiWebAPI.Data
{
    public class EmpleadoData
    {

        private readonly string conexion;
        /*
         private:

            Es un modificador de acceso que indica que la variable solo puede ser utilizada dentro de la misma clase donde está definida.

            readonly:

            Significa que el valor de la variable solo puede ser asignado durante su declaración o en el constructor de la clase. 
            Una vez asignado un valor, no puede ser modificado fuera de esos contextos.

            string:

            Es el tipo de la variable, en este caso, una cadena de texto. Aquí se almacena, típicamente, una cadena de caracteres como una URL, 
            una conexión a base de datos, o cualquier otro tipo de texto.

            conexion:

            Es el nombre de la variable. Por convención, su nombre sugiere que esta variable almacenará una cadena de conexión, 
            como la URL o la cadena de configuración para conectarse a una base de datos.
           */
        public EmpleadoData(IConfiguration configuration)
        {
            //IConfiguration configuration nos permite entrar a app settings
            conexion = configuration.GetConnectionString("CadenaSQL")!;
            /*
             * GetConnectionString("CadenaSQL"):
             Este método se usa para obtener una cadena de conexión específica desde la configuración.
             El "CadenaSQL" es el nombre de la cadena de conexión que se busca en el archivo de configuración (por ejemplo, appsettings.json).
            */
            // "CadenaSQL": "Data Source=JOSE_ARMANDO_MT\\MSSQLSERVER01;Initial Catalog=DDCrudAngular;Integrated Security=True;Trusted_Connection=True;TrustServerCertificate=True;"
            //ignora la posibilidad de venir nulo (!)
        }

        public async Task<List<Empleado>> Lista()
        {
            /*
             * public async Task<List<Empleado>> Lista() ->
                 public: El método es accesible desde cualquier parte.
                 async: El método es asincrónico y no bloquea la ejecución mientras espera el resultado (por ejemplo, una consulta a la base de datos).
                 Task<List<Empleado>>: Devuelve una tarea que, cuando se complete, devolverá una lista de objetos Empleado.
                 Lista(): Es el nombre del método, que probablemente obtiene una lista de empleados.
                 En resumen: Es un método asincrónico que obtiene y devuelve una lista de empleados sin bloquear la aplicación mientras espera la respuesta.
             */
            List<Empleado> lista = new List<Empleado>();
            /*
                 List<Empleado>: Crea una lista de objetos Empleado. Empleado es el tipo de los elementos que la lista almacenará.
                 new List<Empleado>(): Inicializa la lista vacía, lista para agregar objetos de tipo Empleado.
                 lista: Es el nombre de la variable que almacena esta lista.
                 En resumen: Crea una lista vacía de empleados (List<Empleado>) que puede almacenar objetos de tipo Empleado.
             */
            using (var con = new SqlConnection(conexion))
            {
                /*  
                    using: Garantiza que el objeto con (en este caso, la conexión a la base de datos) sea eliminado automáticamente cuando ya no se necesite, cerrando la conexión al final del bloque.
                    var con = new SqlConnection(conexion): Crea una nueva instancia de SqlConnection, usando la cadena de conexión conexion, que establece una conexión a la base de datos SQL.
                */
                await con.OpenAsync();
                //la conexion se abre
                SqlCommand cmd = new SqlCommand("sp_listaEmpleados", con);
                // asignamos a una variable el procedimiento almacenado sp_listaEmpleados y le mandamos tambien la conexion
                cmd.CommandType = CommandType.StoredProcedure;
                //indicamos que cmd sera un procedimiento almacenado
                using (var reader = await cmd.ExecuteReaderAsync())
                //Ejecuta un comando SQL de manera asincrónica y obtiene un lector de datos que se cierra automáticamente al finalizar.
                {
                    while (await reader.ReadAsync())
                    {
                        /*
                            await reader.ReadAsync(): Lee de manera asincrónica la siguiente fila de resultados del lector de datos (reader). Devuelve true si hay más filas para leer, o false si se alcanzó el final de los resultados.
                            while: Ejecuta el bloque de código dentro de las llaves {} mientras haya filas disponibles para leer. 
                            En resumen: Lee las filas de los resultados de la base de datos de manera asincrónica y sigue ejecutando el código dentro del while hasta que no haya más filas.
                         */
                        lista.Add(new Empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                            FechaContrato = reader["FechaContrato"].ToString()
                        });
                    }
                }
            }
            return lista;
            /*Leer los resultados del procedimiento almacenado
             lista.Add(new Empleado { ... }): Crea un nuevo objeto Empleado para cada fila y lo agrega a la lista. 
            Se extraen los valores de las columnas IdEmpleado, NombreCompleto, Correo, Sueldo, y FechaContrato de la fila actual.
            al final retorna una lista
             */

        }

        public async Task<Empleado> Obtener(int Id)
            // obtiene el valor por el metodo reflejado en la linea 127
        {
            Empleado objeto = new Empleado();
            //obtiene las propiedades de empleado de los modelos
            using (var con = new SqlConnection(conexion))
            {
                await con.OpenAsync();
                SqlCommand cmd = new SqlCommand("sp_obtenerEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", Id);
                /*
                    1. cmd.Parameters
                    *cmd es un objeto de tipo SqlCommand que representa un comando SQL que se ejecutará en la base de datos.
                    *Parameters es una propiedad del objeto SqlCommand que representa la colección de parámetros del comando SQL. Los parámetros son valores que se pasan a una consulta o procedimiento almacenado para evitar la inyección de SQL y mejorar la seguridad y eficiencia.
                    2. AddWithValue("@IdEmpleado", Id)
                    *AddWithValue es un método de la colección de parámetros (cmd.Parameters) que agrega un nuevo parámetro al comando SQL.
                    *"@IdEmpleado" es el nombre del parámetro que se usará en la consulta SQL o en el procedimiento almacenado. En este caso, el parámetro se llama @IdEmpleado y es utilizado dentro de la consulta SQL (o procedimiento almacenado) para representar un valor específico.
                    Id es el valor que se asignará al parámetro @IdEmpleado. Este valor puede ser una variable o un valor que ya está definido en el código. El valor de Id se pasa al parámetro cuando el comando se ejecute.
                */
                cmd.CommandType = CommandType.StoredProcedure;

                using (var reader = await cmd.ExecuteReaderAsync())
                {
                    while (await reader.ReadAsync())
                    {
                        objeto = new Empleado
                        // objeto asimila el nuevo empleado
                        {
                            IdEmpleado = Convert.ToInt32(reader["IdEmpleado"]),
                            NombreCompleto = reader["NombreCompleto"].ToString(),
                            Correo = reader["Correo"].ToString(),
                            Sueldo = Convert.ToDecimal(reader["Sueldo"]),
                            FechaContrato = reader["FechaContrato"].ToString()
                        };
                    }
                }
            }
            return objeto;
            // no devuelve una lista si no un unico objeto
        }

        public async Task<bool> Crear(Empleado objeto)
        {
            //recivimos un objeto del tipo empleado y retorna un boleano
            bool respuesta = true;
            // la respuesta inicia verdadera
            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_crearEmpleado", con);
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@Sueldo", objeto.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", objeto.FechaContrato);
                // se alimentan las variables 
                //AddWithValue es un método que se utiliza para agregar un nuevo parámetro a la colección de parámetros del comando SQL.
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                    //retorna el numero de filas
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Editar(Empleado objeto)
        {
            //obtenemos el objeto

            bool respuesta = true;
            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_editarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", objeto.IdEmpleado);
                // agregamos el id del empleado 
                cmd.Parameters.AddWithValue("@NombreCompleto", objeto.NombreCompleto);
                cmd.Parameters.AddWithValue("@Correo", objeto.Correo);
                cmd.Parameters.AddWithValue("@Sueldo", objeto.Sueldo);
                cmd.Parameters.AddWithValue("@FechaContrato", objeto.FechaContrato);
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }

        public async Task<bool> Eliminar(int id)
        {
            bool respuesta = true;

            using (var con = new SqlConnection(conexion))
            {

                SqlCommand cmd = new SqlCommand("sp_eliminarEmpleado", con);
                cmd.Parameters.AddWithValue("@IdEmpleado", id);
                // agregamos solo el id del empleado
                cmd.CommandType = CommandType.StoredProcedure;
                try
                {
                    await con.OpenAsync();
                    respuesta = await cmd.ExecuteNonQueryAsync() > 0 ? true : false;
                }
                catch
                {
                    respuesta = false;
                }
            }
            return respuesta;
        }
    }
}