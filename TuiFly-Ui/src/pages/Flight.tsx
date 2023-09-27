import React, { useContext } from "react";
import FlightComponent from "../component/Flight";
import Navbar from "../component/NavBar";
import ListFlights from "../component/ListFlights";
import ResponsiveDialog from "../component/Dialogue";
import { Snackbar } from "@mui/material";
import { MyContext } from "../context";

const Flight = () => {
  const { snackBar, handleSnackBar } = useContext(MyContext);
  const handleClose = () => {
    handleSnackBar({ isOpen: false, message: "" });
  };

  return (
    <>
      <Navbar />
      <FlightComponent />
      <ListFlights />
      <ResponsiveDialog />
      <Snackbar
        autoHideDuration={1000}
        anchorOrigin={{ vertical: "bottom", horizontal: "right" }}
        open={snackBar?.isOpen}
        onClose={handleClose}
        message={snackBar?.message}
      />
    </>
  );
};

export default Flight;
