# ReactTSWithNetCoreTemplate

React TypeScript with .NET Core Web API template


## **1. Agregar una Migraci�n con EF Core**

Crea una nueva migraci�n de base de datos basada en los cambios de tu modelo.
Aseg�rate de ejecutar este comando desde la **ra�z de tu proyecto** (donde se encuentra el archivo `.sln`).
Hay que realizar una migracion luego de cada cambio que se haga en el esquema.

```bash
dotnet ef migrations add [NombreDeTuMigracion] -p src\ReactTSWithNetCoreTemplate.Infrastructure -s src\ReactTSWithNetCoreTemplate.API
```

## **2. Levantar los Servicios (Build & Run)**

Este comando detiene y elimina los contenedores anteriores, reconstruye las im�genes (si hay cambios en el Dockerfile o el contexto) y luego levanta todos los servicios en modo *detached* (en segundo plano).


```bash
docker-compose -p reacttswithnetcoretemplate down ;
docker-compose -p reacttswithnetcoretemplate up --build -d
```

---

## Quick Access Links

* **API Health Check:** [http://localhost:5010/health](http://localhost:5010/health)
* **API Swagger UI:** [http://localhost:5010/swagger](http://localhost:5010/swagger)
* **React Client:** [http://localhost:3000](http://localhost:3000)


