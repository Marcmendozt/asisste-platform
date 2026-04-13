import { Link } from "react-router-dom";
import { AuthFrame } from "./AuthFrame";

export function RecoveryPage() {
  return (
    <AuthFrame
      eyebrow="Home/RecuperarContraseña"
      title="Recuperación de clave"
      description="La vista ya existe en React y conserva su lugar dentro del flujo del portal, pero la automatización de correo y validación de identidad todavía debe migrarse al backend moderno."
      asideEyebrow="Estado de migración"
      asideTitle="Pantalla portada, servicio pendiente"
      asideDescription="Este corte deja lista la experiencia visual y la navegación. La siguiente iteración debe conectar el envío de correo, la expiración del token y la confirmación de cambio de contraseña."
      facts={[
        { label: "Vista legacy", value: "Home/RecuperarContraseña" },
        { label: "Frontend", value: "React + Tailwind listo" },
        { label: "Backend", value: "Pendiente de integración" }
      ]}
    >
      <div className="space-y-5">
        <div className="rounded-[28px] border border-amber-200 bg-amber-50 px-5 py-4 text-sm font-semibold leading-6 text-amber-800">
          La recuperación aún no envía correos reales desde el nuevo stack. Se dejó la ruta disponible para
          preservar la experiencia del portal mientras se construye el slice correspondiente.
        </div>

        <div className="soft-panel p-5">
          <p className="text-[0.68rem] font-bold uppercase tracking-[0.32em] text-slate-400">Lo que falta conectar</p>
          <ul className="mt-4 space-y-3 text-sm leading-6 text-slate-600">
            <li>Servicio de generación de token y expiración segura.</li>
            <li>Plantilla de correo y proveedor SMTP o servicio equivalente.</li>
            <li>Endpoint moderno para solicitar y confirmar el restablecimiento.</li>
          </ul>
        </div>

        <div className="flex flex-wrap gap-3">
          <Link className="btn-primary" to="/login">
            Volver al acceso
          </Link>
          <Link className="btn-secondary" to="/sesion-expirada">
            Ver sesión expirada
          </Link>
        </div>
      </div>
    </AuthFrame>
  );
}