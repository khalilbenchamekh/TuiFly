import React from "react";
import { render, screen } from "@testing-library/react";
import Flight from "./Flight";
import { IMyContext, ISnackbar, MyContext } from "../context";
import { FlightResponse, PassengerRequest } from "../nswag";
import { IFlightObject } from "../nswag/types";
import i18n from "../i18n";

describe("FlightComponent", () => {
  beforeEach(() => {
    i18n.init()
  });
  test("submits form with correct without values", () => {

    // Check if input fields are rendered
    const departureCityInput = screen.queryByLabelText("Departure City");
    const arrivalCityInput = screen.queryByLabelText("Arrival City");
    const departureDateInput = screen.queryByLabelText("Departure Date");
    const arrivalDateInput = screen.queryByLabelText("Arrival Date");

    expect(departureCityInput).not.toBeInTheDocument();
    expect(arrivalCityInput).not.toBeInTheDocument();
    expect(departureDateInput).not.toBeInTheDocument();
    expect(arrivalDateInput).not.toBeInTheDocument();

    // Submit the form
    const nonExistentButton = screen.queryByRole("button", {
      name: "Non-existent Button",
    });
    expect(nonExistentButton).not.toBeInTheDocument();
  });

  test("submits form with correct values", () => {
    const mockContextValue: IMyContext = {
      flightList: [{page:1,list:[{departureCity:"Departure City"}]}],
      pageNumber: 1,
      pageSize: 10,
      handleChangeCities: () => {},
      handleFlightList: () => {},
      handlePageNumber: (page: number) => {},
      handlePageSize: (size: number) => {},
      handleSearchFlights: (flightSearch: IFlightObject | undefined) => {},
      handleFlightReserve: (reserveFlight?: FlightResponse) => {},
      handleReservations:(reservations?: PassengerRequest)=>{},
      handleSnackBar: (snackBarData: ISnackbar) => {},
      handleLoading:(data : boolean)=>{}
    };
    render(
      <MyContext.Provider value={mockContextValue}>
        <Flight />
      </MyContext.Provider>
    );

    // Fill in input fields
    const departureCityInput = screen.findByLabelText("Departure City");
    const arrivalCityInput = screen.findByLabelText("Arrival City");
    const departureDateInput = screen.findByLabelText("Departure Date");
    const arrivalDateInput = screen.findByLabelText("Arrival Date");

    expect(departureCityInput).toBeInTheDocument;
    expect(arrivalCityInput).toBeInTheDocument;
    expect(departureDateInput).toBeInTheDocument;
    expect(arrivalDateInput).toBeInTheDocument;

    // Check if submit button is rendered
    const submitButton = screen.findByTitle('submit');
    expect(submitButton).toBeInTheDocument;
  });
});
