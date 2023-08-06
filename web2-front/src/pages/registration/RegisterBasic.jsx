import React, { useContext, useEffect, useState } from "react";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import CssBaseline from "@mui/material/CssBaseline";
import TextField from "@mui/material/TextField";
import FormControlLabel from "@mui/material/FormControlLabel";
import Checkbox from "@mui/material/Checkbox";
import Link from "@mui/material/Link";
import Grid from "@mui/material/Grid";
import Box from "@mui/material/Box";
import LockOutlinedIcon from "@mui/icons-material/LockOutlined";
import Typography from "@mui/material/Typography";
import Container from "@mui/material/Container";
import { createTheme, ThemeProvider } from "@mui/material/styles";
import HowToRegTwoToneIcon from "@mui/icons-material/HowToRegTwoTone";
import dayjs from "dayjs";
import { LocalizationProvider } from "@mui/x-date-pickers/LocalizationProvider";
import { AdapterDayjs } from "@mui/x-date-pickers/AdapterDayjs";
import { DatePicker } from "@mui/x-date-pickers/DatePicker";
import { DemoContainer } from "@mui/x-date-pickers/internals/demo";
import InputLabel from "@mui/material/InputLabel";
import MenuItem from "@mui/material/MenuItem";
import FormControl from "@mui/material/FormControl";
import Select from "@mui/material/Select";
import { register } from "../../api/user/register";
import { useNavigate } from "react-router-dom";
import Resizer from "react-image-file-resizer";

const RegisterBasicPage = () => {
  const navigate = useNavigate();
  const [username, setUsername] = useState("");
  const [email, setEmail] = useState("");
  const [password, setPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [name, setName] = useState("");
  const [lastname, setLastname] = useState("");
  const [birthday, setBirthday] = useState("");
  const [address, setAddress] = useState("");
  const [userType, setUserType] = useState("");
  const [picture, setPicture] = useState("");

  const handleSubmit = async (e) => {
    e.preventDefault();
    const input = {
      username: username,
      email: email,
      password: password,
      confirmPassword: confirmPassword,
      name: name,
      lastname: lastname,
      birthday: birthday,
      address: address,
      userType: userType,
      picture: picture,
    };
    const response = await register(input);
    if (response.status !== 200) {
      return navigate("/");
    }
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
          <Box
            component="form"
            noValidate
            sx={{ mt: 1 }}
            onSubmit={handleSubmit}
          >
            <TextField
              margin="normal"
              required
              fullWidth
              label="Username"
              onChange={(e) => setUsername(e.target.value)}
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              label="Email"
              onChange={(e) => setEmail(e.target.value)}
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              label="Password"
              onChange={(e) => setPassword(e.target.value)}
              type="password"
            />
            <TextField
              margin="normal"
              required
              fullWidth
              label="Confirm Password"
              onChange={(e) => setConfirmPassword(e.target.value)}
              type="password"
            />
            <TextField
              margin="normal"
              required
              fullWidth
              label="Name"
              onChange={(e) => setName(e.target.value)}
              autoFocus
            />
            <TextField
              margin="normal"
              required
              fullWidth
              label="Lastname"
              onChange={(e) => setLastname(e.target.value)}
              autoFocus
            />

            <LocalizationProvider dateAdapter={AdapterDayjs}>
              <DemoContainer components={["DatePicker", "DatePicker"]}>
                <DatePicker
                  label="Birthday"
                  value={birthday}
                  onChange={(birthday) => setBirthday(birthday)}
                  fullWidth
                  defaultValue={dayjs("2022-04-17")}
                />
              </DemoContainer>
            </LocalizationProvider>
            <TextField
              margin="normal"
              required
              fullWidth
              label="Address"
              onChange={(e) => setAddress(e.target.value)}
              autoFocus
            />
            <Box sx={{ minWidth: 120 }}>
              <FormControl fullWidth>
                <InputLabel id="demo-simple-select-label">User Type</InputLabel>
                <Select
                  labelId="demo-simple-select-label"
                  id="demo-simple-select"
                  value={userType}
                  label="User Type"
                  onChange={(e) => setUserType(e.target.value)}
                >
                  <MenuItem value={"SELLER"}>SELLER</MenuItem>
                  <MenuItem value={"BUYER"}>BUYER</MenuItem>
                </Select>
              </FormControl>
            </Box>
            <input type="file" accept="image/*" onChange={handleImageChange} />
            <Button
              type="submit"
              fullWidth
              variant="contained"
              sx={{ mt: 3, mb: 2 }}
            >
              Register
            </Button>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
};

export default RegisterBasicPage;
