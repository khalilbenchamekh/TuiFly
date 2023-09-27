import React, { ReactNode, createContext, useState } from "react";
import { ICities, IFlightListPages, IFlightObject } from "../nswag/types";
import { FlightRequest, FlightResponse, PassengerRequest } from "../nswag";

// Define the shape of your context
export interface IMyContext {
  cities?: Array<ICities>;
  initialEnv?: boolean;
  flightList: Array<IFlightListPages>;
  pageNumber: number;
  pageSize: number;
  searchFlight?: IFlightObject;
  flightReserve?: FlightResponse;
  isLoading?: boolean;
  snackBar?:ISnackbar;
  reservations?: PassengerRequest[];
  handleInitiaEnv?: () => void;
  handleChangeCities: (newCities: Array<ICities>) => void;
  handleFlightList: (listFlight: Array<FlightResponse> | undefined) => void;
  handlePageNumber: (page: number) => void;
  handlePageSize: (size: number) => void;
  handleSearchFlights: (flightSearch: IFlightObject | undefined) => void;
  handleFlightReserve: (reserveFlight?: FlightResponse) => void;
  handleReservations: (reservations?: PassengerRequest) => void;
  handleSnackBar:(snackBarData : ISnackbar)=>void;
  handleLoading: (data: boolean) => void;
}

interface IMyContextProvider {
  children: ReactNode;
}

// Create the context
export const MyContext = createContext<IMyContext>({
  flightList: [],
  pageNumber: 1,
  pageSize: 10,
  handleChangeCities: () => {},
  handleFlightList: () => {},
  handlePageNumber: (page: number) => {},
  handlePageSize: (size: number) => {},
  handleSearchFlights: (flightSearch: IFlightObject | undefined) => {},
  handleFlightReserve: (reserveFlight?: FlightResponse) => {},
  handleReservations: (reservations?: PassengerRequest) => {},
  handleSnackBar:(snackBarData : ISnackbar)=>{},
  handleLoading: (data: boolean) => {},
});
export interface ISnackbar {
  isOpen: boolean;
  message?: string;
}
// Create a provider component
export const MyContextProvider = ({ children }: IMyContextProvider) => {
  const [initialEnv, isUpdated] = useState<boolean>(false);
  const [cities, setCities] = useState<Array<ICities>>();
  const [pageNumber, setPageNumber] = useState<number>(1);
  const [pageSize, setPageSize] = useState<number>(10);
  const [flightList, setFlights] = useState<Array<IFlightListPages>>([]);
  const [searchFlight, setSearchFlight] = useState<FlightRequest | undefined>();
  const [flightReserve, setFlightReserve] = useState<
    FlightResponse | undefined
  >();
  const [reservations, setReservations] = useState<PassengerRequest[]>([]);
  const [snackBar, setSnackBar] = useState<ISnackbar>({
    isOpen: false,
    message: "",
  });
  const [isLoading, setLoading] = useState(false);

  const handleLoading = (data: boolean) => {
    setLoading(data);
  };
  const handleSnackBar=(snackBarData : ISnackbar)=>{
    setSnackBar(snackBarData)
  }

  const handleReservations = (reservation?: PassengerRequest) => {
    if (reservation) {
      setReservations((pre) => [...pre, reservation]);
    }
  };

  const handleFlightReserve = (reserveFlight?: FlightResponse) => {
    setFlightReserve(reserveFlight);
  };

  const handleSearchFlights = (flightSearch: IFlightObject | undefined) => {
    setSearchFlight(flightSearch);
  };

  const handlePageSize = (size: number) => {
    setPageSize(size);
  };

  const handlePageNumber = (page: number) => {
    setPageNumber(page);
  };

  const handleFlightList = (listFlight: Array<FlightResponse> | undefined) => {
    const findFlightCheck = flightList?.find(
      (item) => item.page === pageNumber
    );
    if (!findFlightCheck && flightList) {
      setFlights([...flightList, { page: pageNumber, list: listFlight }]);
    }
  };

  const handleInitiaEnv = () => {
    isUpdated(true);
  };
  const handleChangeCities = (newCities: Array<ICities>) => {
    setCities(newCities);
  };

  // Provide the context value to the children components
  return (
    <MyContext.Provider
      value={{
        cities,
        initialEnv,
        flightList,
        pageNumber,
        pageSize,
        searchFlight,
        flightReserve,
        reservations,
        snackBar,
        isLoading,
        handleInitiaEnv,
        handleChangeCities,
        handleFlightList,
        handlePageNumber,
        handlePageSize,
        handleSearchFlights,
        handleFlightReserve,
        handleReservations,
        handleSnackBar,
        handleLoading,
      }}
    >
      {children}
    </MyContext.Provider>
  );
};
