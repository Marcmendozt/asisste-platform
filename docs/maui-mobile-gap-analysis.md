# MAUI Mobile Gap Analysis

## Objetivo

Comparar la app movil actual en `src/4-Presentation/Asisste.MAUI` contra la referencia funcional de `archive/legacy-xamarin-forms-complete` para definir que falta migrar y en que orden conviene hacerlo.

## Estado actual del MAUI

La base moderna todavia conserva la estructura del template y no el flujo real del negocio.

### Evidencia directa

- Login demo: `src/4-Presentation/Asisste.MAUI/Features/Authentication/ViewModels/LoginViewModel.cs`
  - precarga `DemoCredentials`
  - si falla muestra `Usa el acceso demo configurado`
- Autenticacion demo: `src/3-Infrastructure/Asisste.Data/Authentication/DemoAuthenticationService.cs`
  - valida solo `demo@asisste.app` / `Asisste123!`
- Navegacion actual: `src/4-Presentation/Asisste.MAUI/AppShell.xaml.cs`
  - solo publica `Acceso`, `Explorar`, `Ubicacion` y `Acerca de`
- Ubicacion basica: `src/4-Presentation/Asisste.MAUI/Features/Location/ViewModels/UbicacionViewModel.cs`
  - obtiene latitud, longitud y precision
  - no marca asistencia ni valida geovalla
- Servicio de ubicacion: `src/3-Infrastructure/Asisste.Data/Device/LocationService.cs`
  - usa `Geolocation.Default.GetLocationAsync`
  - no abre configuracion GPS ni aplica reglas de negocio
- Persistencia demo: `src/3-Infrastructure/Asisste.Data/Data/InMemoryItemRepository.cs`
  - repositorio en memoria para `Item`
- Modelo actual: `src/1-Core/Asisste.Domain/Entities/Item.cs`
  - no existen entidades de usuario, asistencia, falta o ubicacion de negocio

## Lo que si resuelve el legacy movil completo

### Evidencia directa

- Login real y registro de dispositivo: `archive/legacy-xamarin-forms-complete/XAMAsisste/XAMAsisste/XAMAsisste/VistaModelos/LoginVistaModelo.cs`
- Marcacion entrada/salida: `archive/legacy-xamarin-forms-complete/XAMAsisste/XAMAsisste/XAMAsisste/Vistas/Marcar.xaml.cs`
- Geovalla y validacion diaria de asistencia: `archive/legacy-xamarin-forms-complete/XAMAsisste/XAMAsisste/XAMAsisste/Vistas/Marcar.xaml.cs`
- Envio de faltas con archivo: `archive/legacy-xamarin-forms-complete/XAMAsisste/XAMAsisste/XAMAsisste/VistaModelos/FaltasVistaModelo.cs`
- Persistencia SQLite local: `archive/legacy-xamarin-forms-complete/XAMAsisste/XAMAsisste/XAMAsisste/Servicios/SQL/BaseDatos/BDAsisste.cs`
- Identidad del dispositivo: `archive/legacy-xamarin-forms-complete/XAMAsisste/XAMAsisste/XAMAsisste/Servicios/Clases/ServiceImei.cs`

## Brecha funcional puntual

| Capacidad | Legacy completo | MAUI actual | Brecha |
| --- | --- | --- | --- |
| Login real contra backend | Si | No | Critica |
| Registro / identidad de dispositivo | Si | No | Alta |
| Marcacion de entrada | Si | No | Critica |
| Marcacion de salida | Si | No | Critica |
| Validacion de una asistencia por dia | Si | No | Alta |
| Geovalla / Haversine | Si | No | Alta |
| Apertura de settings GPS | Si | No | Media |
| Vista de faltas con adjunto | Si | No | Critica |
| Validacion de una falta por dia | Si | No | Alta |
| Persistencia local SQLite | Si | No | Alta |
| Navegacion orientada al negocio | Si | Parcial | Alta |
| Catalogos / reglas de negocio | Parcial | No | Alta |

