import { client } from "../axios/axios";

export const approveUser = (input) => {
  return client.post("user/approve", input);
};

export const rejectUser = (input) => {
  return client.post("user/reject", input);
};

export const updateProfile = (input) => {
  return client.put("user/update", input);
};

export const updateItem = (input) => {
  return client.put("item/update", input);
};
