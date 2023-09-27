import { dateFormat, flightMapper, sortPriceMapper } from ".";


describe('dateFormat', () => {
  it('should format the time correctly', () => {
    // Arrange
    const time = new Date('2023-09-23T10:30:00Z');

    // Act
    const result = dateFormat(time);

    // Assert
    expect(result).toBe('11:30 AM');
  });

  it('should return undefined if time is undefined', () => {
    // Arrange
    const time = undefined;

    // Act
    const result = dateFormat(time);

    // Assert
    expect(result).toBeUndefined();
  });
});

describe('sortPriceMapper', () => {
  it('should sort the results by price in ascending order', () => {
    // Arrange
    const results = {
      response: [
        { price: 100 },
        { price: 50 },
        { price: 200 },
      ],
    };

    // Act
    const sortedResults = sortPriceMapper(results);

    // Assert
    expect(sortedResults).toEqual([
      { price: 50 },
      { price: 100 },
      { price: 200 },
    ]);
  });

  it('should return undefined if results.response is undefined', () => {
    // Arrange
    const results = {
      response: undefined,
    };

    // Act
    const sortedResults = sortPriceMapper(results);

    // Assert
    expect(sortedResults).toBeUndefined();
  });

  it('should return an empty array if results.response is an empty array', () => {
    // Arrange
    const results = {
      response: [],
    };

    // Act
    const sortedResults = sortPriceMapper(results);

    // Assert
    expect(sortedResults).toEqual([]);
  });
})

describe('flightMapper', () => {
  it('rendr flightMapper should sort the results by price in ascending order', () => {
    // Arrange
    const results = {
      response: [
        { price: 100 },
        { price: 50 },
        { price: 200 },
      ],
    };

    // Act
    const sortedResults = flightMapper(results);

    // Assert
    expect(sortedResults).toEqual([
      { price: 50 },
      { price: 100 },
      { price: 200 },
    ]);
  });

  it('rendr flightMapper should return undefined if results.response is undefined', () => {
    // Arrange
    const results = {
      response: undefined,
    };

    // Act
    const sortedResults = flightMapper(results);

    // Assert
    expect(sortedResults).toBeUndefined();
  });

  it('should return an empty array if results.response is an empty array', () => {
    // Arrange
    const results = {
      response: [],
    };

    // Act
    const sortedResults = sortPriceMapper(results);

    // Assert
    expect(sortedResults).toEqual([]);
  });
})



