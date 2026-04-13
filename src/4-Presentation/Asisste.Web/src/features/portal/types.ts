export type PortalPhase = "connected" | "portal";

export type PortalSectionId = "administracion" | "jornadas" | "control" | "ubicaciones" | "comunicaciones";

export interface PortalSection {
  id: PortalSectionId;
  title: string;
  summary: string;
}

export interface PortalModule {
  id: string;
  title: string;
  route: string;
  section: PortalSectionId;
  legacyPath: string;
  phase: PortalPhase;
  summary: string;
  description: string;
  dependencies: string[];
}