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
import { AuthContext } from "../../auth/authProvider";
import { updateProfile } from "../../api/update";
import Resizer from "react-image-file-resizer";

const RegisterBasicPage = () => {
  const navigate = useNavigate();
  const { user, handleUserInfo, isGoogleLogged } = useContext(AuthContext);
  const [username, setUsername] = useState(user.username);
  const [email, setEmail] = useState(user.email);
  const [oldPassword, setOldPassword] = useState("");
  const [changedPassword, setChangedPassword] = useState("");
  const [confirmPassword, setConfirmPassword] = useState("");
  const [name, setName] = useState(user.name);
  const [lastname, setLastname] = useState(user.lastname);
  const [birthday, setBirthday] = useState(user.birthday);
  const [address, setAddress] = useState(user.address);
  const [userType, setUserType] = useState(user.userType);
  const [picture, setPicture] = useState(user.picture);

  const handleSubmit = async (e) => {
    e.preventDefault();
    const input = {
      id: user.id,
      username: username,
      email: email,
      oldPassword: oldPassword,
      changedPassword: changedPassword,
      confirmPassword: confirmPassword,
      name: name,
      lastname: lastname,
      birthday: birthday,
      address: address,
      userType: userType,
      picture: picture,
    };
    const response = await updateProfile(input);
    if (response.status !== 200) {
      console.log("greska u update");
      return navigate("/");
    }
    console.log(response);
    console.log(response.data);
    handleUserInfo(response.data);
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
            Profile
          </Typography>
          <Box
            component="form"
            noValidate
            sx={{ mt: 1 }}
            onSubmit={handleSubmit}
          >
            {!isGoogleLogged && (
              <TextField
                margin="normal"
                required
                fullWidth
                label="Username"
                defaultValue={user.username}
                onChange={(e) => setUsername(e.target.value)}
                autoFocus
              />
            )}
            {isGoogleLogged ? (
              <TextField
                margin="normal"
                required
                fullWidth
                label="Email"
                defaultValue={user.email}
                onChange={(e) => setEmail(e.target.value)}
                InputProps={{
                  readOnly: true,
                }}
                autoFocus
              />
            ) : (
              <TextField
                margin="normal"
                required
                fullWidth
                label="Email"
                defaultValue={user.email}
                onChange={(e) => setEmail(e.target.value)}
                autoFocus
              />
            )}
            {!isGoogleLogged && (
              <React.Fragment>
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  label="Old Password"
                  onChange={(e) => setOldPassword(e.target.value)}
                  type="password"
                />
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  label="Change Password"
                  onChange={(e) => setChangedPassword(e.target.value)}
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
              </React.Fragment>
            )}
            {isGoogleLogged ? (
              <React.Fragment>
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  label="Name"
                  defaultValue={user.name}
                  onChange={(e) => setName(e.target.value)}
                  InputProps={{
                    readOnly: true,
                  }}
                  autoFocus
                />
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  label="Lastname"
                  defaultValue={user.lastname}
                  onChange={(e) => setLastname(e.target.value)}
                  InputProps={{
                    readOnly: true,
                  }}
                  autoFocus
                />
              </React.Fragment>
            ) : (
              <React.Fragment>
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  label="Name"
                  defaultValue={user.name}
                  onChange={(e) => setName(e.target.value)}
                  autoFocus
                />
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  label="Lastname"
                  defaultValue={user.lastname}
                  onChange={(e) => setLastname(e.target.value)}
                  autoFocus
                />
              </React.Fragment>
            )}
            {!isGoogleLogged && (
              <React.Fragment>
                <LocalizationProvider dateAdapter={AdapterDayjs}>
                  <DemoContainer components={["DatePicker", "DatePicker"]}>
                    <DatePicker
                      label="Birthday"
                      value={dayjs(birthday)}
                      onChange={(birthday) => setBirthday(birthday)}
                      fullWidth
                      defaultValue={dayjs(user.birthday)}
                    />
                  </DemoContainer>
                </LocalizationProvider>
                <TextField
                  margin="normal"
                  required
                  fullWidth
                  label="Address"
                  defaultValue={user.address}
                  onChange={(e) => setAddress(e.target.value)}
                  autoFocus
                />
              </React.Fragment>
            )}
            <Box sx={{ minWidth: 120 }}>
              <FormControl fullWidth disabled>
                <InputLabel id="demo-simple-select-label">User Type</InputLabel>
                <Select
                  labelId="demo-simple-select-label"
                  id="demo-simple-select"
                  value={user.userType}
                  label="User Type"
                >
                  <MenuItem value={user.userType}>
                    {user.userType === 0 && "ADMIN"}
                    {user.userType === 1 && "SELLER"}
                    {user.userType === 2 && "BUYER"}
                  </MenuItem>
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
              Update
            </Button>
          </Box>
        </Box>
      </Container>
    </ThemeProvider>
  );
};

export default RegisterBasicPage;
