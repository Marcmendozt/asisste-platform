export interface Profile {
  id: number;
  description: string;
  isActive: boolean;
}

export interface ProfileDraft {
  description: string;
}

export type ProfileEditorMode = "create" | "edit";