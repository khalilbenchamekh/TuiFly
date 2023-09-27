import React from "react";
import { render, screen } from "@testing-library/react";
import { I18nextProvider } from "react-i18next";
import DataListFlight from ".";
import i18n from "../../../i18n";

describe("DataListFlight", () => {
  it("should render the flight data correctly", () => {
    // Arrange
    const itemData = [
      {
        departureCity: "City A",
        arrivalCity: "City B",
        departureDate: new Date("2023-09-23"),
        arrivalDate: new Date("2023-09-24"),
        price: 100,
      },
      {
        departureCity: "City C",
        arrivalCity: "City D",
        departureDate: new Date("2023-09-25"),
        arrivalDate: new Date("2023-09-26"),
        price: 200,
      },
    ];

    // Act
    render(
      <I18nextProvider i18n={i18n}>
        <DataListFlight itemData={itemData} />
      </I18nextProvider>
    );

    // Assert
    expect(screen.getByText("City A")).toBeInTheDocument();
    expect(screen.getByText("City B")).toBeInTheDocument();
    expect(screen.getByText("City C")).toBeInTheDocument();
    expect(screen.getByText("City D")).toBeInTheDocument();
    expect(screen.getByText("100")).toBeInTheDocument();
    expect(screen.getByText("200")).toBeInTheDocument();
  });
});
