import { URL_POSTFIX_LOGIN } from "./const";
import * as auth from "./auth";

export async function apiRequest(url: string, options: RequestInit = {}) {
  const token = auth.getToken();

  const headers = {
    "Content-Type": "application/json",
    ...(token ? { Authorization: `Bearer ${token}` } : {}),
    ...(options.headers || {})
  };

  const res = await fetch(url, { ...options, headers });

  if (!res.ok) {
    if (res.status === 401) {
      auth.clearToken();
      window.location.href = URL_POSTFIX_LOGIN;
    }
    throw new Error(`API error: ${res.status}`);
  }

  return res;
}

export async function apiFetch(url: string, options: RequestInit = {}) {
  const result = await apiRequest(url, options);
  return result.json();
}

