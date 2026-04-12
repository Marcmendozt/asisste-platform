# Monorepo Structure

## Objetivo

Dejar Asisste con una base única de monorepo donde el código activo, las pruebas y la documentación tengan una ubicación estable antes de iniciar la migración tecnológica.

## Estructura adoptada

```text
src/
├── 1-Core/
│   └── Asisste.Domain
├── 2-Application/
│   └── Asisste.Services
├── 3-Infrastructure/
│   └── Asisste.Data
└── 4-Presentation/
    ├── Asisste.API
    ├── Asisste.MAUI
    └── Asisste.Web

tests/
└── Asisste.Domain.Tests

docs/
└── documentación funcional y de migración
```

## Mapeo desde la estructura anterior

| Origen | Destino canónico | Observación |
| --- | --- | --- |
| `src/Assiste.Domain` | `src/1-Core/Asisste.Domain` | Dominio puro |
| `src/Assiste.Application` | `src/2-Application/Asisste.Services` | Contratos y casos de uso iniciales |
| `src/Assiste.Infrastructure` | `src/3-Infrastructure/Asisste.Data` | Proyecto transitorio; aún mezcla datos demo y servicios de dispositivo |
| `src/Assiste.Presentation.Maui` | `src/4-Presentation/Asisste.MAUI` | App móvil activa |
| `DevStart.Web` | `archive/legacy-aspnet-mvc-monolith` | Referencia principal de negocio legado |
| `XAMAsisste` | `archive/legacy-xamarin-forms-complete` | Referencia principal de UX y flujos móviles legados |
| `XAMARIN/WCFAPPAsisste` | `archive/legacy-wcf-backend/WCFAPPAsisste` | Backend WCF histórico |
| `XAMARIN/APPAsissteXamarin` | eliminado del archive | Template Xamarin sin lógica de negocio útil |
| `archive/xamarin-legacy` | eliminado del archive | Snapshot histórico inicial sin lógica de negocio |

## Reglas actuales

- El desarrollo nuevo debe caer solo en la ruta nueva del monorepo.
- `archive/` es solo referencia histórica.
- La única variante Xamarin legacy que se conserva para extraer funcionalidad móvil es `archive/legacy-xamarin-forms-complete`.
- Las próximas migraciones deben extraer lógica desde el legado hacia `Asisste.Domain`, `Asisste.Services`, `Asisste.Data`, `Asisste.API` y `Asisste.Web`.
- `Asisste.Data` es transitorio hasta que se introduzca EF Core 10 y se desacople la lógica móvil del acceso a datos/dispositivo.

## Próxima fase sugerida

1. Levantar `Asisste.API` como backend ASP.NET Core.
2. Extraer entidades maestras y reglas desde el monolito MVC a `Asisste.Domain` y `Asisste.Services`.
3. Reemplazar repositorios demo por EF Core 10 en `Asisste.Data`.
4. Migrar flujos móviles reales desde el Xamarin completo a `Asisste.MAUI`.
5. Definir la tecnología de `Asisste.Web` y empezar la migración del portal.