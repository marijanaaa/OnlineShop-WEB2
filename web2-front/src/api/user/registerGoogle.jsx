import { client } from "../../axios/axios";

export const registerGoogle = (input) => {
  return client.post("user/registerGoogle", input);
};
