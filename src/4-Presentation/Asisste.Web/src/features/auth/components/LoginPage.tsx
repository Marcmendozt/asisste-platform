import { type FormEvent, useState } from "react";
import { Link } from "react-router-dom";
import { authApi } from "../api/authApi";
import type { AuthenticatedSession, LoginDraft } from "../types";
import { AuthFrame } from "./AuthFrame";

interface LoginPageProps {
  onAuthenticated: (session: AuthenticatedSession) => void;
}

export function LoginPage({ onAuthenticated }: LoginPageProps) {
  const [draft, setDraft] = useState<LoginDraft>({
    username: "",
    password: ""
  });
  const [isSubmitting, setIsSubmitting] = useState(false);
  const [errorMessage, setErrorMessage] = useState<string | null>(null);

  const canSubmit = !isSubmitting && draft.username.trim().length > 0 && draft.password.length > 0;

  async function handleSubmit(event: FormEvent<HTMLFormElement>) {
    event.preventDefault();

    if (!draft.username.trim() || !draft.password) {
      setErrorMessage("Ingresa correo y contraseña para continuar.");
      return;
    }

    setIsSubmitting(true);
    setErrorMessage(null);

    try {
      const session = await authApi.login(draft);
      onAuthenticated(session);
    } catch (error) {
      setErrorMessage(getErrorMessage(error));
      setDraft((currentDraft) => ({
        ...currentDraft,
        password: ""
      }));
    } finally {
      setIsSubmitting(false);
    }
  }

  function updateField<K extends keyof LoginDraft>(field: K, value: LoginDraft[K]) {
    setDraft((currentDraft) => ({
      ...currentDraft,
      [field]: value
    }));
  }

  return (
    <AuthFrame
      eyebrow="Acceso histórico, interfaz nueva"
      title="Portal Asisste"
      description="Ingresa con el mismo usuario del backend real y entra al nuevo shell React que ya replica la navegación principal del MVC legado."
      asideEyebrow="Migración activa"
      asideTitle="Login real sobre SQL local"
      asideDescription="El acceso usa el endpoint moderno existente, preserva la sesión en esta pestaña y habilita el portal completo con diseño Tailwind."
      facts={[
        { label: "Persistencia", value: "sessionStorage" },
        { label: "Módulo operativo", value: "Perfiles" },
        { label: "Contratos vigentes", value: "API moderna y compatibilidad legacy" }
      ]}
    >
      <div className="space-y-6">
        <div className="flex flex-wrap gap-3 text-xs font-bold uppercase tracking-[0.28em] text-slate-500">
          <span className="pill-note">Home/Login</span>
          <span className="pill-note">/api/mobile/auth/login</span>
        </div>

        {errorMessage ? (
          <div className="rounded-[28px] border border-rose-200 bg-rose-50 px-5 py-4 text-sm font-semibold text-rose-700">
            {errorMessage}
          </div>
        ) : null}

        <form className="space-y-5" onSubmit={(event) => void handleSubmit(event)}>
          <label className="field-shell" htmlFor="login-username">
            Correo
            <input
              id="login-username"
              className="field-input"
              type="email"
              value={draft.username}
              onChange={(event) => updateField("username", event.target.value)}
              placeholder="usuario@asisste.com"
              autoComplete="username"
              autoFocus
            />
          </label>

          <label className="field-shell" htmlFor="login-password">
            Contraseña
            <input
              id="login-password"
              className="field-input"
              type="password"
              value={draft.password}
              onChange={(event) => updateField("password", event.target.value)}
              placeholder="Ingresar contraseña"
              autoComplete="current-password"
            />
          </label>

          <div className="flex flex-col gap-4 pt-2 sm:flex-row sm:items-end sm:justify-between">
            <div className="space-y-2 text-sm text-slate-500">
              <p>La cuenta debe existir en la base Asisste configurada en el API local.</p>
              <Link className="font-semibold text-asisste-700 transition hover:text-asisste-900" to="/recuperar-clave">
                Olvide mi contraseña
              </Link>
            </div>

            <button className="btn-primary min-w-[190px]" type="submit" disabled={!canSubmit}>
              {isSubmitting ? "Validando acceso..." : "Iniciar sesión"}
            </button>
          </div>
        </form>
      </div>
    </AuthFrame>
  );
}

function getErrorMessage(error: unknown) {
  return error instanceof Error ? error.message : "No fue posible iniciar sesión.";
}