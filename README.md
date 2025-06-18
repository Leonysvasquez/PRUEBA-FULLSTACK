# PRUEBA-FULLSTACK

# ProductSales

Sistema de gestión de productos, clientes y ventas desarrollado como prueba técnica. Incluye backend en ASP.NET Core 8 y frontend en Vue.js.

---

## Descripción del Proyecto

ProductSales permite:

* Gestionar productos (CRUD)
* Gestionar clientes (CRUD)
* Registrar ventas
* Consultar historial de ventas
* Autenticación con JWT y control de roles (`admin` y `vendedor`)

---

## Tecnologías Utilizadas

### Backend (.NET 8)

* ASP.NET Core Web API
* Entity Framework Core
* SQL Server (InMemory para pruebas)
* JWT Authentication
* xUnit para pruebas unitarias

### Frontend (Vue.js)

* Vue 3 + Vite
* Vue Router
* Axios
* Bootstrap CSS (básico)

---

## Instrucciones para Ejecutar la Aplicación

### 1. Clonar Repositorios

#### Backend:

```bash
git clone https://github.com/Leonysvasquez/PRUEBA-FULLSTACK.git
```

#### Frontend:

```bash
git clone https://github.com/Leonysvasquez/Front-End-ProductSales.git
```

---

## 2. Ejecutar Backend (ASP.NET Core)

### Requisitos:

* SDK .NET 8
* Visual Studio 2022 (o VS Code con C# plugin)

### Instrucciones:

```bash
cd PRUEBA-FULLSTACK/ProductSales

dotnet restore
dotnet ef database update
dotnet run
```

El backend se ejecutará por defecto en `https://localhost:7002`

Puedes probar los endpoints desde `https://localhost:7002/swagger`

---

## 3. Ejecutar Frontend (Vue.js)

### Requisitos:

* Node.js 18+
* npm o yarn

### Instrucciones:

```bash
cd Front-End-ProductSales
npm install
npm run dev
```

El frontend estará disponible en `http://localhost:5173`

---

## Pruebas Unitarias

Están implementadas en el proyecto `ProductSales.Tests` usando xUnit y una base de datos en memoria.

Para ejecutar:

```bash
cd ProductSales

dotnet test
```

---

## Autor

**Leonys Vasquez**

* GitHub: [Leonysvasquez](https://github.com/Leonysvasquez)

---

## Licencia

Este proyecto es parte de una prueba técnica y no requiere licencia comercial.
