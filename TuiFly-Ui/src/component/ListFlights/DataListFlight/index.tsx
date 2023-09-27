import React, { useContext } from "react";
import {
  Box,
  Button,
  ButtonBase,
  Grid,
  Paper,
  Typography,
} from "@mui/material";
import { FlightResponse } from "../../../nswag";
import { dateFormat } from "../../../mapper/flightMapper";
import { MyContext } from "../../../context";
import { useTranslation } from "react-i18next";

interface IDataListFlight {
  itemData?: FlightResponse[];
}

interface IDataList {
  data?: string;
  content?: string;
}

const DataList = ({ data, content }: IDataList) => {
  return (
    <Grid container spacing={1} sx={{ marginTop: 1 }}>
      <Grid
        sx={{ display: "flex", alignItems: "center", justifyContent: "start" }}
      >
        <Typography
          gutterBottom
          variant="subtitle1"
          component="div"
          sx={{ fontStyle: "italic" }}
        >
          {content}
        </Typography>
      </Grid>
      <Grid sx={{ marginLeft: 5 }}>
        <Typography
          gutterBottom
          variant="subtitle1"
          component="div"
          sx={{ display: "flex", alignItems: "center", justifyContent: "end" }}
        >
          {data}
        </Typography>
      </Grid>
    </Grid>
  );
};

const DataListFlight = ({ itemData }: IDataListFlight) => {
  const { t } = useTranslation();
  const { handleFlightReserve } = useContext(MyContext);
  const handleResarvation = (item: FlightResponse) => {
    handleFlightReserve(item);
  };

  return (
    <>
      {itemData?.map((item, index) => (
        <Box key={index}>
          <Grid
            sx={{
              margin: 2,
              display: "flex",
              alignItems: "center",
              justifyContent: "center",
            }}
          >
            <Paper
              sx={{
                p: 2,
                maxWidth: 500,
                flexGrow: 1,
                backgroundColor: (theme) =>
                  theme.palette.mode === "dark" ? "#1A2027" : "#fff",
              }}
            >
              <Grid container spacing={2}>
                <Grid item xs={4}>
                  <ButtonBase sx={{ width: 128, height: 128 }}>
                    <h3>TUIFLY</h3>
                  </ButtonBase>
                </Grid>
                <Grid item xs={8} container direction="column">
                  <DataList
                    data={item.departureCity}
                    content={t("reserve.from")}
                  />
                  <DataList data={item.arrivalCity} content={t("reserve.to")} />
                  <DataList
                    data={dateFormat(item.departureDate)}
                    content={t("reserve.timetDep")}
                  />
                  <DataList
                    data={dateFormat(item.arrivalDate)}
                    content={t("reserve.timetRet")}
                  />
                  <DataList
                    data={item.price?.toString()}
                    content={t("reserve.price")}
                  />
                </Grid>
              </Grid>
              <Box
                sx={{
                  display: "flex",
                  alignContent: "center",
                  justifyContent: "end",
                }}
              >
                <Button
                  className="reservation"
                  onClick={() => handleResarvation(item)}
                >
                  {t("reserve.booking")}
                </Button>
              </Box>
            </Paper>
          </Grid>
        </Box>
      ))}
    </>
  );
};

export default DataListFlight;
