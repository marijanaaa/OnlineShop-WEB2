import { client } from "../../axios/axios";

export const login = (input) => {
  return client.post("auth/login", input);
};
