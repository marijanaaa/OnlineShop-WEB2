import { client } from "../../axios/axios";

export const register = (input) => {
  return client.post("user/register", input);
};
