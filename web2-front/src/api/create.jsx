import { client } from "../axios/axios";

export const createEntity = (input) => {
  return client.post("create", input);
};

export const addItem = (input) => {
  return client.post("item/add", input);
};

export const addOrder = (input) => {
  return client.post("order/add", input);
};
