import React from "react";
import { render } from "@testing-library/react";
import { IMyContext, ISnackbar, MyContext } from "../../context";
import { FlightRequest, FlightResponse, PassengerRequest } from "../../nswag";
import { I18nextProvider } from "react-i18next";
import ResponsiveDialog from ".";
import i18n from "../../i18n";

describe("ResponsiveDialog", () => {
  it("should render the dialog ", () => {
    const passengers = 2;
    const mockContextValue: IMyContext = {
      pageNumber: 1,
      flightList: [
        { page: 1, list: [{ departureCity: "Departure Cit test" }] },
      ],
      flightReserve: {
        flightNumber: "string",
        departureCity: "string",
        arrivalCity: "string",
        departureDate: new Date(),
        arrivalDate: new Date(),
        price: 1524,
      },
      pageSize: 10,
      searchFlight: { nbPassenger: passengers },
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
          <ResponsiveDialog />
        </I18nextProvider>
      </MyContext.Provider>
    );
    const titles = container.getElementsByTagName("h3");
    expect(titles.length).toEqual(0);
  });
});
