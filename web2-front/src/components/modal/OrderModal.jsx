import * as React from "react";
import Box from "@mui/material/Box";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import Modal from "@mui/material/Modal";
import TextField from "@mui/material/TextField";
import { useState } from "react";
import Resizer from "react-image-file-resizer";
import { addOrder } from "../../api/create";
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

const OrderModal = ({ open, handleClose, getAllitems, dictionary }) => {
  const { user } = useContext(AuthContext);
  const [comment, setComment] = useState("");
  const [address, setAddress] = useState("");

  const navigate = useNavigate();

  const handleAddOrder = async (e) => {
    e.preventDefault();
    const input = {
      itemIds: dictionary,
      userId: user.id,
      comment: comment,
      address: address,
    };
    //promeniti
    const response = await addOrder(input);
    if (response.status !== 200) {
      return navigate("/");
    }
    const backendTime = response.data.date;
    const dateTime = new Date(backendTime);
    const hours = dateTime.getHours();
    const minutes = dateTime.getMinutes();
    console.log("hours: " + hours);
    console.log("minutes: " + minutes);
    Object.entries(response.data.dict).forEach(([key, value]) => {
      window.alert(`Article with Id: ${key}, Success: ${value}`);
    });
    getAllitems();
  };
  const handleModalClose = () => {
    handleClose();
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
          <form onSubmit={handleAddOrder}>
            <Typography id="modal-modal-description" sx={{ mt: 2 }}>
              <TextField
                label="Comment"
                fullWidth
                onChange={(e) => setComment(e.target.value)}
              />
              <TextField
                label="Address"
                fullWidth
                sx={{ mt: "1rem" }}
                onChange={(e) => setAddress(e.target.value)}
              />
            </Typography>

            <Button
              variant="contained"
              color="primary"
              onClick={handleAddOrder}
              sx={{ mt: "1rem" }}
              type="submit"
            >
              Add Order
            </Button>
          </form>
        </Box>
      </Modal>
    </div>
  );
};

export default OrderModal;
