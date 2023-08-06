import Button from "@mui/material/Button";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import CssBaseline from "@mui/material/CssBaseline";
import Box from "@mui/material/Box";
import Avatar from "@mui/material/Avatar";
import Typography from "@mui/material/Typography";
import HowToRegTwoToneIcon from "@mui/icons-material/HowToRegTwoTone";
import React, { useContext, useEffect, useState } from "react";
import { useNavigate } from "react-router-dom";
import jwt_decode from "jwt-decode";
import { registerGoogle } from "../../api/user/registerGoogle";
import { AuthContext } from "../../auth/authProvider";

const RegisterPage = () => {
  const { handleGoogleLogin } = useContext(AuthContext);
  const [email, setEmail] = useState("");
  const [name, setName] = useState("");
  const [lastname, setLastname] = useState("");

  const [googleToken, setGoogleToken] = useState("");

  const [user, setUser] = useState({});
  const navigate = useNavigate();
  const clickRegisterBasic = async () => {
    return navigate("/registerBasic");
  };
  const handleCallbackResponse = async (response) => {
    var gtoken = response.credential;
    const input = {
      googleToken: gtoken,
    };
    const res = await registerGoogle(input);
    if (res.status !== 200) {
      return;
    }
    console.log(res.data);
    handleGoogleLogin(res.data);
    return navigate("/home");
  };

  useEffect(() => {
    /*global google*/ //this object coming from the script
    google.accounts.id.initialize({
      client_id: process.env.GOOGLE_CLIENT_ID,
      //if we ever do a credentials or someone ever logs in, what function do we want to call
      callback: handleCallbackResponse,
    });
    google.accounts.id.renderButton(document.getElementById("signInDiv"), {
      theme: "outline",
      size: "large",
    });

    google.accounts.id.prompt();
  }, []); //if anything in this array changes it is going to run useEffect again, but we want this effect to only run once

  const theme = createTheme();
  return (
    <ThemeProvider theme={theme}>
      <Container component="main" maxWidth="xs">
        <CssBaseline />
        <Box
          sx={{
            marginTop: 8,
            display: "flex",
            flexDirection: "column",
            alignItems: "center",
          }}
        >
          <Avatar sx={{ m: 1, bgcolor: "secondary.main" }}>
            <HowToRegTwoToneIcon />
          </Avatar>
          <Typography component="h1" variant="h5">
            Register
          </Typography>

          <Button
            fullWidth
            variant="contained"
            sx={{ mt: 3, mb: 2 }}
            onClick={clickRegisterBasic}
          >
            Register
          </Button>
          <div id="signInDiv"></div>
        </Box>
      </Container>
    </ThemeProvider>
  );
};

export default RegisterPage;
