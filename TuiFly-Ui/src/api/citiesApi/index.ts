export const getCities = async () => {
  const res = await fetch("./data/cities.json");
  return await res.json()
};