# Assiste

Aplicación móvil migrada a .NET MAUI desde Xamarin.Forms.

Este repositorio conserva dos líneas de trabajo:

- La aplicación activa en .NET MAUI dentro de src.
- El código Xamarin legado archivado en archive/xamarin-legacy como referencia histórica.

## Estado actual

La solución activa es Assiste.sln y la interfaz principal vive en src/Assiste.Presentation.Maui.

La app ya está operativa sobre Android con:

- .NET 10
- .NET MAUI Single Project
- Shell para navegación
- Inyección de dependencias con Microsoft.Extensions.DependencyInjection
- CommunityToolkit.Mvvm para viewmodels y comandos
- Autenticación demo
- Repositorio en memoria para elementos de ejemplo

## Estructura de la solución

- src/Assiste.Domain: entidades y modelo de dominio.
- src/Assiste.Application: contratos y abstracciones de aplicación.
- src/Assiste.Infrastructure: implementaciones concretas de servicios, autenticación y persistencia temporal.
- src/Assiste.Presentation.Maui: aplicación MAUI, navegación, páginas, recursos y viewmodels.
- archive/xamarin-legacy: snapshot del proyecto Xamarin original, fuera de la solución activa.

## Funcionalidad disponible

### Acceso

La pantalla de inicio de sesión usa autenticación demo para facilitar pruebas locales.

Credenciales actuales:

- Usuario: demo@assiste.app
- Contraseña: Assiste123!

El login está implementado con un servicio demo registrado en infraestructura, por lo que hoy no depende de backend ni base de datos.

### Navegación

La aplicación usa Shell y habilita el menú principal después del acceso. Las secciones activas son:

- Acceso
- Explorar
- Ubicación
- Acerca de

### Explorar

La pantalla Explorar muestra una lista de elementos de ejemplo y permite:

- Ver detalle de un elemento
- Crear un nuevo elemento

Actualmente estos datos se guardan en memoria durante la ejecución de la app. Al cerrar la aplicación se pierden.

### Ubicación

La pantalla de ubicación usa APIs del dispositivo para obtener coordenadas actuales. No requiere base de datos.

### Acerca de

La pantalla informativa resume la migración y enlaza a documentación de .NET MAUI.

## Persistencia actual

La solución no usa base de datos real por ahora.

El módulo de elementos usa un repositorio en memoria en:

- src/Assiste.Infrastructure/Data/InMemoryItemRepository.cs

Esto es suficiente para demostrar navegación, formularios, detalle y flujo general, pero no para un escenario productivo.

Si se requiere persistencia real, los siguientes caminos naturales son:

- SQLite local dentro de la app
- API remota con base de datos en servidor
- Sincronización híbrida local/remota

## Requisitos de entorno

Para compilar y ejecutar la app Android se necesita:

- .NET SDK 10
- Workload maui-android
- Android SDK
- Un emulador o dispositivo Android disponible

Target framework actual:

- net10.0-android

## Cómo ejecutar la aplicación

### Compilar la solución

```powershell
dotnet build Assiste.sln
```

### Ejecutar la app MAUI sobre Android

```powershell
dotnet build .\src\Assiste.Presentation.Maui\Assiste.Presentation.Maui.csproj -t:Run -f net10.0-android
```

Si el SDK de Android no está en el PATH o la resolución automática falla, puedes indicar la ruta explícitamente:

```powershell
dotnet build .\src\Assiste.Presentation.Maui\Assiste.Presentation.Maui.csproj -t:Run -f net10.0-android -p:AndroidSdkDirectory="$env:LOCALAPPDATA\Android\Sdk"
```

## Arquitectura de presentación

La app sigue una separación simple por capas:

- Domain: entidades puras
- Application: interfaces y contratos
- Infrastructure: implementaciones de servicios
- Presentation: UI, navegación y estado de pantalla

Dentro de Presentation, la organización es por features:

- Features/Authentication
- Features/Items
- Features/Location
- Features/About
- Common/Navigation
- Common/ViewModels

## Decisiones actuales

### Autenticación

La autenticación actual es local y de demostración. Se añadió para facilitar la validación del flujo completo de la app sin depender de un backend externo.

### Base de datos

No existe una base de datos real integrada todavía. Esto es intencional en la etapa actual de la migración para priorizar:

- estructura de solución
- navegación
- inyección de dependencias
- bindings compilados
- validación visual y funcional en Android

### Legacy archivado

El contenido de archive/xamarin-legacy se conserva solo para auditoría, comparación o recuperación puntual de código. No forma parte del build activo.

## Siguientes pasos sugeridos

- Reemplazar la autenticación demo por autenticación real
- Sustituir el repositorio en memoria por SQLite o una API
- Persistir sesión de usuario
- Agregar pruebas y validaciones de negocio
- Ampliar el módulo de items con edición y eliminación desde UI

## Notas de mantenimiento

- Evita subir bin y obj al repositorio.
- El proyecto activo es el contenido bajo src; archive solo documenta el legado.
- Si cambias el flujo de acceso o persistencia, actualiza este README junto con la implementación.
