import React, { useContext} from "react";

import { MyContext } from "../../context";

import DataListFlight from "./DataListFlight";
import { Box, Grid, Pagination, Skeleton } from "@mui/material";
import { FlightResponse } from "../../nswag";
import { canDisplay } from "../../utile";


export const checkSize = (
  obj: FlightResponse[] | undefined,
  page?: number,
  size?: number
) => {
  if (obj && page && size) {
    if (obj.length < size) {
      return page;
    }
  }
  return 2;
};

const ListFlights = () => {
  const {
    flightList,
    pageNumber,
    pageSize,
    isLoading,
    handlePageNumber,
  } = useContext(MyContext);


  const findListPage = (page: number) => {
    return flightList?.find((item) => item.page === page);
  };

  const handleChangePage = (e: React.ChangeEvent<unknown>, page: number) => {
    const items = findListPage(pageNumber)?.list;
    if (items?.length && items.length > pageSize) {
      handlePageNumber(page);
    }
  };
 

  return (
    <div style={{ width: "100%" }}>
      {flightList && flightList.length > 0 && (
        <>
        {canDisplay(pageNumber,flightList)?<><DataListFlight itemData={findListPage(pageNumber)?.list} /> 
         <Box>
           <Grid
             sx={{
               display: "flex",
               alignItems: "center",
               justifyContent: "center",
             }}
           >
             <Pagination
               count={checkSize(
                 findListPage(pageNumber)?.list,
                 pageNumber,
                 pageSize
               )}
               onChange={handleChangePage}
             />
           </Grid>
         </Box></>
         :<span>No Flights</span>}
       </>
      )}
      {isLoading && (
        <Skeleton
          variant="rectangular"
          width="100%"
          sx={{
            display: "flex",
            alignItems: "center",
            justifyContent: "center",
          }}
        ></Skeleton>
      )}
    </div>
  );
};

export default ListFlights;
