export interface LoginDraft {
  username: string;
  password: string;
}

export interface AuthenticatedSession {
  userId: number;
  username: string;
  fullName: string;
  entryTime: string;
  toleranceMinutes: number;
  mobileId: number;
  hasMobileSession: boolean;
}