import { useState } from "react";
import { useAuth } from "../auth/AuthContext";
import { guestLogin } from "../api/auth";
import { useNavigate } from "react-router-dom";

export default function LoginPage() {
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const auth = useAuth();
  const navigate = useNavigate();

  async function handleSubmit(e: React.FormEvent) {
    e.preventDefault();
    const token = await guestLogin(email, password);
    auth.login(token);
    navigate("/");
  }

  return (
    <span>
      <form onSubmit={handleSubmit}>
        <h2>Login</h2>
        <input value={email} onChange={e => setEmail(e.target.value)} />
        <input
          type="password"
          value={password}
          onChange={e => setPassword(e.target.value)}
        />
        <button type="submit">Login</button>
      </form>

      <button>
        Login with Google
      </button>
    </span>
  );
}
