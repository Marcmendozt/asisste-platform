# Asisste

Monorepo base de Asisste reorganizado con una estructura alineada a Microsoft Architecture Guides.

## Solución activa

- Solución canónica: `Asisste.sln`
- App móvil activa: `src/4-Presentation/Asisste.MAUI`
- Documentación de brechas y migración: `docs/`

## Estructura actual

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
├── monorepo-structure.md
└── legacy-gap-analysis.md
```

## Estado de transición

La organización física y de solución ya quedó preparada para la migración tecnológica, pero todavía hay componentes transitorios:

- `Asisste.MAUI` es la app activa y concentra la UI moderna.
- `Asisste.Data` aún contiene servicios demo y wrappers del dispositivo heredados de la fase MAUI inicial; en la siguiente fase debe separarse hacia API + EF Core 10.
- `Asisste.API` y `Asisste.Web` quedaron creados como placeholders documentados para la migración posterior.
- La comparación entre el MAUI actual y los legados funcionales ya está documentada en `docs/legacy-gap-analysis.md`.

## Legado archivado

Todo el material histórico salió del nivel raíz y quedó agrupado en `archive/`:

- `archive/legacy-xamarin-forms-complete`
- `archive/legacy-wcf-backend`
- `archive/legacy-aspnet-mvc-monolith`

Se conservaron solo las líneas históricas que sí aportan lógica útil para la migración. Las variantes Xamarin incompletas se descartaron porque eran snapshots/template sin valor funcional adicional frente al Xamarin legacy completo.

## Capacidades vigentes en la base moderna

- Login demo en MAUI
- Navegación con Shell
- Pantallas de Items, Location y About
- Inyección de dependencias
- Repositorio en memoria para demo

## Brechas ya identificadas

Las capacidades críticas que existen en legado y todavía no están en la base moderna incluyen:

- Marcación de asistencia real
- Gestión de faltas
- Usuarios reales y sesión persistida
- Reportes y listados
- Catálogos maestros: cargo, horario, jornada, tipo documento, género, ubicación
- SQLite local funcional o persistencia real
- API/backend moderno equivalente al WCF y al MVC legado

## Credenciales demo actuales

- Usuario: `demo@asisste.app`
- Contraseña: `Asisste123!`

## Build rápido

Compilar la solución:

```powershell
dotnet build Asisste.sln
```

Compilar la app MAUI Android:

```powershell
dotnet build .\src\4-Presentation\Asisste.MAUI\Asisste.MAUI.csproj -t:Run -f net10.0-android
```

## Documentación clave

- `docs/monorepo-structure.md`: mapa de carpetas, proyectos y reglas de transición.
- `docs/legacy-gap-analysis.md`: comparación funcional entre MAUI actual, Xamarin completo, Web MVC y WCF.

## Nota operativa

La migración tecnológica todavía no empezó. Este cambio deja listo el repo para arrancar la siguiente fase sobre una estructura consistente de monorepo.
