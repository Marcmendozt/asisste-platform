import { requestJson } from "../../../shared/api/http";
import type { Profile, ProfileDraft } from "../types";

export const profileApi = {
  list(): Promise<Profile[]> {
    return requestJson<Profile[]>("/api/profiles", undefined, "No fue posible cargar los perfiles.");
  },

  create(draft: ProfileDraft): Promise<Profile> {
    return requestJson<Profile>(
      "/api/profiles",
      {
        method: "POST",
        body: JSON.stringify(draft)
      },
      "No fue posible crear el perfil."
    );
  },

  update(profileId: number, draft: ProfileDraft): Promise<Profile> {
    return requestJson<Profile>(
      `/api/profiles/${profileId}`,
      {
        method: "PUT",
        body: JSON.stringify(draft)
      },
      "No fue posible actualizar el perfil."
    );
  },

  remove(profileId: number): Promise<void> {
    return requestJson<void>(
      `/api/profiles/${profileId}`,
      {
        method: "DELETE"
      },
      "No fue posible eliminar el perfil."
    );
  }
};