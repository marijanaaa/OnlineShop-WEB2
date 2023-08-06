import React from "react";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import { AuthContext } from "../../auth/authProvider";
import { useContext } from "react";
import { getStatus } from "../../api/get";

const StatusPage = () => {
  const { user } = useContext(AuthContext);
  const handleCheckStatus = async () => {
    const input = {
      email: user.email,
    };
    const response = await getStatus(input);
    console.log("Status checked: " + response.data.userStatus);
  };

  return (
    <div>
      <Typography variant="h5">Check your status</Typography>
      <Button variant="contained" onClick={handleCheckStatus}>
        Check
      </Button>
      <Typography variant="h5">{user.userStatus}</Typography>
    </div>
  );
};

export default StatusPage;
