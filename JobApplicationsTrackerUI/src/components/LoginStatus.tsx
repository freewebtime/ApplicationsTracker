import { useAuth } from "../auth/AuthContext";
import { useNavigate } from "react-router-dom";

export default function LoginStatus() {
  const { isAuthenticated, logout } = useAuth();
  const navigate = useNavigate();

  return (
    <div style={{ padding: "10px", borderBottom: "1px solid #ccc" }}>
      {isAuthenticated ? (
        <button
          onClick={() => {
            logout();
            navigate("/login");
          }}
        >
          Logout
        </button>
      ) : (
        <button onClick={() => navigate("/login")}>
          Login
        </button>
      )}
    </div>
  );
}