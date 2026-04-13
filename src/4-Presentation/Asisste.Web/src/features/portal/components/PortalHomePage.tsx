import { Link } from "react-router-dom";
import type { AuthenticatedSession } from "../../auth/types";
import { getModuleHref, getSectionModules, portalModules, portalSections } from "../data/modules";
import { MigrationBadge } from "./MigrationBadge";

interface PortalHomePageProps {
  session: AuthenticatedSession;
}

export function PortalHomePage({ session }: PortalHomePageProps) {
  const connectedCount = portalModules.filter((module) => module.phase === "connected").length;
  const routedCount = portalModules.length + 3;
  const pendingIntegrationCount = portalModules.filter((module) => module.phase === "portal").length;

  return (
    <div className="space-y-6">
      <section className="grid gap-6 xl:grid-cols-[1.35fr_0.85fr]">
        <article className="glass-panel p-6 sm:p-8">
          <div className="flex flex-wrap gap-3">
            <span className="pill-note">Maestros/Inicio</span>
            <span className="pill-note">React + Tailwind</span>
          </div>

          <h2 className="mt-5 max-w-4xl font-display text-4xl text-slate-950 sm:text-5xl">
            Centro de operación del portal Asisste
          </h2>
          <p className="mt-5 max-w-3xl text-base leading-7 text-slate-600 sm:text-lg">
            El frontend nuevo ya contiene la estructura completa del portal MVC: login, vistas auxiliares,
            navegación por secciones y rutas equivalentes para todos los módulos principales. Perfiles ya está
            conectado; el resto quedó preparado para enlazar slices backend sin rehacer la UI.
          </p>

          <div className="mt-8 flex flex-wrap gap-3">
            <Link className="btn-primary" to="/portal/administracion/perfiles">
              Abrir módulo operativo
            </Link>
            <Link className="btn-secondary" to="/recuperar-clave">
              Ver recuperación de clave
            </Link>
          </div>
        </article>

        <aside className="soft-panel p-6 sm:p-7">
          <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Sesión activa</p>
          <h3 className="mt-3 font-display text-2xl text-slate-950">{session.fullName}</h3>
          <p className="mt-2 text-sm text-slate-500">{session.username}</p>

          <dl className="mt-6 space-y-3 text-sm text-slate-600">
            <div className="flex items-center justify-between gap-4 rounded-[22px] bg-slate-50 px-4 py-3">
              <dt>Entrada</dt>
              <dd className="font-semibold text-slate-950">{formatEntryTime(session.entryTime)}</dd>
            </div>
            <div className="flex items-center justify-between gap-4 rounded-[22px] bg-slate-50 px-4 py-3">
              <dt>Tolerancia</dt>
              <dd className="font-semibold text-slate-950">{session.toleranceMinutes} min</dd>
            </div>
            <div className="flex items-center justify-between gap-4 rounded-[22px] bg-slate-50 px-4 py-3">
              <dt>Sesión móvil</dt>
              <dd className="font-semibold text-slate-950">{session.hasMobileSession ? "Activa" : "No"}</dd>
            </div>
          </dl>
        </aside>
      </section>

      <section className="grid gap-4 md:grid-cols-3">
        <article className="metric-panel">
          <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Módulos conectados</p>
          <p className="mt-4 font-display text-4xl text-slate-950">{connectedCount}</p>
          <p className="mt-2 text-sm leading-6 text-slate-600">Slices funcionando contra el API moderno y SQL real.</p>
        </article>

        <article className="metric-panel">
          <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Rutas React listas</p>
          <p className="mt-4 font-display text-4xl text-slate-950">{routedCount}</p>
          <p className="mt-2 text-sm leading-6 text-slate-600">Incluye acceso, vistas auxiliares y todas las secciones principales del portal.</p>
        </article>

        <article className="metric-panel">
          <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Integraciones pendientes</p>
          <p className="mt-4 font-display text-4xl text-slate-950">{pendingIntegrationCount}</p>
          <p className="mt-2 text-sm leading-6 text-slate-600">Pantallas ya maquetadas que necesitan controller y casos de uso modernos.</p>
        </article>
      </section>

      <div className="space-y-8">
        {portalSections.map((section) => {
          const modules = getSectionModules(section.id);

          return (
            <section key={section.id} className="space-y-4">
              <div className="flex flex-col gap-2 sm:flex-row sm:items-end sm:justify-between">
                <div>
                  <p className="pill-note">{section.title}</p>
                  <h3 className="mt-4 font-display text-2xl text-slate-950">{section.summary}</h3>
                </div>
                <p className="max-w-2xl text-sm leading-6 text-slate-500">
                  Cada tarjeta abre su ruta React correspondiente y documenta el siguiente bloque de backend que
                  falta integrar.
                </p>
              </div>

              <div className="grid gap-4 md:grid-cols-2 2xl:grid-cols-3">
                {modules.map((module) => (
                  <Link key={module.id} className="module-link-card" to={getModuleHref(module)}>
                    <div className="flex flex-col gap-4 sm:flex-row sm:items-start sm:justify-between">
                      <div>
                        <p className="text-[0.68rem] font-bold uppercase tracking-[0.28em] text-slate-400">
                          {module.legacyPath}
                        </p>
                        <h4 className="mt-3 text-xl font-semibold text-slate-950">{module.title}</h4>
                      </div>
                      <MigrationBadge className="shrink-0" phase={module.phase} />
                    </div>

                    <p className="mt-4 text-sm leading-6 text-slate-600">{module.summary}</p>
                    <p className="mt-5 text-sm font-semibold text-asisste-700">Abrir módulo</p>
                  </Link>
                ))}
              </div>
            </section>
          );
        })}
      </div>
    </div>
  );
}

function formatEntryTime(entryTime: string) {
  const [time] = entryTime.split(".");
  return time.length >= 5 ? time.slice(0, 5) : entryTime;
}