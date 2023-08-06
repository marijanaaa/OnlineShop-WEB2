import Login from "./pages/login/Login"
import { BrowserRouter, Routes, Route, createBrowserRouter, Outlet } from "react-router-dom";
import AuthProvider from "./auth/authProvider";
import ProtectedRoute from "./auth/protectedRoute";
import RegisterBasic from "./pages/registration/RegisterBasic";
import Register from "./pages/registration/Register";
import Home from "./pages/home/Home"
import Pending from "./pages/pending/Pending"
import Profile from "./pages/profile/Profile"
import Status from "./pages/status/Status"
import Article from "./pages/articles/Article"
import UsersAdmin from "./pages/usersAdmin/UsersAdmin"
import NewOrder from "./pages/order/NewOrder"
import { useEffect } from 'react'
import { AuthContext } from "./auth/authProvider";
import { useContext } from "react";
import Navbar from "./components/navbar/Navbar";

function Layout() {
    return <>
        <div style={{ display: "flex", flexDirection: "column", minHeight: "100vh" }}>
            <Navbar />
            <div style={{ flexGrow: 2 }}>
                <Outlet />
            </div>
            <footer>
                &copy; 2023
            </footer>
        </div>    </>
}


function App() {
    return (
        <div>
            <BrowserRouter>
                <AuthProvider>

                    <Routes>
                        <Route path="/">
                            <Route index element={<Login />} />
                            <Route path="registerBasic" element={<RegisterBasic />} />
                            <Route path="register" element={<Register />} />
                            <Route path="home" element={<Layout />}>
                                <Route index element={
                                    <ProtectedRoute>
                                        <Home />
                                    </ProtectedRoute>
                                } />
                                <Route path="pending" element={<Pending />} />
                                <Route path="profile" element={
                                    <ProtectedRoute>
                                        <Profile />
                                    </ProtectedRoute>} />
                                <Route path="status" element={
                                    <ProtectedRoute>
                                        <Status />
                                    </ProtectedRoute>} />
                                <Route path="articles" element={
                                    <ProtectedRoute>
                                        <Article />
                                    </ProtectedRoute>} />
                                <Route path="usersAdmin" element={
                                    <ProtectedRoute>
                                        <UsersAdmin />
                                    </ProtectedRoute>} />
                                <Route path="newOrder" element={
                                    <ProtectedRoute>
                                        <NewOrder />
                                    </ProtectedRoute>} />
                            </Route>
                        </Route>
                    </Routes>
                </AuthProvider>
            </BrowserRouter>
        </div >
    );
}

export default App;