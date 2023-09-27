import { ApiClient } from ".";
import { Client } from "../../nswag";

describe("ApiClient", () => {
  test("should return a new instance of Client", () => {
    const client = ApiClient();
    expect(client).toBeInstanceOf(Client);
  });
});
