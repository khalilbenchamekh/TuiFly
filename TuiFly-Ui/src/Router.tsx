import React from "react";
import { BrowserRouter, Routes, Route, Outlet } from "react-router-dom";
import Flight from "./pages/Flight";
const Router=()=>{
    return(

        <BrowserRouter>
        <Routes>
          <Route path="/" element={<div> <Outlet /></div>}>
            <Route index element={<Flight />} />
          </Route>
        </Routes>
      </BrowserRouter>
    )
}

export default Router
