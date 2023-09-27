import React from "react";
import { render } from "@testing-library/react";
import ListFlights, { checkSize } from ".";
import { IMyContext, ISnackbar, MyContext } from "../../context";
import { FlightRequest, FlightResponse, PassengerRequest } from "../../nswag";
import { I18nextProvider } from "react-i18next";
import i18n from "../../i18n";

describe("FlightComponent", () => {
  beforeEach(() => {
    i18n.init()
  });
  test("renered ListFlights with correct values", () => {
    const mockContextValue: IMyContext = {
      pageNumber: 1,
      flightList: [
        { page: 1, list: [{ departureCity: "Departure Cit test" }] },
      ],
      pageSize: 10,
      handleChangeCities: () => {},
      handleFlightList: () => {},
      handlePageNumber: (page: number) => {},
      handlePageSize: (size: number) => {},
      handleSearchFlights: (flightSearch: FlightRequest | undefined) => {},
      handleFlightReserve: (reserveFlight?: FlightResponse) => {},
      handleReservations:(reservations?: PassengerRequest)=>{},
      handleSnackBar: (snackBarData: ISnackbar) => {},
      handleLoading:(data : boolean)=>{}
    };
    const { container } = render(
      <MyContext.Provider value={mockContextValue}>
        <I18nextProvider i18n={i18n}>
           <ListFlights />
        </I18nextProvider>
      </MyContext.Provider>
    );

    const content = container.getElementsByClassName("MuiTypography-root");

    expect(content.length).toEqual(10);
    expect(content[0]).toHaveTextContent("City of departure");
    expect(content[1]).toHaveTextContent("Departure Cit test");
    const button = container.getElementsByTagName("button");

    expect(button[0]).toHaveAccessibleName("TUIFLY");
    const reserve=container.getElementsByClassName('reservation')
    expect(reserve[0]).toHaveAccessibleName("Reserve");
  });

  test("renered ListFlights without correct values", () => {
    const mockContextValue: IMyContext = {
      pageNumber: 1,
      flightList: [],
      pageSize: 10,
      handleChangeCities: () => {},
      handleFlightList: () => {},
      handlePageNumber: (page: number) => {},
      handlePageSize: (size: number) => {},
      handleSearchFlights: (flightSearch: FlightRequest | undefined) => {},
      handleFlightReserve: (reserveFlight?: FlightResponse) => {},
      handleReservations:(reservations?: PassengerRequest)=>{},
      handleSnackBar: (snackBarData: ISnackbar) => {},
      handleLoading:(data : boolean)=>{}
    };
    const { container } = render(
      <MyContext.Provider value={mockContextValue}>
        <I18nextProvider i18n={i18n}>
           <ListFlights />
        </I18nextProvider>
      </MyContext.Provider>
    );

    const departureCityInput = container.getElementsByClassName("MuiTypography-root");
    expect(departureCityInput.length).not.toEqual(9);
    expect(departureCityInput[1]).toBeUndefined();
    expect(departureCityInput[2]).toBeUndefined();
    expect(departureCityInput[3]).toBeUndefined();
    expect(departureCityInput[4]).toBeUndefined();
    expect(departureCityInput[5]).toBeUndefined();
    expect(departureCityInput[6]).toBeUndefined();

    const button = container.getElementsByTagName("button");

    expect(button[0]).toBeUndefined();
  });
});

describe("testing checkSize function",()=>{
  test('test in case lenght bigger then pageSize',()=>{
    const list :FlightResponse[] | undefined = [{id:1},{id:2}]
    const page = 5
    const size = 2
    const  render = checkSize(list,page,size)
    expect(render).toEqual(2)
  })
  test('test in case lenght not bigger then pageSize',()=>{
    const list :FlightResponse[] | undefined = [{id:1}]
    const page = 5
    const size = 2
    const  render = checkSize(list,page,size)
    expect(render).toEqual(page)
  })
});


