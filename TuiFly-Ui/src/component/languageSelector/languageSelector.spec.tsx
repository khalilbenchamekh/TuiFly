import React from "react";
import { render } from "@testing-library/react";
import { IMyContext, ISnackbar, MyContext } from "../../context";
import { FlightRequest, FlightResponse, PassengerRequest } from "../../nswag";
import LanguageSelector from ".";
import { I18nextProvider } from "react-i18next";
import i18n from "../../i18n";

describe("LanguageSelector", () => {
  test("renered LanguageSelector English", () => {
    const mockContextValue: IMyContext = {
      pageNumber: 1,
      flightList: [
        { page: 1, list: [{ departureCity: "Departure Cit test " }] },
      ],
      pageSize: 10,
      handleChangeCities: () => {},
      handleFlightList: () => {},
      handlePageNumber: (page: number) => {},
      handlePageSize: (size: number) => {},
      handleSearchFlights: (flightSearch: FlightRequest | undefined) => {},
      handleFlightReserve: (reserveFlight?: FlightResponse) => {},
      handleReservations: (reservations?: PassengerRequest) => {},
      handleSnackBar: (snackBarData: ISnackbar) => {},
      handleLoading: (data: boolean) => {},
    };
    const { container } = render(
      <MyContext.Provider value={mockContextValue}>
        <I18nextProvider i18n={i18n}>
          <LanguageSelector />
        </I18nextProvider>
      </MyContext.Provider>
    );
    const langueButton = container.getElementsByClassName("MuiSelect-select");
    expect(langueButton.length).toEqual(1);
    expect(langueButton[0]).toHaveAccessibleName("US EN")
  });
  test("renered LanguageSelector French", () => {
    const mockContextValue: IMyContext = {
      pageNumber: 1,
      flightList: [
        { page: 1, list: [{ departureCity: "Departure Cit test " }] },
      ],
      pageSize: 10,
      handleChangeCities: () => {},
      handleFlightList: () => {},
      handlePageNumber: (page: number) => {},
      handlePageSize: (size: number) => {},
      handleSearchFlights: (flightSearch: FlightRequest | undefined) => {},
      handleFlightReserve: (reserveFlight?: FlightResponse) => {},
      handleReservations: (reservations?: PassengerRequest) => {},
      handleSnackBar: (snackBarData: ISnackbar) => {},
      handleLoading: (data: boolean) => {},
    };
    i18n.changeLanguage('fr')
    const { container } = render(
      <MyContext.Provider value={mockContextValue}>
        <I18nextProvider i18n={i18n}>
          <LanguageSelector />
        </I18nextProvider>
      </MyContext.Provider>
    );
    const langueButton = container.getElementsByClassName("MuiSelect-select");
    expect(langueButton.length).toEqual(1);
    expect(langueButton[0]).toHaveAccessibleName("FR FR")
  });
});
