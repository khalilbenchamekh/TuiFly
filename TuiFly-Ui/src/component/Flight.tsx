import {
  Autocomplete,
  FilterOptionsState,
  Button,
  Grid,
  TextField,
  Card,
  CardContent,
  Box,
  Select,
  MenuItem,
  Skeleton,
} from "@mui/material";
import React, { useContext, useEffect, useState } from "react";
import { MyContext } from "../context";
import { ICities, IErrorMsg, IFlightObject } from "../nswag/types";
import { getCurrentDate, validationForm } from "../utile";
import { FlightRequest, FlightResponseMainResponse } from "../nswag";
import { flightFindAll } from "../api/flightApi";
import { flightMapper } from "../mapper/flightMapper";
import { useTranslation } from "react-i18next";

const FlightComponent = () => {
  const [flights, setFlights] = useState<FlightRequest | undefined>({});
  const [datas, setdatas] = useState<ICities[] | undefined>(undefined);
  const [numberPassenger, setNumberPassenger] = useState<number>(1);
  const {t} = useTranslation()
  const {
    cities,
    handleFlightList,
    pageNumber,
    pageSize,
    searchFlight,
    handleLoading,
    handleSearchFlights,
  } = useContext(MyContext);
  const [errorMsg, setErrorMsg] = useState<IErrorMsg | undefined>();

  useEffect(() => {
    setdatas(cities);
  }, [cities, flights, errorMsg, searchFlight]);
  const filterOptions = (
    data: ICities[],
    { inputValue }: FilterOptionsState<ICities>
  ) => {
    return data.filter((option) =>
      option.value.toLowerCase().startsWith(inputValue.toLowerCase())
    );
  };

  const handleSubmit = async (event: any) => {
    event.preventDefault();
    const obj: FlightRequest = {
      ...flights,
      pageNumber: pageNumber,
      pageSize: pageSize,
      customersCount:numberPassenger
    };
    setErrorMsg({ city: false, time: false });
    const validation = validationForm(obj);
    if (!validation.isValid) {
      setErrorMsg(validation.msg);
    } else {
      try {
        handleLoading(true)
        const res: FlightResponseMainResponse = await flightFindAll(obj);
        const mapper = flightMapper(res);
        handleFlightList(mapper);
        const flightObji: IFlightObject = {
          ...obj,
          nbPassenger: numberPassenger,
        };
        handleSearchFlights(flightObji);
      } catch (error) {}
    }
    handleLoading(false)
  };
  const handleChangeDate = (e: React.ChangeEvent<HTMLInputElement>) => {
    const { name, value } = e.target;
    let dateValue = new Date(e.target.value).getTime();
    let currentData = new Date().getTime();
    if (currentData <= dateValue) {
      setFlights((prevPerson) => ({
        ...prevPerson,
        [name]: value,
      }));
    }
    if (currentData > dateValue) {
      setFlights((prevPerson) => ({
        ...prevPerson,
        [name]: getCurrentDate(),
      }));
    }
    if (
      flights?.departureDate &&
      flights?.arrivalDate &&
      flights?.departureDate > flights?.arrivalDate
    ) {
      setFlights((prevPerson) => ({
        ...prevPerson,
        arrivalDate: flights?.departureDate,
      }));
    }
  };
  const champErrorCity = t('searchFormular.errorMessage.City')

  const champErrorTime = t('searchFormular.errorMessage.time')
  return (
    <form onSubmit={handleSubmit}>
      {datas  ? (
        <Box sx={{ padding: 2 }}>
          <Card
            variant="outlined"
            sx={{ marginBottom: 2, border: "1px solid DodgerBlue" }}
          >
            <CardContent>
              <Grid container spacing={2}>
                <Grid item xs={2}>
                  <Autocomplete
                    sx={{ width: 150 }}
                    filterSelectedOptions
                    filterOptions={filterOptions}
                    options={datas}
                    onInputChange={(e, v) => {
                      setFlights((pre) => ({
                        ...pre,
                        departureCity: v,
                      }));
                    }}
                    renderInput={(params) => (
                      <TextField
                        {...params}
                        label={
                          t('searchFormular.from')
                        }
                        name="departureCity"
                        helperText={errorMsg?.city && champErrorCity}
                        error={errorMsg?.city}
                        fullWidth
                      />
                    )}
                  />
                </Grid>
                <Grid item xs={2}>
                  <Autocomplete
                    sx={{ width: 150 }}
                    filterSelectedOptions
                    filterOptions={filterOptions}
                    options={datas}
                    onInputChange={(e, v) => {
                      setFlights((pre) => ({
                        ...pre,
                        arrivalCity: v,
                      }));
                    }}
                    renderInput={(params) => (
                      <TextField
                        {...params}
                        label={
                          t('searchFormular.to')
                        }
                        helperText={errorMsg?.city && champErrorCity}
                        error={errorMsg?.city}
                        name="arrivalCity"
                        fullWidth
                      />
                    )}
                  />
                </Grid>
                <Grid item xs={2}>
                  <TextField
                    sx={{ width: 150 }}
                    label={
                      t('searchFormular.timetDep')
                    }
                    type="date"
                    value={flights?.departureDate}
                    error={errorMsg?.time}
                    name="departureDate"
                    InputLabelProps={{ shrink: true }}
                    onChange={handleChangeDate}
                    helperText={errorMsg?.time && champErrorTime}
                  />
                </Grid>
                <Grid item xs={2}>
                  <TextField
                    sx={{ width: 150 }}
                    label={
                      t('searchFormular.timetRet')
                    }
                    type="date"
                    value={flights?.arrivalDate}
                    error={errorMsg?.time}
                    name="arrivalDate"
                    InputLabelProps={{ shrink: true }}
                    onChange={handleChangeDate}
                    helperText={errorMsg?.time && champErrorTime}
                  />
                </Grid>
                <Grid item xs={2}>
                  <Select
                    value={numberPassenger}
                    onChange={(e) => {
                      setNumberPassenger(Number(e.target.value));
                    }}
                    displayEmpty
                    inputProps={{ "aria-label": "Passenger Count" }}
                  >
                    {[...Array(9)].map((x, i) => (
                      <MenuItem value={i + 1} key={i}>
                        {i + 1} {t('passenger.name')}{i+1>1 && "s" }
                      </MenuItem>
                    ))}
                  </Select>
                </Grid>
                <Grid
                  item
                  xs={1}
                  sx={{
                    display: "flex",
                    alignItems: "center",
                    justifyContent: "center",
                  }}
                >
                  <Button title="submit" variant="contained" type="submit">
                 {t('search')}
                  </Button>
                </Grid>
              </Grid>
            </CardContent>
          </Card>
        </Box>
      ) : (
        <Box sx={{ padding: 2, height: 150 }}>
          <Skeleton variant="rectangular" width="100%">
            <Card
              variant="outlined"
              sx={{ marginBottom: 2, border: "1px solid DodgerBlue" }}
            >
              <CardContent></CardContent>
            </Card>
          </Skeleton>
        </Box>
      )}
    </form>
  );
};

export default FlightComponent;
