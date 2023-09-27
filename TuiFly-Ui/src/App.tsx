import React, { useContext, useEffect, useReducer } from "react";
import "./App.css";
import { Env, initialisationEnv } from "./config/environment";
import Router from "./Router";
import { getCities } from "./api/citiesApi";
import { MyContext } from "./context";
import { ICities } from "./nswag/types";

const App: React.FC = () => {
  const [items, dispatch] = useReducer(() => false, true);
  const { handleChangeCities, cities } = useContext(MyContext);
  
  async function isLoaded() {
    
    if (!cities) {
      const cities: Array<ICities> = await getCities();
      handleChangeCities(cities);
    }

    await initialisationEnv();
    dispatch();
  }
  useEffect(() => {
    if (!Env.apiEnv) {
      isLoaded();
    }
  }, [items]);

  return <Router />;
};

export default App;
