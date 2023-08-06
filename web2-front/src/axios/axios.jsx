import axios from "axios";
import removeAuthKeys from "../utils/removeAuth";

export const client = axios.create({
  baseURL: process.env.REACT_APP_BACKEND_URL,
});

client.interceptors.request.use(
  (config) => {
    const accessToken = localStorage.getItem("access_token");
    const refreshToken = localStorage.getItem("refresh_token");
    if (accessToken && refreshToken) {
      config.headers.set("Auth-Access-Token", accessToken);
      config.headers.set("Auth-Refresh-Token", refreshToken);
    }

    config.headers.set("Content-Type", "application/json");
    return config;
  },
  (error) => {
    return Promise.reject(error);
  }
);

client.interceptors.response.use(
  (response) => {
    const accessToken = response.headers.get("Auth-Access-Token");
    const refreshToken = response.headers.get("Auth-Refresh-Token");
    if (accessToken && refreshToken) {
      localStorage.setItem("access_token", accessToken);
      localStorage.setItem("refresh_token", refreshToken);
      const decodedToken = decodeURIComponent(
        atob(accessToken.split(".")[1].replace(/-/g, "+").replace(/_/g, "/"))
      );
      const tokenData = JSON.parse(decodedToken);
      const role =
        tokenData[
          "http://schemas.microsoft.com/ws/2008/06/identity/claims/role"
        ];
      console.log(role);
      localStorage.setItem("role", role);
    }
    return response;
  },
  (error) => {
    if (error.response.status === 403) {
      removeAuthKeys();
      window.location.reload();
    }
    return error;
  }
);
