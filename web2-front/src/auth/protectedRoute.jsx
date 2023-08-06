import { Backdrop, CircularProgress } from "@mui/material";
import { useContext } from "react";
import { Navigate, useNavigate } from "react-router-dom";
import removeAuthKeys from "../utils/removeAuth";
import { AuthContext } from "./authProvider";

const ProtectedRoute = (props) => {
  const { isLoading, isLoggedIn, handleLogin } = useContext(AuthContext);
  const navigate = useNavigate();
  if (isLoading) {
    return (
      <Backdrop
        sx={{ color: "#fff", zIndex: (theme) => theme.zIndex.drawer + 1 }}
        open={true}
      >
        <CircularProgress color="inherit" />
      </Backdrop>
    );
  }

  if (!isLoggedIn) {
    return <Navigate to="/" />;
  }
  return props.children;
};

export default ProtectedRoute;
