import { client } from "../axios/axios";

export const removeEntity = (input) => {
  return client.post("delete", input);
};

export const removeItems = (input) => {
  return client.post("item/delete", input);
};
