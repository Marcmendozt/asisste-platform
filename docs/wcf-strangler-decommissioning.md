# Migración WCF/ASMX a Web API/BFF

## Contexto

El backend archivado en `archive/legacy-wcf-backend/WCFAPPAsisste` no es un WCF completo sino un ASMX clásico que hoy expone un slice mínimo de login móvil (`WSLogin`) y ejecuta `sp_ListarUsuarioMovil` contra SQL Server.

## Slice 1 implementado

- Nuevo BFF: `POST /api/mobile/auth/login`
- Compatibilidad táctica: `GET /WSAsisste.asmx/WSLogin?Usuario=...&Clave=...`
- Fuente de datos: `sp_ListarUsuarioMovil`

## Patrón Strangler Fig

1. Poner la nueva API delante del slice antiguo sin tocar todavía el resto del legado.
2. Mover consumidores al endpoint nuevo cuando sea posible.
3. Mantener la ruta de compatibilidad para clientes que todavía no se pueden cambiar.
4. Repetir el proceso por slices funcionales: usuarios, asistencia, faltas, maestros, reportes.

## Plan de decommissioning

1. Shadow traffic o validación paralela del login contra ambos backends.
2. Corte de lectura/escritura del login hacia la API .NET 10.
3. Observabilidad: tasa de error, latencia, usuarios afectados y diferencias de payload.
4. Congelar el ASMX para ese slice.
5. Retirar la ruta legacy del proxy una vez que no quede tráfico activo.

## Riesgos actuales

- El legado tenía la cadena de conexión hardcodeada en `Web.config`; no debe migrarse tal cual.
- El stored procedure sigue siendo dependencia fuerte del dominio actual.
- El resto de endpoints móviles activos aún dependen del backend ASP.NET MVC legado y no del ASMX.

## Siguiente corte recomendado

Después del login ASMX, el siguiente paso natural no es ampliar este ASMX, sino mover los endpoints móviles que hoy consume MAUI desde el backend MVC legado hacia la misma API/BFF para consolidar el estrangulamiento en un solo frontend server-side.