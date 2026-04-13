import clsx from "clsx";
import { useEffect, useState } from "react";
import { Link, NavLink, Outlet, useLocation } from "react-router-dom";
import type { AuthenticatedSession } from "../../auth/types";
import {
  findPortalModuleByPathname,
  getModuleHref,
  getSectionMeta,
  getSectionModules,
  portalSections
} from "../data/modules";

interface PortalLayoutProps {
  session: AuthenticatedSession;
  onSignOut: () => void;
}

export function PortalLayout({ session, onSignOut }: PortalLayoutProps) {
  const location = useLocation();
  const [isMenuOpen, setIsMenuOpen] = useState(false);

  useEffect(() => {
    setIsMenuOpen(false);
  }, [location.pathname]);

  const currentModule = findPortalModuleByPathname(location.pathname);
  const currentSection = currentModule ? getSectionMeta(currentModule.section) : null;
  const currentTitle = currentModule?.title ?? "Inicio";
  const currentSummary =
    currentModule?.summary ??
    "Portal principal con navegación equivalente al MVC legado, adaptado a React + Tailwind.";
  const todayLabel = new Intl.DateTimeFormat("es-PE", { dateStyle: "full" }).format(new Date());

  return (
    <div className="relative min-h-screen px-4 py-4 sm:px-6 lg:px-8">
      {isMenuOpen ? (
        <button
          aria-label="Cerrar navegación"
          className="fixed inset-0 z-30 bg-slate-950/35 lg:hidden"
          type="button"
          onClick={() => setIsMenuOpen(false)}
        />
      ) : null}

      <div className="mx-auto flex min-h-[calc(100vh-2rem)] max-w-[1700px] gap-6">
        <aside
          className={clsx(
            "fixed inset-y-4 left-4 z-40 flex w-[320px] flex-col gap-4 overflow-hidden rounded-[34px] border border-white/70 bg-[#f7fafc]/92 p-4 shadow-[0_30px_90px_rgba(15,23,42,0.16)] backdrop-blur-xl transition duration-300 lg:static lg:translate-x-0",
            isMenuOpen ? "translate-x-0" : "-translate-x-[115%]"
          )}
        >
          <div className="glass-panel p-5">
            <Link className="flex items-center gap-4" to="/portal">
              <div className="brand-chip">A</div>
              <div>
                <p className="text-[0.7rem] font-bold uppercase tracking-[0.32em] text-slate-400">Asisste</p>
                <h1 className="mt-1 font-display text-2xl text-slate-950">Portal Web</h1>
              </div>
            </Link>
            <p className="mt-4 text-sm leading-6 text-slate-600">
              Shell React que replica la estructura del menú legacy y deja listas las pantallas para ir
              conectando slices backend.
            </p>
          </div>

          <nav className="flex-1 space-y-5 overflow-y-auto pr-1">
            <NavLink
              className={({ isActive }) => clsx("nav-link", isActive ? "nav-link-active" : "nav-link-idle")}
              end
              to="/portal"
            >
              <p className="text-[0.68rem] font-bold uppercase tracking-[0.28em] text-slate-400">Inicio</p>
              <p className="mt-2 text-base font-semibold text-current">Panel principal</p>
              <p className="mt-2 text-sm leading-6 text-slate-500">Resumen de módulos y avance de migración.</p>
            </NavLink>

            {portalSections.map((section) => {
              const sectionModules = getSectionModules(section.id);

              return (
                <section key={section.id} className="space-y-2">
                  <div className="px-2">
                    <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">
                      {section.title}
                    </p>
                    <p className="mt-1 text-sm leading-6 text-slate-500">{section.summary}</p>
                  </div>

                  <div className="space-y-2">
                    {sectionModules.map((module) => (
                      <NavLink
                        key={module.id}
                        className={({ isActive }) =>
                          clsx("nav-link", isActive ? "nav-link-active" : "nav-link-idle")
                        }
                        to={getModuleHref(module)}
                      >
                        <div className="flex items-center gap-3">
                          <span
                            className={
                              module.phase === "connected"
                                ? "h-2.5 w-2.5 rounded-full bg-emerald-500"
                                : "h-2.5 w-2.5 rounded-full bg-amber-400"
                            }
                          />
                          <p className="text-base font-semibold text-current">{module.title}</p>
                        </div>
                        <p className="mt-2 text-sm leading-6 text-slate-500">{module.summary}</p>
                      </NavLink>
                    ))}
                  </div>
                </section>
              );
            })}
          </nav>

          <div className="soft-panel p-4">
            <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Compatibilidad</p>
            <p className="mt-3 text-sm leading-6 text-slate-600">
              El login web y el slice de perfiles ya operan con el API moderno. El móvil y el endpoint legacy
              siguen coexistiendo mientras se migra el resto del dominio.
            </p>
            <Link className="mt-4 inline-flex text-sm font-semibold text-asisste-700 hover:text-asisste-900" to="/recuperar-clave">
              Recuperar clave
            </Link>
          </div>
        </aside>

        <div className="min-w-0 flex-1 lg:pl-0">
          <header className="sticky top-4 z-20 mb-6">
            <div className="glass-panel px-4 py-4 sm:px-6">
              <div className="flex flex-col gap-4 xl:flex-row xl:items-center xl:justify-between">
                <div className="flex items-start gap-3">
                  <button className="btn-secondary lg:hidden" type="button" onClick={() => setIsMenuOpen(true)}>
                    Menu
                  </button>

                  <div>
                    <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">
                      {currentSection ? `${currentSection.title} / ${currentTitle}` : "Portal / Inicio"}
                    </p>
                    <h2 className="mt-2 font-display text-2xl text-slate-950 sm:text-3xl">{currentTitle}</h2>
                    <p className="mt-2 max-w-3xl text-sm leading-6 text-slate-600">{currentSummary}</p>
                  </div>
                </div>

                <div className="flex flex-col gap-3 sm:flex-row sm:items-center">
                  <div className="soft-panel min-w-[250px] px-4 py-3">
                    <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Sesión</p>
                    <p className="mt-2 text-base font-semibold text-slate-950">{session.fullName}</p>
                    <p className="mt-1 text-sm text-slate-500">{session.username}</p>
                    <p className="mt-2 text-xs font-semibold uppercase tracking-[0.22em] text-asisste-700">{todayLabel}</p>
                  </div>

                  <button className="btn-secondary" type="button" onClick={onSignOut}>
                    Cerrar sesión
                  </button>
                </div>
              </div>
            </div>
          </header>

          <main className="pb-8">
            <Outlet />
          </main>
        </div>
      </div>
    </div>
  );
}