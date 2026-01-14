import { API_ENDPOINT_GUEST_LOGIN, MESSAGE_LOGIN_FAILED } from "./const";

const TOKEN_KEY = "auth_token";

export async function guestLogin(email: string, password: string): Promise<string> {
  const res = await fetch(API_ENDPOINT_GUEST_LOGIN, {
    method: "POST",
    headers: { "Content-Type": "application/json" },
    body: JSON.stringify({ email, password })
  });

  if (!res.ok) throw new Error(MESSAGE_LOGIN_FAILED);

  const data = await res.json();
  return data.token;
}

export function getToken(): string | null {
  return localStorage.getItem(TOKEN_KEY);
}

export function setToken(token: string) {
  localStorage.setItem(TOKEN_KEY, token);
}

export function clearToken() {
  localStorage.removeItem(TOKEN_KEY);
}

export function getIsAuthenticated(): boolean {
  return !!getToken();
}
