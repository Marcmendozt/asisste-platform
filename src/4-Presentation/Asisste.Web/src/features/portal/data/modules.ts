import type { PortalModule, PortalSection, PortalSectionId } from "../types";

export const portalSections: PortalSection[] = [
  {
    id: "administracion",
    title: "Administración",
    summary: "Catálogos base, usuarios y configuración operativa."
  },
  {
    id: "jornadas",
    title: "Jornadas",
    summary: "Asignación y parametrización de jornadas laborales."
  },
  {
    id: "control",
    title: "Control",
    summary: "Reportes, asistencias y faltas del personal."
  },
  {
    id: "ubicaciones",
    title: "Ubicaciones",
    summary: "Ubicaciones registradas y reglas geográficas."
  },
  {
    id: "comunicaciones",
    title: "Comunicaciones",
    summary: "Acciones transversales como correo y empresa."
  }
];

export const portalModules: PortalModule[] = [
  {
    id: "profiles",
    title: "Perfiles",
    route: "administracion/perfiles",
    section: "administracion",
    legacyPath: "Maestros/Perfil",
    phase: "connected",
    summary: "CRUD completo conectado al API moderno y a SQL Server.",
    description: "Primer slice totalmente funcional en el nuevo portal, con flujo CRUD real sobre perfiles.",
    dependencies: [
      "Sin bloqueos actuales en frontend.",
      "Mantener el contrato existente de /api/profiles.",
      "Extender pruebas al resto de slices conforme se migren."
    ]
  },
  {
    id: "employees",
    title: "Usuarios registrados",
    route: "administracion/usuarios-registrados",
    section: "administracion",
    legacyPath: "Maestros/Empleados",
    phase: "portal",
    summary: "Vista React lista para enlazar listados, filtros y edición de usuarios.",
    description: "Replica la posición del módulo dentro del menú principal y deja preparada la superficie para gestión de empleados y usuarios registrados.",
    dependencies: [
      "Crear controller moderno para usuarios o empleados.",
      "Migrar consultas de listado, detalle y actualización.",
      "Definir contratos de activación, estado y búsqueda."
    ]
  },
  {
    id: "schedules",
    title: "Horarios",
    route: "administracion/horarios",
    section: "administracion",
    legacyPath: "Maestros/Horarios",
    phase: "portal",
    summary: "Pantalla lista para conectar CRUD de horarios del legacy.",
    description: "Conserva el lugar del catálogo de horarios dentro de Administración y deja preparado el módulo para edición de franjas y reglas horarias.",
    dependencies: [
      "Endpoints modernos para listar, crear y editar horarios.",
      "Mapeo de stored procedures y validaciones de solapamiento.",
      "Modelo de aplicación para turnos y franjas horarias."
    ]
  },
  {
    id: "document-types",
    title: "Tipos de documento",
    route: "administracion/tipos-documento",
    section: "administracion",
    legacyPath: "Maestros/TipoDocumento",
    phase: "portal",
    summary: "Ruta disponible para el catálogo de documentos usado por empleados.",
    description: "Sirve como base para trasladar el mantenimiento de tipos de documento sin perder la navegación del portal legado.",
    dependencies: [
      "Controller de catálogos para tipos de documento.",
      "Casos de uso de alta, edición y baja lógica.",
      "Persistencia en la nueva capa de infraestructura."
    ]
  },
  {
    id: "work-areas",
    title: "Áreas de trabajo",
    route: "administracion/areas-trabajo",
    section: "administracion",
    legacyPath: "Maestros/AreaTrabajo",
    phase: "portal",
    summary: "Superficie de UI lista para migrar áreas y jerarquías internas.",
    description: "Prepara el catálogo de áreas de trabajo con la misma ubicación funcional que tenía en MVC 5.",
    dependencies: [
      "Endpoints de catálogo para áreas de trabajo.",
      "Reglas para jerarquía o relación con empleados.",
      "Pruebas sobre consistencia de nombres y estados."
    ]
  },
  {
    id: "workplaces",
    title: "Lugares de trabajo",
    route: "administracion/lugares-trabajo",
    section: "administracion",
    legacyPath: "Maestros/LugarTrabajo",
    phase: "portal",
    summary: "Ruta preparada para el catálogo de sedes o lugares de trabajo.",
    description: "Mantiene la estructura del mantenimiento de lugares de trabajo y permite completar luego la integración real del catálogo.",
    dependencies: [
      "Endpoints para sedes o lugares de trabajo.",
      "Validaciones de relación con ubicaciones y empleados.",
      "Migración de stored procedures del módulo legacy."
    ]
  },
  {
    id: "positions",
    title: "Cargos",
    route: "administracion/cargos",
    section: "administracion",
    legacyPath: "Maestros/Cargo",
    phase: "portal",
    summary: "Estructura de página lista para el catálogo de cargos.",
    description: "Deja migrado el espacio visual para administrar cargos, asociado a usuarios, áreas y reportes.",
    dependencies: [
      "Controller moderno para cargos.",
      "Operaciones CRUD con validaciones de duplicidad.",
      "Conexión con usuarios o empleados registrados."
    ]
  },
  {
    id: "company",
    title: "Empresa",
    route: "comunicaciones/empresa",
    section: "comunicaciones",
    legacyPath: "Maestros/Empresa",
    phase: "portal",
    summary: "Página lista para centralizar configuración de empresa y tenant.",
    description: "Prepara un espacio único para parámetros institucionales y branding que antes vivían dentro del monolito.",
    dependencies: [
      "Modelo de configuración de empresa.",
      "Endpoints para lectura y actualización de parámetros.",
      "Definir almacenamiento de logo, datos fiscales y correo."
    ]
  },
  {
    id: "assigned-shifts",
    title: "Jornadas asignadas",
    route: "jornadas/asignadas",
    section: "jornadas",
    legacyPath: "Maestros/JornadasAsignadas",
    phase: "portal",
    summary: "La interfaz está lista para conectar asignaciones entre empleados y jornadas.",
    description: "Preserva la navegación del legacy y deja lista la experiencia para asignar jornadas por colaborador o grupo.",
    dependencies: [
      "API para asignación y consulta de jornadas por usuario.",
      "Validaciones de vigencia y conflictos de horario.",
      "Consultas combinadas con horarios y tipos de jornada."
    ]
  },
  {
    id: "shift-types",
    title: "Tipos de jornada",
    route: "jornadas/tipos",
    section: "jornadas",
    legacyPath: "Maestros/TipoJornada",
    phase: "portal",
    summary: "Pantalla preparada para parametrizar jornadas y turnos de trabajo.",
    description: "Migra la presencia del mantenimiento de tipos de jornada con espacio para reglas, tolerancias y horarios asociados.",
    dependencies: [
      "Endpoints modernos para tipos de jornada.",
      "Reglas de aplicación y validación de franjas.",
      "Persistencia y consultas en la nueva infraestructura."
    ]
  },
  {
    id: "report-builder",
    title: "Crear reporte",
    route: "control/reporte",
    section: "control",
    legacyPath: "Maestros/Asistencia",
    phase: "portal",
    summary: "Ruta lista para el generador de reportes de asistencia.",
    description: "Reproduce el acceso principal al reporte de asistencias y deja listo el módulo para filtros, exportación y consolidación de resultados.",
    dependencies: [
      "API de consultas de reporte con filtros por fechas y usuarios.",
      "Generación de exportables o descargas desde el backend moderno.",
      "Optimización de consultas para volúmenes altos."
    ]
  },
  {
    id: "attendance-control",
    title: "Asistencias",
    route: "control/asistencias",
    section: "control",
    legacyPath: "Maestros/ControlAsistencia",
    phase: "portal",
    summary: "Vista lista para el seguimiento diario de marcaciones y asistencias.",
    description: "Permite completar después la tabla operacional de asistencias conservando el menú, jerarquía y contexto del portal original.",
    dependencies: [
      "Endpoint de asistencias con filtros, paginación y estados.",
      "Modelos para regularización o aprobación si aplica.",
      "Integración con jornadas, horarios y faltas."
    ]
  },
  {
    id: "absences",
    title: "Faltas",
    route: "control/faltas",
    section: "control",
    legacyPath: "Maestros/Falta",
    phase: "portal",
    summary: "Pantalla preparada para registrar y consultar faltas del personal.",
    description: "Deja listo el lugar del módulo de faltas para enlazar reglas, tipos de incidencia y cruces con asistencia.",
    dependencies: [
      "API para faltas y ausencias.",
      "Catálogo de motivos o tipos de falta.",
      "Cruce con reportes y control de asistencia."
    ]
  },
  {
    id: "default-locations",
    title: "Ubicaciones predeterminadas",
    route: "ubicaciones/predeterminadas",
    section: "ubicaciones",
    legacyPath: "Maestros/UbicacionesPredeterminadas",
    phase: "portal",
    summary: "Ruta creada para administrar ubicaciones base por empresa o perfil.",
    description: "Prepara la UI que luego podrá asociar ubicaciones predeterminadas a distintos ámbitos del sistema.",
    dependencies: [
      "Endpoints para ubicaciones predeterminadas.",
      "Reglas de asociación por empresa, usuario o perfil.",
      "Persistencia de coordenadas y radios permitidos."
    ]
  },
  {
    id: "user-locations",
    title: "Ubicaciones registradas",
    route: "ubicaciones/registradas",
    section: "ubicaciones",
    legacyPath: "Maestros/UbicacionesPorUsuario",
    phase: "portal",
    summary: "Interfaz lista para ver ubicaciones ligadas a usuarios y marcaciones.",
    description: "Mantiene la estructura de navegación del módulo de ubicaciones por usuario, lista para enlazar mapas, listados y detalle operativo.",
    dependencies: [
      "Consultas modernas de ubicaciones por usuario.",
      "Definición de formato geográfico y filtros.",
      "Relación con asistencia y validación móvil."
    ]
  },
  {
    id: "email",
    title: "Enviar correo",
    route: "comunicaciones/enviar-correo",
    section: "comunicaciones",
    legacyPath: "Maestros/EnviarCorreo",
    phase: "portal",
    summary: "Pantalla base lista para mensajería y notificaciones administrativas.",
    description: "Deja preparado el módulo de comunicaciones para envíos manuales o automatizados desde el nuevo portal.",
    dependencies: [
      "Servicio de correo o notificaciones en backend.",
      "Templates y validación de destinatarios.",
      "Auditoría de envíos y estado de entrega."
    ]
  }
];

export function getSectionModules(sectionId: PortalSectionId) {
  return portalModules.filter((module) => module.section === sectionId);
}

export function getSectionMeta(sectionId: PortalSectionId) {
  return portalSections.find((section) => section.id === sectionId) ?? portalSections[0];
}

export function getModuleHref(module: PortalModule) {
  return `/portal/${module.route}`;
}

export function findPortalModuleByPathname(pathname: string) {
  const normalizedPath = pathname !== "/" && pathname.endsWith("/") ? pathname.slice(0, -1) : pathname;

  return portalModules.find((module) => getModuleHref(module) === normalizedPath) ?? null;
}