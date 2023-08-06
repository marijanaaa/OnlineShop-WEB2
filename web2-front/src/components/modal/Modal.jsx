import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import TextField from "@mui/material/TextField";
import { useState } from "react";
import Resizer from "react-image-file-resizer";
import { addItem } from "../../api/create";
import { useNavigate } from "react-router-dom";
import { AuthContext } from "../../auth/authProvider";
import { useContext } from "react";

const style = {
  position: "absolute",
  top: "50%",
  left: "50%",
  transform: "translate(-50%, -50%)",
  width: 400,
  bgcolor: "background.paper",
  border: "2px solid #000",
  boxShadow: 24,
  p: 4,
};

const BasicModal = ({ open, handleClose, fetchItems }) => {
  const { user } = useContext(AuthContext);
  const [name, setName] = useState("");
  const [price, setPrice] = useState("");
  const [amount, setAmount] = useState("");
  const [description, setDescription] = useState("");
  const [picture, setPicture] = useState("");
  const navigate = useNavigate();
  console.log("basic");

  const handleAddItem = async (e) => {
    e.preventDefault();
    const input = {
      name: name,
      price: price,
      amount: amount,
      description: description,
      picture: picture,
      userId: user.id,
    };
    //promeniti
    const response = await addItem(input);
    if (response.status !== 200) {
      return navigate("/");
    }
    fetchItems();
  };
  const handleModalClose = () => {
    handleClose();
  };

  const resizeFile = (file) =>
    new Promise((resolve) => {
      Resizer.imageFileResizer(
        file,
        300,
        300,
        "JPEG",
        100,
        0,
        (uri) => {
          resolve(uri);
        },
        "base64"
      );
    });

  const handleImageChange = async (event) => {
    try {
      const file = event.target.files[0];
      const image = await resizeFile(file);
      setPicture(image);
    } catch (err) {
      console.log(err);
    }
  };
  return (
    <div>
      <Modal
        open={open}
        onClose={handleModalClose}
        aria-labelledby="modal-modal-title"
        aria-describedby="modal-modal-description"
      >
        <Box sx={style}>
          <Typography id="modal-modal-title" variant="h6" component="h2">
            Add Item
          </Typography>
          <form onSubmit={handleAddItem}>
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
              <TextField
                label="Name"
                fullWidth
                onChange={(e) => setName(e.target.value)}
              />
              <TextField
                label="Price"
                fullWidth
                type="number"
                sx={{ mt: "1rem" }}
                onChange={(e) => setPrice(e.target.value)}
              />
              <TextField
                label="Amount"
                fullWidth
                type="number"
                sx={{ mt: "1rem" }}
                onChange={(e) => setAmount(e.target.value)}
              />
              <TextField
                label="Description"
                fullWidth
                sx={{ mt: "1rem" }}
                onChange={(e) => setDescription(e.target.value)}
              />
              <input
                type="file"
                accept="image/*"
                sx={{ mt: "1rem" }}
                onChange={handleImageChange}
              />
            </Typography>

            <Button
              variant="contained"
              color="primary"
              onClick={handleAddItem}
              sx={{ mt: "1rem" }}
              type="submit"
            >
              Add
            </Button>
          </form>
        </Box>
      </Modal>
    </div>
  );
};

export default BasicModal;
