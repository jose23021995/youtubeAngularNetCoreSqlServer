create database DDCrudAngular

go

use DDCrudAngular

go

create table Empleado(
IdEmpleado int primary key identity,
NombreCompleto varchar(50),
Correo varchar(50),
Sueldo decimal(10,2),
FechaContrato date
)

go

insert into Empleado(NombreCompleto, Correo,Sueldo,FechaContrato)
Values
('Maria Mendez','maria@gmail.com',4500,'2024-01-12')

go

select * from Empleado

go

create proc sp_listaEmpleados
as 
begin
	select 
	IdEmpleado,NombreCompleto,Correo,Sueldo,
	CONVERT(char(10),FechaContrato,103)[FechaContrato]
	from Empleado
end

go

create proc sp_obtenerEmpleado
(@IdEmpleado int)
as 
begin
	select 
	IdEmpleado,NombreCompleto,Correo,Sueldo,
	CONVERT(char(10),FechaContrato,103)[FechaContrato]
	from Empleo
	where IdEmpleado= @IdEmpleado
end

go

create proc sp_crearEmpleado
(
@NombreCompleto varchar(50),
@Correo varchar(50),
@Sueldo decimal(10,2),
@FechaContrato varchar(10)
)
as 
begin
	set dateformat dmy
	insert into Empleado(NombreCompleto, Correo,Sueldo,FechaContrato)
	Values
	(@NombreCompleto,@Correo,@Sueldo,convert(date,@FechaContrato))
end

go

create proc sp_editarEmpleado
(
@IdEmpleado int,
@NombreCompleto varchar(50),
@Correo varchar(50),
@Sueldo decimal(10,2),
@FechaContrato varchar(10)
)
as 
begin
set dateformat dmy
	update Empleado
	set 
	 NombreCompleto=@NombreCompleto,
	 Correo=@Correo,
	 Sueldo=@Sueldo,
	 FechaContrato=convert(date,@FechaContrato)
	 where idEmpleado= @IdEmpleado       
end

go

create proc sp_eliminarEmpleado
(
@IdEmpleado int
)
as 
begin
	 delete from Empleado where  @IdEmpleado = @IdEmpleado       
end