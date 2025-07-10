# Sistema de Peaje API

Una API RESTful desarrollada en .NET Core 9 para gestionar un sistema de peaje automatizado.

## Características

- **Gestión de Vehículos**: Registro y administración de vehículos
- **Estaciones de Peaje**: Configuración de estaciones y tarifas
- **Transacciones**: Procesamiento automático de transacciones de peaje
- **Cálculo de Tarifas**: Sistema flexible de tarifas por tipo de vehículo
- **Reportes**: Consulta de transacciones por diferentes criterios

## Tecnologías Utilizadas

- **.NET Core 9**
- **Entity Framework Core**
- **SQL Server / LocalDB**
- **AutoMapper**
- **Swagger/OpenAPI**
- **FluentValidation**

## Estructura del Proyecto

```
SistemaPeaje.API/
├── Controllers/          # Controladores de la API
├── Models/              # Modelos de datos (entidades)
├── DTOs/                # Data Transfer Objects
├── Services/            # Lógica de negocio
├── Repositories/        # Acceso a datos
├── Data/                # Contexto de Entity Framework
├── Helpers/             # Utilidades y AutoMapper profiles
├── Middlewares/         # Middlewares personalizados
└── Program.cs           # Configuración de la aplicación
```

## Modelos Principales

### Vehicle (Vehículo)
- Placa, tipo de vehículo, marca, modelo, año, color

### TollStation (Estación de Peaje)
- Nombre, ubicación, coordenadas, estado activo

### TollRate (Tarifa de Peaje)
- Tarifas específicas por tipo de vehículo y estación

### TollTransaction (Transacción de Peaje)
- Registro de paso por peaje con monto, método de pago, estado

## Endpoints Principales

### Vehículos
- `GET /api/vehicles` - Obtener todos los vehículos
- `GET /api/vehicles/{id}` - Obtener vehículo por ID
- `GET /api/vehicles/by-license-plate/{licensePlate}` - Obtener vehículo por placa
- `POST /api/vehicles` - Crear nuevo vehículo
- `PUT /api/vehicles/{id}` - Actualizar vehículo
- `DELETE /api/vehicles/{id}` - Eliminar vehículo

### Peaje
- `POST /api/toll/process` - Procesar transacción de peaje
- `GET /api/toll/calculate/{tollStationId}/{vehicleType}` - Calcular tarifa
- `GET /api/toll/transactions/{id}` - Obtener transacción por ID
- `GET /api/toll/transactions/vehicle/{vehicleId}` - Transacciones por vehículo
- `GET /api/toll/transactions/station/{tollStationId}` - Transacciones por estación
- `GET /api/toll/transactions/date-range` - Transacciones por rango de fechas

## Configuración

### Base de Datos
La aplicación utiliza SQL Server LocalDB por defecto. La cadena de conexión se encuentra en `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=(localdb)\\mssqlLocalDB;Database=SistemaPeajeDB;Trusted_Connection=true;MultipleActiveResultSets=true"
  }
}
```

### Datos Iniciales
La aplicación incluye datos semilla (seed data) que se crean automáticamente:
- 2 estaciones de peaje predefinidas
- Tarifas para cada tipo de vehículo en cada estación

## Instalación y Ejecución

### Prerequisitos
- .NET 9 SDK
- SQL Server LocalDB (incluido con Visual Studio)

### Pasos
1. **Clonar el repositorio**
   ```bash
   git clone [repository-url]
   cd SistemaPeaje.API
   ```

2. **Restaurar paquetes**
   ```bash
   dotnet restore
   ```

3. **Ejecutar la aplicación**
   ```bash
   dotnet run
   ```

4. **Acceder a la documentación**
   - Swagger UI: `https://localhost:[puerto]/`
   - La base de datos se crea automáticamente al iniciar la aplicación

## Tipos de Vehículo

1. **Motorcycle** - Motocicleta
2. **Car** - Automóvil
3. **Van** - Camioneta
4. **Truck** - Camión
5. **Bus** - Autobús
6. **Trailer** - Tráiler

## Métodos de Pago

1. **Cash** - Efectivo
2. **CreditCard** - Tarjeta de crédito
3. **DebitCard** - Tarjeta de débito
4. **ElectronicTag** - Tag electrónico
5. **MobilePayment** - Pago móvil
6. **BankTransfer** - Transferencia bancaria

## Estados de Transacción

1. **Pending** - Pendiente
2. **Completed** - Completada
3. **Failed** - Fallida
4. **Cancelled** - Cancelada
5. **Refunded** - Reembolsada

## Características Técnicas

- **Arquitectura Clean**: Separación clara de responsabilidades
- **Repository Pattern**: Abstracción del acceso a datos
- **Unit of Work**: Manejo consistente de transacciones
- **AutoMapper**: Mapeo automático entre entidades y DTOs
- **Manejo de Errores**: Middleware personalizado para excepciones
- **Validaciones**: Validaciones de modelo y lógica de negocio
- **Documentación**: Swagger/OpenAPI integrado
- **CORS**: Configurado para desarrollo

## Desarrollo

### Agregar Migraciones (si es necesario)
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

### Ejecutar Tests
```bash
dotnet test
```

## Contribuir

1. Fork el proyecto
2. Crear una rama para tu feature (`git checkout -b feature/AmazingFeature`)
3. Commit tus cambios (`git commit -m 'Add some AmazingFeature'`)
4. Push a la rama (`git push origin feature/AmazingFeature`)
5. Abrir un Pull Request

## Licencia

Este proyecto está bajo la Licencia MIT - ver el archivo [LICENSE](LICENSE) para detalles.
