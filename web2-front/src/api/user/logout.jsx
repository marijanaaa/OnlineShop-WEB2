import { client } from "../../axios/axios";

export const logout = () => {
  return client.post("auth/logout");
};
