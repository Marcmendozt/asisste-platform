import type { ReactNode } from "react";

interface AuthFact {
  label: string;
  value: string;
}

interface AuthFrameProps {
  eyebrow: string;
  title: string;
  description: string;
  asideEyebrow: string;
  asideTitle: string;
  asideDescription: string;
  facts: AuthFact[];
  children: ReactNode;
}

export function AuthFrame({
  eyebrow,
  title,
  description,
  asideEyebrow,
  asideTitle,
  asideDescription,
  facts,
  children
}: AuthFrameProps) {
  return (
    <main className="relative min-h-screen overflow-hidden px-4 py-6 sm:px-6 lg:px-8">
      <div className="mx-auto flex min-h-[calc(100vh-3rem)] max-w-7xl items-stretch">
        <section className="grid flex-1 gap-6 lg:grid-cols-[1.04fr_0.96fr]">
          <article className="glass-panel flex flex-col justify-between p-6 sm:p-8 lg:p-10">
            <div>
              <div className="brand-chip">A</div>
              <p className="mt-8 pill-note">{eyebrow}</p>
              <h1 className="mt-5 max-w-3xl font-display text-4xl text-slate-950 sm:text-5xl lg:text-6xl">
                {title}
              </h1>
              <p className="mt-5 max-w-2xl text-base leading-7 text-slate-600 sm:text-lg">{description}</p>
            </div>

            <div className="mt-10">{children}</div>
          </article>

          <aside className="relative overflow-hidden rounded-[32px] border border-asisste-200/50 bg-[linear-gradient(160deg,#132d44_0%,#1f5b86_52%,#d99517_140%)] p-6 text-white shadow-[0_28px_80px_rgba(19,45,68,0.24)] sm:p-8 lg:p-10">
            <div className="absolute -left-16 top-10 h-48 w-48 rounded-full bg-white/10 blur-2xl" />
            <div className="absolute bottom-0 right-0 h-56 w-56 translate-x-10 translate-y-10 rounded-full bg-brandgold-100/20 blur-3xl" />

            <div className="relative z-10 flex h-full flex-col justify-between gap-8">
              <div>
                <p className="text-[0.72rem] font-bold uppercase tracking-[0.34em] text-white/70">{asideEyebrow}</p>
                <h2 className="mt-5 max-w-xl font-display text-3xl sm:text-4xl">{asideTitle}</h2>
                <p className="mt-5 max-w-xl text-base leading-7 text-white/82">{asideDescription}</p>
              </div>

              <dl className="grid gap-4 sm:grid-cols-3 lg:grid-cols-1">
                {facts.map((fact) => (
                  <div key={fact.label} className="rounded-[28px] border border-white/12 bg-white/10 px-5 py-5 backdrop-blur-md">
                    <dt className="text-[0.68rem] font-bold uppercase tracking-[0.3em] text-white/62">{fact.label}</dt>
                    <dd className="mt-3 text-lg font-semibold text-white">{fact.value}</dd>
                  </div>
                ))}
              </dl>
            </div>
          </aside>
        </section>
      </div>
    </main>
  );
}