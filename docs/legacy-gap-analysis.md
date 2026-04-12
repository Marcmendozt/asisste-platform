# Legacy Gap Analysis

## Fuentes comparadas

| Fuente | Rol actual |
| --- | --- |
| `src/4-Presentation/Asisste.MAUI` | base moderna activa |
| `archive/legacy-xamarin-forms-complete` | UX y lógica móvil legado más completa |
| `archive/legacy-aspnet-mvc-monolith` | fuente principal de dominio, reglas y catálogos |
| `archive/legacy-wcf-backend/WCFAPPAsisste` | referencia técnica secundaria y backend WCF histórico |

## Analisis puntual MAUI movil

Para el contraste especifico entre la app MAUI actual y la referencia movil legacy completa, ver `docs/maui-mobile-gap-analysis.md`.

## Qué existe hoy en la base moderna

- Login demo
- Navegación Shell
- Pantallas demo de items
- Pantalla de ubicación básica
- Servicios demo para autenticación y persistencia en memoria

## Qué existe en legado y aún falta migrar

| Capacidad | MVC legado | Xamarin completo | Base MAUI actual | Estado |
| --- | --- | --- | --- | --- |
| Marcación de asistencia | Sí | Sí | No | Crítica |
| Gestión de faltas | Sí | Sí | No | Crítica |
| Usuarios reales | Sí | Sí | Demo | Crítica |
| Reportes/listados | Sí | No | No | Alta |
| Catálogos maestros | Sí | Parcial | No | Alta |
| Perfiles y módulos | Sí | No | No | Alta |
| Geolocalización real de negocio | No | Sí | Parcial | Alta |
| SQLite local | No | Sí | No | Alta |
| API/backend moderno | No | No | No | Crítica |

## Hallazgos concretos

### Legado móvil completo

La variante `legacy-xamarin-forms-complete` contiene los flujos que hoy más valor aportan para la migración del móvil:

- `Vistas/Marcar.xaml`: clock-in y clock-out real
- `Vistas/Faltas.xaml`: envío de faltas
- `Servicios/Geolocalizacion`: geolocalización funcional
- `Servicios/SQL`: persistencia SQLite local
- `Servicios/Clases/ServiceImei.cs`: identidad de dispositivo

### Depuración de variantes Xamarin

Tras comparar las variantes históricas de Xamarin, se conservó solo `legacy-xamarin-forms-complete`.

- El snapshot inicial era un template temprano sin lógica de negocio reutilizable.
- La variante móvil descartada repetía el mismo patrón de template y no aportaba funcionalidades únicas frente al código completo.
- El backend WCF histórico se separó a `legacy-wcf-backend/WCFAPPAsisste` para no mezclarlo con referencias móviles.

### Legado web MVC

La variante `legacy-aspnet-mvc-monolith` es la fuente principal para reconstruir dominio y casos de uso:

- `Web.Marcacion.Entidades`: entidades maestras y de negocio
- `Web.Marcacion.Reglas`: reglas por módulo
- `Web.Marcacion.Datos`: acceso a datos legado

Módulos detectados por nombre y estructura:

- asistencia
- faltas
- usuarios
- cargos
- horarios
- jornadas
- perfiles
- módulos
- género
- tipo documento
- tipo jornada
- ubicaciones y áreas de trabajo

### WCF histórico

`legacy-wcf-backend/WCFAPPAsisste` sirve solo como referencia del contrato antiguo. No debe migrarse tal cual; debe reemplazarse por `Asisste.API`.

## Incongruencias actuales en la base moderna

- `Asisste.Data` todavía contiene servicios demo del dispositivo y no un acceso a datos real.
- La base MAUI moderna no implementa los flujos centrales del negocio.
- No existe aún backend ASP.NET Core para consumir desde MAUI o Web.
- La persistencia sigue siendo de demostración.

## Recomendación de extracción por capa

| Destino | Fuente prioritaria | Tipo de contenido |
| --- | --- | --- |
| `Asisste.Domain` | MVC legado | entidades y value objects |
| `Asisste.Services` | MVC legado | casos de uso y contratos |
| `Asisste.Data` | MVC legado + Xamarin completo | EF Core 10, repositorios, servicios técnicos |
| `Asisste.API` | MVC legado + WCF | endpoints modernos |
| `Asisste.MAUI` | Xamarin completo | UI y flujos reales móviles |
| `Asisste.Web` | MVC legado | portal web moderno |

## Prioridad sugerida para la siguiente fase

1. API de autenticación, usuarios y asistencia.
2. Entidades maestras y reglas núcleo.
3. Marcación móvil real con geolocalización.
4. Faltas y subida de evidencias.
5. Reportes y portal web.