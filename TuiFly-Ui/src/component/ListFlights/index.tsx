import React, { useContext} from "react";

import { MyContext } from "../../context";

import DataListFlight from "./DataListFlight";
import { Box, Card, Grid, Pagination, Skeleton } from "@mui/material";
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
        <Box width="100%" sx={{display:"flex",alignItems:'center',justifyContent:'center'}}>
        <Skeleton
          variant="rectangular"
          width="30%" height={300} >
          <Card sx={{height:400,display:'flex',alignContent:'center',justifyContent:'center'}}>

          </Card>
        </Skeleton></Box>
      )}
    </div>
  );
};

export default ListFlights;
