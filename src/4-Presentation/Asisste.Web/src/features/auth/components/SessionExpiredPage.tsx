import { Link } from "react-router-dom";
import { AuthFrame } from "./AuthFrame";

export function SessionExpiredPage() {
  return (
    <AuthFrame
      eyebrow="Home/TimeOver"
      title="La sesión ha expirado"
      description="Esta ruta reemplaza la página legacy de expiración y deja preparado el punto de retorno al login para cualquier flujo futuro de timeout o revocación de sesión."
      asideEyebrow="Continuidad del portal"
      asideTitle="Retorno claro al inicio de sesión"
      asideDescription="Se mantiene el comportamiento esperado del MVC: avisar el vencimiento y devolver al usuario a un acceso limpio. Cuando exista control de tiempo de sesión en el web nuevo, esta vista ya está lista para reutilizarse."
      facts={[
        { label: "Vista legacy", value: "Home/TimeOver" },
        { label: "Acción primaria", value: "Reingresar" },
        { label: "Integración", value: "Lista para enlazar" }
      ]}
    >
      <div className="space-y-5">
        <div className="rounded-[28px] border border-slate-200 bg-slate-50 px-5 py-4 text-sm leading-6 text-slate-600">
          La sesión fue invalidada o venció. El portal conserva esta ruta para futuros controles de timeout,
          cierre remoto o reautenticación obligatoria.
        </div>

        <div className="flex flex-wrap gap-3">
          <Link className="btn-primary" to="/login">
            Ingresar nuevamente
          </Link>
          <Link className="btn-secondary" to="/recuperar-clave">
            Recuperar clave
          </Link>
        </div>
      </div>
    </AuthFrame>
  );
}