import { useState } from "react";
import type { AuthenticatedSession } from "../types";

const authStorageKey = "asisste.web.auth";

const legacySessionKeys = {
  userId: "DTIDUsuario",
  fullName: "DTNombreCompleto",
  username: "DTNombreUsuario",
  entryTime: "DTHoraEntrada",
  tolerance: "DTTolerancia"
} as const;

export function useAuthSession() {
  const [session, setSession] = useState<AuthenticatedSession | null>(() => readStoredSession());

  function completeSignIn(nextSession: AuthenticatedSession) {
    persistSession(nextSession);
    setSession(nextSession);
  }

  function signOut() {
    clearSession();
    setSession(null);
  }

  return {
    session,
    completeSignIn,
    signOut
  };
}

function readStoredSession(): AuthenticatedSession | null {
  if (typeof window === "undefined") {
    return null;
  }

  const rawSession = window.sessionStorage.getItem(authStorageKey);
  if (!rawSession) {
    return null;
  }

  try {
    const parsedSession = JSON.parse(rawSession) as unknown;

    if (isAuthenticatedSession(parsedSession)) {
      return parsedSession;
    }
  } catch {
  }

  clearSession();
  return null;
}

function persistSession(session: AuthenticatedSession) {
  if (typeof window === "undefined") {
    return;
  }

  window.sessionStorage.setItem(authStorageKey, JSON.stringify(session));
  window.sessionStorage.setItem(legacySessionKeys.userId, String(session.userId));
  window.sessionStorage.setItem(legacySessionKeys.fullName, session.fullName);
  window.sessionStorage.setItem(legacySessionKeys.username, session.username);
  window.sessionStorage.setItem(legacySessionKeys.entryTime, session.entryTime);
  window.sessionStorage.setItem(legacySessionKeys.tolerance, String(session.toleranceMinutes));
}

function clearSession() {
  if (typeof window === "undefined") {
    return;
  }

  window.sessionStorage.removeItem(authStorageKey);
  window.sessionStorage.removeItem(legacySessionKeys.userId);
  window.sessionStorage.removeItem(legacySessionKeys.fullName);
  window.sessionStorage.removeItem(legacySessionKeys.username);
  window.sessionStorage.removeItem(legacySessionKeys.entryTime);
  window.sessionStorage.removeItem(legacySessionKeys.tolerance);
}

function isAuthenticatedSession(value: unknown): value is AuthenticatedSession {
  if (!value || typeof value !== "object") {
    return false;
  }

  const session = value as Partial<AuthenticatedSession>;

  return (
    typeof session.userId === "number" &&
    typeof session.username === "string" &&
    typeof session.fullName === "string" &&
    typeof session.entryTime === "string" &&
    typeof session.toleranceMinutes === "number" &&
    typeof session.mobileId === "number" &&
    typeof session.hasMobileSession === "boolean"
  );
}