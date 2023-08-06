import { createContext, useEffect, useState } from "react";
import { login } from "../api/user/login";
import { logout } from "../api/user/logout";

import { read } from "../api/get";

import removeAuthKeys from "../utils/removeAuth";

export const AuthContext = createContext({});

const AuthProvider = ({ children }) => {
  const [isLoggedIn, setIsLoggedIn] = useState(false);
  const [isLoading, setIsLoading] = useState(true);
  const [user, setUser] = useState({});
  const [isGoogleLogged, setIsGoogleLogged] = useState(false);
  const [isApproved, setIsApproved] = useState(false);

  /*const fetchUserInfo = async () => {
    const input = {
      entity: "user",
    };

    const response = await read(input);
    if (response.status !== 200) {
      removeAuthKeys();
      setIsLoggedIn(false);
      setIsLoading(false);
      return;
    }

    setUser(response.data.data);
    setIsLoggedIn(true);
    setIsLoading(false);
  };

  */

  const handleUserInfo = (input) => {
    setUser((prevUser) => ({ ...prevUser, ...input }));
  };

  useEffect(() => {
    if (!localStorage.getItem("access_token")) {
      setIsLoading(false);
      return;
    }
    //fetchUserInfo();
    setIsLoading(false);
  }, []);

  const handleLogin = async (email, password, setErrorMessage) => {
    const input = {
      email: email,
      password: password,
    };

    const response = await login(input);
    if (response.status !== 200) {
      setErrorMessage("incorrect username or password");
      return;
    }
    setUser(response.data);
    if (response.data.userStatus == 1) {
      setIsApproved(true);
    }
    console.log(response.data.userStatus);
    console.log(user.userStatus);
    console.log(isApproved);
    setIsLoggedIn(true);
  };

  const handleGoogleLogin = async (input) => {
    setUser(input);
    setIsGoogleLogged(true);
    setIsLoggedIn(true);
  };

  const handleLogout = async () => {
    //await logout();
    console.log("logout");
    removeAuthKeys();
    setIsLoggedIn(false);
    setIsGoogleLogged(false);
    setIsApproved(false);
  };

  return (
    <AuthContext.Provider
      value={{
        isLoading,
        isLoggedIn,
        isGoogleLogged,
        user,
        isApproved,
        handleLogin,
        handleLogout,
        handleGoogleLogin,
        handleUserInfo,
      }}
    >
      {children}
    </AuthContext.Provider>
  );
};

export default AuthProvider;
