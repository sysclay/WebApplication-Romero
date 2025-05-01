# PRUEBA TECNICA - ROMERO

## Arquitectura 
- NET Core MVC - .NET 8.0
- Microsoft SQL Server 2022 (RTM), Microsoft Corporation Express Edition (64-bit)
- VISUAL STUDIO 2022

## Base de datos 
- Creamos la base de datos `wayuser` desde SQL SERVER

## Configuración
- Paquetes instalados desde Nuget. Instalar cada paquete.
- Microsoft.EntityFrameworkCore.SqlServer
- Microsoft.EntityFrameworkCore.Tools
- Swashbuckle.AspNetCore.Swagger
- Microsoft.VisualStudio.Web.CodeGeneration.Design

- Para ejecutar el proyecto de manera local desde su PC cambiar en el archivo "appsettings.json" por la dirección de su servidor de SqlServer 

```json
{
  "ConnectionStrings": {
    "ConnectionDatabase": "Server=NombreDeSuServicor;Database=wayuser;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```
- Para crear la tabla Users desde el models, en Visual Studio entra en "Herramientas" luego "Administrador de Paquetes NuGet" luego  "Consola del Administrador de Paquetes", una ves este en la Consola del Administrador de Paquetes.
- Copiar estos 2 comandos para hacer la migracion a la base de datos 'wayuser'.
```json
{
	Add-Migration InitialCreate
}
```
```json
{
	Update-Database
}
```
- Ejecutado esos comando cada uno a la ves, listo tendra la tabla users en la BD 'wayuser'
- Opcional :: Si no puede crear la tabla de esa manera, ejecute el script desde Sql Server y ya tendra la tabla creada.
# SCRIPT SQL
```sql
/********* CREAMOS TABLA [Users]*****/
CREATE TABLE [dbo].[Users](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[nombre] [nvarchar](max) NOT NULL,
	[telefono] [nvarchar](max) NOT NULL,
	[correo] [nvarchar](max) NOT NULL,
	[nombre_company] [nvarchar](max) NOT NULL,
	[calle] [nvarchar](max) NOT NULL,
	[latitud] [nvarchar](max) NOT NULL,
	[longitud] [nvarchar](max) NOT NULL,
 CONSTRAINT [PK_Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
```

# LISTO 
- Solo ejecuta el proyecto.
