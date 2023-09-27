import { FlightResponse, FlightResponseMainResponse } from "../../nswag";

export const dateFormat = (time: Date | undefined) => {
  if (time) {
    let isoString = new Date(time).toISOString();
    let options: any = {
      hour: "numeric",
      minute: "numeric",
    };
    let date = new Date(isoString);
     
    return new Intl.DateTimeFormat("en", options).format(date);
  }
  return time;
};

export const sortPriceMapper = (results: FlightResponseMainResponse) =>
  results.response?.sort((a: FlightResponse, b: FlightResponse) => {
    if (a?.price && b?.price) {
      return a.price - b.price;
    }
    return 0;
  });

export const flightMapper = (results: FlightResponseMainResponse) => {
  return sortPriceMapper(results);
};
