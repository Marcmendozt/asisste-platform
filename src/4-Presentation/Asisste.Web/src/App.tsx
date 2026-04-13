import { Navigate, Route, Routes } from "react-router-dom";
import { LoginPage } from "./features/auth/components/LoginPage";
import { RecoveryPage } from "./features/auth/components/RecoveryPage";
import { SessionExpiredPage } from "./features/auth/components/SessionExpiredPage";
import type { AuthenticatedSession } from "./features/auth/types";
import { useAuthSession } from "./features/auth/hooks/useAuthSession";
import { PortalHomePage } from "./features/portal/components/PortalHomePage";
import { PortalLayout } from "./features/portal/components/PortalLayout";
import { PortalModulePage } from "./features/portal/components/PortalModulePage";
import { portalModules } from "./features/portal/data/modules";
import { ProfilesPage } from "./features/profiles/components/ProfilesPage";

const connectedModuleId = "profiles";

export default function App() {
  const { session, completeSignIn, signOut } = useAuthSession();

  return session ? (
    <AuthenticatedApp session={session} onSignOut={signOut} />
  ) : (
    <PublicApp onAuthenticated={completeSignIn} />
  );
}

interface PublicAppProps {
  onAuthenticated: (session: AuthenticatedSession) => void;
}

function PublicApp({ onAuthenticated }: PublicAppProps) {
  return (
    <Routes>
      <Route path="/login" element={<LoginPage onAuthenticated={onAuthenticated} />} />
      <Route path="/recuperar-clave" element={<RecoveryPage />} />
      <Route path="/sesion-expirada" element={<SessionExpiredPage />} />
      <Route path="*" element={<Navigate to="/login" replace />} />
    </Routes>
  );
}

interface AuthenticatedAppProps {
  session: AuthenticatedSession;
  onSignOut: () => void;
}

function AuthenticatedApp({ session, onSignOut }: AuthenticatedAppProps) {
  return (
    <Routes>
      <Route path="/login" element={<Navigate to="/portal" replace />} />
      <Route path="/recuperar-clave" element={<RecoveryPage />} />
      <Route path="/sesion-expirada" element={<SessionExpiredPage />} />

      <Route path="/portal" element={<PortalLayout session={session} onSignOut={onSignOut} />}>
        <Route index element={<PortalHomePage session={session} />} />
        <Route path="inicio" element={<Navigate to="/portal" replace />} />
        <Route path="administracion/perfiles" element={<ProfilesPage session={session} />} />

        {portalModules
          .filter((module) => module.id !== connectedModuleId)
          .map((module) => (
            <Route key={module.id} path={module.route} element={<PortalModulePage module={module} />} />
          ))}

        <Route path="*" element={<Navigate to="/portal" replace />} />
      </Route>

      <Route path="*" element={<Navigate to="/portal" replace />} />
    </Routes>
  );
}