# Perfil Slice Migration

## Fuente legacy analizada

| Legacy | Rol heredado | Destino nuevo |
| --- | --- | --- |
| `Web.Marcacion.Entidades/bePerfil.cs` | modelo de datos | `Asisste.Domain/Entities/Profile.cs` |
| `Web.Marcacion.Reglas/brPerfil.cs` | coordinación de reglas y acceso | `Asisste.Services/Profiles/ProfileService.cs` |
| `Web.Marcacion.Datos/daPerfil.cs` | acceso SQL por stored procedures | `Asisste.ApiData/Profiles/EfProfileRepository.cs` |
| `Views/Maestros/Perfil.cshtml` | UI MVC | `Asisste.Web/src/features/profiles/components/ProfilesPage.tsx` |
| `js/Perfil/Perfil.js` | estado de UI, paginación, llamados AJAX | `Asisste.Web/src/features/profiles/hooks/useProfiles.ts` y `api/profileApi.ts` |

## Reglas trasladadas al Application layer

- descripción obligatoria
- longitud mínima de 4 caracteres
- detección de duplicados
- eliminación de respuestas string tipo `Correcto_` / `Error_`

## Decisiones de arquitectura

- El contrato de persistencia vive en dominio para respetar inversión de dependencia.
- La lógica transaccional del módulo vive en `Asisste.Services`; los controladores quedan delgados.
- La infraestructura server se implementa en `Asisste.ApiData` porque el repositorio actual separa backend y cliente móvil. No se usa `Asisste.Data` para EF Core porque ese proyecto hoy compila para Android y mezclarlo con SQL Server rompería la separación técnica.
- El acceso de datos usa EF Core sobre stored procedures legacy para desacoplar el caso de uso del `SqlConnection` manual y dejar una ruta clara hacia tablas/mapeos nativos más adelante.
- La vista React consume la API moderna y reemplaza el DOM scripting imperativo por un hook de estado y un servicio HTTP tipado.