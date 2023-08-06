import * as React from "react";
import AppBar from "@mui/material/AppBar";
import Box from "@mui/material/Box";
import Toolbar from "@mui/material/Toolbar";
import IconButton from "@mui/material/IconButton";
import Typography from "@mui/material/Typography";
import Menu from "@mui/material/Menu";
import MenuIcon from "@mui/icons-material/Menu";
import Container from "@mui/material/Container";
import Avatar from "@mui/material/Avatar";
import Button from "@mui/material/Button";
import Tooltip from "@mui/material/Tooltip";
import MenuItem from "@mui/material/MenuItem";
import AdbIcon from "@mui/icons-material/Adb";
import { AuthContext } from "../../auth/authProvider";
import { useContext } from "react";
import { Navigate, useNavigate } from "react-router-dom";

const pagesAdmin = ["Pending", "Orders", "All Users"];
const pagesSeller = ["Articles", "New Orders", "My Orders", "Status"];
const pagesBuyer = ["New Order", "Previous Orders"];
const settings = ["Profile", "Edit Profile", "Logout"];

const Navbar = () => {
  const [anchorElNav, setAnchorElNav] = React.useState(null);
  const [anchorElUser, setAnchorElUser] = React.useState(null);
  const { handleLogout, user, isApproved } = useContext(AuthContext);
  const [isNavigationDisabled, setIsNavigationDisabled] = React.useState(false);
  const navigate = useNavigate();

  const handleOpenNavMenu = (event) => {
    setAnchorElNav(event.currentTarget);
  };
  const handleOpenUserMenu = (event) => {
    setAnchorElUser(event.currentTarget);
  };

  const handleCloseNavMenu = () => {
    setAnchorElNav(null);
  };

  const handleCloseUserMenu = () => {
    setAnchorElUser(null);
  };
  console.log(localStorage.getItem("role"));

  const handleOptionNavMenu = (page) => {
    if (page === "Pending") {
      setAnchorElNav(null);
      return navigate("/home/pending");
    } else if (page === "Orders") {
      setAnchorElNav(null);
      console.log("orders");
    } else if (page === "All Users") {
      console.log("all users");
      return navigate("/home/usersAdmin");
    } else if (page === "New Order") {
      console.log("new order");
      return navigate("/home/newOrder");
    } else if (page === "Previous Orders") {
      console.log("previous orders");
    } else if (page === "Articles") {
      console.log("articles");
      return navigate("/home/articles");
    } else if (page === "New Orders") {
      console.log("new orders");
    } else if (page === "My Orders") {
      console.log("my orders");
    } else if (page === "Status") {
      return navigate("/home/status");
    }
  };

  const handleClickProfile = () => {
    return navigate("/home/profile");
  };

  const handleClickLogout = () => {
    handleLogout();
  };

  return (
    <AppBar position="static">
      <Container maxWidth="xl">
        <Toolbar disableGutters>
          <AdbIcon sx={{ display: { xs: "none", md: "flex" }, mr: 1 }} />
          <Typography
            variant="h6"
            noWrap
            component="a"
            href="/"
            sx={{
              mr: 2,
              display: { xs: "none", md: "flex" },
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".3rem",
              color: "inherit",
              textDecoration: "none",
            }}
          >
            LOGO
          </Typography>

          <Box sx={{ flexGrow: 1, display: { xs: "flex", md: "none" } }}>
            <IconButton
              size="large"
              aria-label="account of current user"
              aria-controls="menu-appbar"
              aria-haspopup="true"
              onClick={handleOpenNavMenu}
              color="inherit"
            >
              <MenuIcon />
            </IconButton>
            <Menu
              id="menu-appbar"
              anchorEl={anchorElNav}
              anchorOrigin={{
                vertical: "bottom",
                horizontal: "left",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "left",
              }}
              open={Boolean(anchorElNav)}
              onClose={handleCloseNavMenu}
              sx={{
                display: { xs: "block", md: "none" },
              }}
            >
              {localStorage.getItem("role") === "admin" &&
                pagesAdmin.map((page) => (
                  <MenuItem
                    key={page}
                    onClick={() => handleOptionNavMenu(page)}
                  >
                    <Typography textAlign="center">{page}</Typography>
                  </MenuItem>
                ))}
              {localStorage.getItem("role") === "seller" &&
                pagesSeller.map((page) => (
                  <MenuItem
                    key={page}
                    onClick={() => handleOptionNavMenu(page)}
                  >
                    <Typography textAlign="center">{page}</Typography>
                  </MenuItem>
                ))}
              {localStorage.getItem("role") === "buyer" &&
                pagesBuyer.map((page) => (
                  <MenuItem
                    key={page}
                    onClick={() => handleOptionNavMenu(page)}
                  >
                    <Typography textAlign="center">{page}</Typography>
                  </MenuItem>
                ))}
            </Menu>
          </Box>
          <AdbIcon sx={{ display: { xs: "flex", md: "none" }, mr: 1 }} />
          <Typography
            variant="h5"
            noWrap
            component="a"
            href=""
            sx={{
              mr: 2,
              display: { xs: "flex", md: "none" },
              flexGrow: 1,
              fontFamily: "monospace",
              fontWeight: 700,
              letterSpacing: ".3rem",
              color: "inherit",
              textDecoration: "none",
            }}
          >
            LOGO
          </Typography>
          <Box sx={{ flexGrow: 1, display: { xs: "none", md: "flex" } }}>
            {localStorage.getItem("role") === "admin" &&
              pagesAdmin.map((page) => (
                <MenuItem key={page} onClick={() => handleOptionNavMenu(page)}>
                  <Typography textAlign="center">{page}</Typography>
                </MenuItem>
              ))}
            {localStorage.getItem("role") === "buyer" &&
              pagesBuyer.map((page) => (
                <MenuItem key={page} onClick={() => handleOptionNavMenu(page)}>
                  <Typography textAlign="center">{page}</Typography>
                </MenuItem>
              ))}
            {localStorage.getItem("role") === "seller" && (
              <React.Fragment>
                <MenuItem
                  onClick={() => handleOptionNavMenu("Articles")}
                  disabled={!isApproved}
                >
                  <Typography textAlign="center">Articles</Typography>
                </MenuItem>
                <MenuItem
                  onClick={() => handleOptionNavMenu("New Orders")}
                  disabled={!isApproved}
                >
                  <Typography textAlign="center">New Orders</Typography>
                </MenuItem>
                <MenuItem
                  onClick={() => handleOptionNavMenu("My Orders")}
                  disabled={!isApproved}
                >
                  <Typography textAlign="center">My Orders</Typography>
                </MenuItem>
                <MenuItem onClick={() => handleOptionNavMenu("Status")}>
                  <Typography textAlign="center">Status</Typography>
                </MenuItem>
              </React.Fragment>
            )}
          </Box>

          <Box sx={{ flexGrow: 0 }}>
            <Tooltip title="Open settings">
              <IconButton onClick={handleOpenUserMenu} sx={{ p: 0 }}>
                <Avatar alt="Profile picture" src={user.picture} />
              </IconButton>
            </Tooltip>
            <Menu
              sx={{ mt: "45px" }}
              id="menu-appbar"
              anchorEl={anchorElUser}
              anchorOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              keepMounted
              transformOrigin={{
                vertical: "top",
                horizontal: "right",
              }}
              open={Boolean(anchorElUser)}
              onClose={handleCloseUserMenu}
            >
              <MenuItem onClick={handleClickProfile}>
                <Typography textAlign="center">Profile</Typography>
              </MenuItem>
              <MenuItem onClick={handleClickLogout}>
                <Typography textAlign="center">Logout</Typography>
              </MenuItem>
            </Menu>
          </Box>
        </Toolbar>
      </Container>
    </AppBar>
  );
};

export default Navbar;
