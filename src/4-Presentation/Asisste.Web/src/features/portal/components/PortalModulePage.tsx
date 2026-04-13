import { Link } from "react-router-dom";
import type { PortalModule } from "../types";
import { MigrationBadge } from "./MigrationBadge";

interface PortalModulePageProps {
  module: PortalModule;
}

const migratedUiChecklist = [
  "Ruta React incorporada al portal protegido.",
  "Diseño actualizado con Tailwind y jerarquía visual consistente.",
  "Contexto técnico documentado para conectar el slice backend correspondiente."
];

export function PortalModulePage({ module }: PortalModulePageProps) {
  return (
    <div className="space-y-6">
      <section className="glass-panel p-6 sm:p-8">
        <div className="flex flex-col gap-6 xl:flex-row xl:items-start xl:justify-between">
          <div className="max-w-3xl">
            <div className="flex flex-wrap items-center gap-3">
              <span className="pill-note">{module.legacyPath}</span>
              <MigrationBadge phase={module.phase} />
            </div>

            <h2 className="mt-4 font-display text-3xl text-slate-950 sm:text-4xl">{module.title}</h2>
            <p className="mt-4 text-base leading-7 text-slate-600">{module.description}</p>
          </div>

          <div className="soft-panel p-4 xl:w-[320px]">
            <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Entrega actual</p>
            <dl className="mt-3 space-y-3 text-sm leading-6 text-slate-600">
              <div>
                <dt className="font-semibold text-slate-900">Ruta React</dt>
                <dd>/portal/{module.route}</dd>
              </div>
              <div>
                <dt className="font-semibold text-slate-900">Origen MVC</dt>
                <dd>{module.legacyPath}</dd>
              </div>
              <div>
                <dt className="font-semibold text-slate-900">Estado</dt>
                <dd>UI migrada, integración backend pendiente</dd>
              </div>
            </dl>
          </div>
        </div>
      </section>

      <section className="grid gap-6 xl:grid-cols-[1.1fr_0.9fr]">
        <article className="soft-panel p-6 sm:p-7">
          <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Alcance ya migrado</p>
          <h3 className="mt-3 font-display text-2xl text-slate-950">Base visual y de navegación lista</h3>
          <ul className="mt-5 space-y-3 text-sm leading-6 text-slate-600">
            {migratedUiChecklist.map((item) => (
              <li key={item} className="rounded-[22px] bg-slate-50 px-4 py-3">
                {item}
              </li>
            ))}
          </ul>
        </article>

        <aside className="soft-panel p-6 sm:p-7">
          <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Siguiente slice técnico</p>
          <h3 className="mt-3 font-display text-2xl text-slate-950">Qué falta conectar</h3>
          <ul className="mt-5 space-y-3 text-sm leading-6 text-slate-600">
            {module.dependencies.map((dependency) => (
              <li key={dependency} className="rounded-[22px] bg-slate-50 px-4 py-3">
                {dependency}
              </li>
            ))}
          </ul>
        </aside>
      </section>

      <section className="grid gap-6 lg:grid-cols-2">
        <article className="soft-panel p-6 sm:p-7">
          <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Resumen funcional</p>
          <p className="mt-4 text-base leading-7 text-slate-600">{module.summary}</p>
        </article>

        <article className="soft-panel p-6 sm:p-7">
          <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Siguiente validación útil</p>
          <p className="mt-4 text-base leading-7 text-slate-600">
            Cuando exista el controller moderno de este módulo, esta misma ruta podrá recibir su listado real,
            filtros, formularios y acciones sin necesidad de rehacer la navegación ni el diseño.
          </p>

          <div className="mt-6 flex flex-wrap gap-3">
            <Link className="btn-primary" to="/portal/administracion/perfiles">
              Revisar módulo operativo
            </Link>
            <Link className="btn-secondary" to="/portal">
              Volver al inicio
            </Link>
          </div>
        </article>
      </section>
    </div>
  );
}