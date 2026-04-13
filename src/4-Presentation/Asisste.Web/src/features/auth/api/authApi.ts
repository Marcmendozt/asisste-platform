import { requestJson } from "../../../shared/api/http";
import type { AuthenticatedSession, LoginDraft } from "../types";

interface MobileLoginResponse {
  userId: number;
  fullName: string;
  entryTime: string;
  toleranceMinutes: number;
  mobileId: number;
  hasMobileSession: boolean;
}

export const authApi = {
  async login(draft: LoginDraft): Promise<AuthenticatedSession> {
    const username = draft.username.trim();
    const response = await requestJson<MobileLoginResponse>(
      "/api/mobile/auth/login",
      {
        method: "POST",
        body: JSON.stringify({
          username,
          password: draft.password
        })
      },
      "No fue posible iniciar sesión."
    );

    return {
      username,
      ...response
    };
  }
};