import {
  canDisplay,
  getCurrentDate,
  isValidEmail,
  validatePassengers,
  validationForm,
} from ".";
import { FlightRequest, FlightResponse } from "../nswag";
import { IErrorForm, IErrorMsg, IFlightListPages } from "../nswag/types";

describe("isValidEmail", () => {
  test("should return true for a valid email address", () => {
    const validEmail = "test@example.com";
    const result = isValidEmail(validEmail);
    expect(result).toBe(true);
  });

  test("should return false for an invalid email address", () => {
    const invalidEmail = "test@example";
    const result = isValidEmail(invalidEmail);
    expect(result).toBe(false);
  });
});

describe("canDisplay", () => {
  test("should return false when the page is not found in the list", () => {
    const page = 2;
    const list: IFlightListPages[] = [
      { page: 1, list: [] },
      { page: 3, list: [] },
    ];
    const result = canDisplay(page, list);
    expect(result).toBe(false);
  });

  test("should return false when the list is not an array", () => {
    const page = 1;
    const list: IFlightListPages[] = [
      { page: 1, list: {} as FlightResponse[] },
    ];
    const result = canDisplay(page, list);
    expect(result).toBe(false);
  });

  test("should return false when the list is empty", () => {
    const page = 1;
    const list: IFlightListPages[] = [{ page: 1, list: [] }];
    const result = canDisplay(page, list);
    expect(result).toBe(false);
  });

  test("should return true when the list has items", () => {
    const page = 1;
    const list: IFlightListPages[] = [
      { page: 1, list: [{ id: 1, airline: "Airline 1" }] },
      { page: 2, list: [] },
    ];
    const result = canDisplay(page, list);
    expect(result).toBe(true);
  });
});

describe("validatePassengers", () => {
  test("should return an empty array for valid passengers", () => {
    const passengers = [
      { firstName: "John", lastName: "Doe", email: "john.doe@example.com" },
      { firstName: "Jane", lastName: "Smith", email: "jane.smith@example.com" },
    ];
    const champsErrorFirstName = "First name is required";
    const champsErrorLastName = "Last name is required";
    const champsErrorEmail = "Email is required";
    const result = validatePassengers(
      passengers,
      champsErrorFirstName,
      champsErrorLastName,
      champsErrorEmail
    );
    expect(result).toEqual([]);
  });

  test("should return an array of validation errors for invalid passengers", () => {
    const passengers = [
      {
        firstName: "",
        lastName: "Doe",
        email: "john.doe@example.com",
        index: 0,
      },
      {
        firstName: "Jane",
        lastName: "",
        email: "jane.smith@example.com",
        index: 1,
      },
      { firstName: "John", lastName: "Doe", email: "invalid-email", index: 2 },
    ];
    const champsErrorFirstName = "First name is required";
    const champsErrorLastName = "Last name is required";
    const champsErrorEmail = "Email is required";
    const result = validatePassengers(
      passengers,
      champsErrorFirstName,
      champsErrorLastName,
      champsErrorEmail
    );
    expect(result).toEqual([
      { firstName: "First name is required", index: 0 },
      { lastName: "Last name is required", index: 1 },
      { email: "Invalid email address", index: 2 },
    ]);
  });
});
describe("getCurrentDate", () => {
  test('should return the current date in the format "YYYY-MM-DD"', () => {
    const currentDate = new Date();
    const year = currentDate.getFullYear();
    const month = String(currentDate.getMonth() + 1).padStart(2, "0");
    const day = String(currentDate.getDate()).padStart(2, "0");
    const expectedDate = `${year}-${month}-${day}`;

    const result = getCurrentDate();

    expect(result).toBe(expectedDate);
  });
});

describe("validationForm", () => {
  test("should return valid error form when departure city and arrival city are different", () => {
    const flight: FlightRequest = {
      departureCity: "City A",
      arrivalCity: "City B",
    };

    const result = validationForm(flight);

    expect(result.isValid).toBe(false);
    expect(result.msg.city).toBe(false);
    expect(result.msg.time).toBe(true);
  });

  test("should return invalid error form when departure city and arrival city are the same", () => {
    const flight: FlightRequest = {
      departureCity: "City A",
      arrivalCity: "City A",
    };

    const result = validationForm(flight);

    expect(result.isValid).toBe(false);
    expect(result.msg.city).toBe(true);
    expect(result.msg.time).toBe(true);
  });

  test("should return valid error form when departure date is before arrival date", () => {
    const flight: FlightRequest = {
      departureDate: new Date("2023-09-01"),
      arrivalDate: new Date("2023-09-02"),
    };

    const result = validationForm(flight);

    expect(result.isValid).toBe(false);
    expect(result.msg.city).toBe(true);
    expect(result.msg.time).toBe(false);
  });

  test("should return invalid error form when departure date is after arrival date", () => {
    const flight: FlightRequest = {
      departureDate: new Date("2023-09-02"),
      arrivalDate: new Date("2023-09-01"),
    };

    const result = validationForm(flight);

    expect(result.isValid).toBe(false);
    expect(result.msg.city).toBe(true);
    expect(result.msg.time).toBe(true);
  });

  test("should return invalid error form when departure city or arrival city is missing", () => {
    const flight: FlightRequest = {
      departureCity: "City A",
    };

    const result = validationForm(flight);

    expect(result.isValid).toBe(false);
    expect(result.msg.city).toBe(true);
    expect(result.msg.time).toBe(true);
  });

  test("should return invalid error form when departure date or arrival date is missing", () => {
    const flight: FlightRequest = {
      departureDate: new Date("2023-09-01"),
    };

    const result = validationForm(flight);

    expect(result.isValid).toBe(false);
    expect(result.msg.city).toBe(true);
    expect(result.msg.time).toBe(true);
  });
});

describe("IErrorForm", () => {
  test("should have the correct properties", () => {
    const errorForm: IErrorForm = {
      isValid: true,
      msg: {
        city: false,
        time: true,
      },
    };

    expect(errorForm).toHaveProperty("isValid", true);
    expect(errorForm).toHaveProperty("msg");
    expect(errorForm.msg).toHaveProperty("city", false);
    expect(errorForm.msg).toHaveProperty("time", true);
  });
});

describe("IErrorMsg", () => {
  test("should have the correct properties", () => {
    const errorMsg: IErrorMsg = {
      city: true,
      time: false,
    };

    expect(errorMsg).toHaveProperty("city", true);
    expect(errorMsg).toHaveProperty("time", false);
  });
});
