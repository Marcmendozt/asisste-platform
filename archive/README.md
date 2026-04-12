# Archive

Este directorio concentra todas las variantes legadas del producto que ya no deben vivir en el nivel raíz del repositorio.

## Contenido

- `legacy-xamarin-forms-complete`: variante Xamarin.Forms con lógica real de marcación, faltas, geolocalización y SQLite.
- `legacy-wcf-backend`: backend WCF/ASMX histórico conservado solo como referencia de contratos y comportamiento técnico.
- `legacy-aspnet-mvc-monolith`: portal ASP.NET MVC con entidades, reglas y acceso a datos que hoy sirve como fuente principal de negocio para migrar.

Las variantes Xamarin incompletas fueron eliminadas después de validar que no aportaban lógica móvil única frente a `legacy-xamarin-forms-complete`.

## Regla de uso

Nada dentro de `archive/` debe considerarse código activo. Se conserva únicamente para:

- auditoría
- comparación funcional
- extracción controlada de entidades, reglas y casos de uso
- soporte a la migración tecnológica