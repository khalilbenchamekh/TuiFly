import React from 'react';
import { AppBar, Toolbar, Typography, Box } from '@mui/material';
import LanguageSelector from '../languageSelector';

const Navbar = () => {
  return (
    <AppBar sx={{backgroundColor:"white"}} position="static">
      <Toolbar disableGutters >
        <Box sx={{ flexGrow: 1, display: 'flex', alignItems: 'center',justifyContent: 'end' }}>
          <Typography variant="h6" component="div">
          <LanguageSelector />
          </Typography>
        </Box>
      </Toolbar>
    </AppBar>
  );
};

export default Navbar;