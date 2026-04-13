const defaultApiBaseUrl = "http://localhost:5075";

export const apiBaseUrl = (import.meta.env.VITE_API_BASE_URL?.trim() || defaultApiBaseUrl).replace(/\/$/, "");

interface ApiErrorPayload {
  message?: string;
}

export async function requestJson<T>(path: string, init: RequestInit | undefined, fallbackMessage: string): Promise<T> {
  const response = await fetch(`${apiBaseUrl}${path}`, {
    headers: createHeaders(init?.headers),
    ...init
  });

  if (!response.ok) {
    const payload = (await tryReadJson(response)) as ApiErrorPayload | undefined;
    throw new Error(payload?.message || fallbackMessage);
  }

  if (response.status === 204) {
    return undefined as T;
  }

  return (await response.json()) as T;
}

function createHeaders(headers?: HeadersInit): Headers {
  const mergedHeaders = new Headers(headers);

  if (!mergedHeaders.has("Content-Type")) {
    mergedHeaders.set("Content-Type", "application/json");
  }

  return mergedHeaders;
}

async function tryReadJson(response: Response): Promise<unknown> {
  try {
    return await response.json();
  } catch {
    return undefined;
  }
}