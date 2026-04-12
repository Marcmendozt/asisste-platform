# Asisste.API

Primer slice real del backend ASP.NET Core en .NET 10.

## Objetivo

Aplicar Strangler Fig sobre el backend legado empezando por el login móvil del ASMX `WSAsisste`.

## Slice implementado

- `POST /api/mobile/auth/login`: endpoint BFF moderno para login móvil.
- `GET /WSAsisste.asmx/WSLogin?Usuario=...&Clave=...`: endpoint de compatibilidad para cortar tráfico del ASMX sin reescribir el cliente en el mismo paso.

## Fuente principal de migración

- `archive/legacy-wcf-backend/WCFAPPAsisste`

## Configuración

La API espera `ConnectionStrings:LegacyAssiste` en configuración. El valor por defecto en `appsettings.json` es solo un placeholder local y debe sustituirse por una conexión real fuera del repositorio.

## Estrategia de corte

1. Desplegar la API nueva en paralelo al ASMX.
2. Redirigir primero el login móvil hacia `POST /api/mobile/auth/login` o, si hace falta compatibilidad táctica, hacia `GET /WSAsisste.asmx/WSLogin` servido por esta API.
3. Medir tráfico y errores; una vez estable, congelar cambios en el ASMX.
4. Continuar con usuarios, asistencia, faltas y maestros hasta retirar el backend legado.