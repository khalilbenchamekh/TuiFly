import { Env, initialisationEnv } from ".";


describe('Env class', () => {
  it('should set the apiEnv property correctly', () => {
    const json = { apiEnv: 'http://example.com/api' };
    global.fetch = jest.fn().mockResolvedValue({
      json: jest.fn().mockResolvedValue(json),
    });

    return initialisationEnv().then(() => {
      expect(Env.apiEnv).toBe(json.apiEnv);
    });
  });
});