## Que ya se puede reutilizar del MAUI

- `AppShell.xaml.cs`: estructura Shell y visibilidad segun autenticacion.
- `Common/ViewModels/ViewModelBase.cs`: base MVVM actual.
- `Features/Location/ViewModels/UbicacionViewModel.cs`: patron para obtener y presentar ubicacion.
- `src/3-Infrastructure/Asisste.Data/Device/LocationService.cs`: acceso MAUI moderno a geolocalizacion.
- `src/4-Presentation/Asisste.MAUI/DependencyInjection.cs`: punto unico para registrar nuevos servicios y paginas.

## Lo que falta crear o reemplazar

### Dominio y contratos

- Entidades nuevas en `src/1-Core/Asisste.Domain/Entities`:
  - `Usuario`
  - `Asistencia`
  - `Falta`
  - `UbicacionLaboral` o equivalente
  - `DispositivoRegistrado`
- Contratos nuevos en `src/2-Application/Asisste.Services/Abstractions`:
  - autenticacion real
  - asistencia
  - faltas
  - identidad de dispositivo
  - geovalla
  - almacenamiento local / sesion

### Infraestructura

- Cliente HTTP centralizado para la API moderna.
- Persistencia real en SQLite o almacenamiento local equivalente.
- Sustituir `DemoAuthenticationService` por autenticacion real.
- Servicio de identidad de dispositivo con fallback moderno; no depender solo de IMEI.

### Presentacion MAUI

- Feature `Attendance` con una pagina `Marcar` y su ViewModel.
- Feature `Absences` con una pagina `Faltas` y su ViewModel.
- Integrar reloj en vivo, estados de marcado y mensajes de negocio.
- Agregar rutas / items de Shell para los modulos reales.

## Priorizacion recomendada

### Bloque 1: base funcional

Objetivo: dejar el movil autenticando con datos reales y preparado para el resto del flujo.

1. Reemplazar autenticacion demo por autenticacion HTTP real.
2. Definir entidades de negocio minimas: usuario, asistencia, falta y dispositivo.
3. Crear cliente API centralizado y configuracion externa de endpoints.
4. Implementar persistencia local minima para sesion y usuario actual.

### Bloque 2: asistencia

Objetivo: recuperar la funcion central del negocio.

1. Crear la vista MAUI de marcacion.
2. Implementar consulta de asistencia del dia.
3. Implementar marcado de entrada y salida.
4. Incorporar geolocalizacion de negocio y validacion de geovalla.
5. Resolver activacion de GPS desde UX o con guia al usuario.

### Bloque 3: faltas y endurecimiento

Objetivo: completar paridad MVP con el legado movil util.

1. Crear la vista MAUI de faltas con selector de archivo.
2. Implementar envio de evidencia y validacion de una falta por dia.
3. Endurecer manejo de errores, estados de carga y mensajes.
4. Agregar pruebas unitarias al menos para ViewModels y servicios criticos.

## Primer slice recomendado

Si quieres reducir riesgo, el primer corte vertical deberia ser este:

1. Login real.
2. Obtener usuario y sesion actual.
3. Consultar si ya existe asistencia del dia.
4. Marcar solo entrada con ubicacion.

Ese slice deja validado el contrato base de la API y reduce mucho la incertidumbre antes de migrar salida, geovalla completa y faltas.

## Riesgos que conviene resolver antes del backend moderno

- El legado usa endpoints hardcoded a `201.240.192.217`; no deben copiarse tal cual.
- IMEI ya no es una base confiable en Android moderno; conviene un identificador persistente compatible con la plataforma.
- La geovalla hoy vive mezclada en UI legacy; debe moverse a servicio de aplicacion o a backend.
- El estado global en el legacy usa `Application.Current.Properties`; en MAUI conviene encapsularlo en servicios con DI.

## Conclusion

La brecha no esta en la infraestructura visual de MAUI, sino en que todavia no se migraron los flujos reales de negocio. El orden correcto es: autenticacion real, asistencia, luego faltas. Todo lo demas puede esperar.