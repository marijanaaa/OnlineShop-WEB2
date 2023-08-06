import { client } from "../axios/axios";

export const read = (input) => {
  return client.post("read", input);
};

export const list = () => {
  return client.get("user/list");
};

export const getStatus = (input) => {
  return client.post("user/status", input);
};

export const listAllAdmin = () => {
  return client.get("user/listAll");
};

export const listItems = (input) => {
  return client.post("item/get", input);
};

export const getItem = (input) => {
  return client.post("item/getById", input);
};

export const listAllItems = () => {
  return client.get("item/getAll");
};
