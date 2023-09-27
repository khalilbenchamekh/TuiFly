import React from "react";
import {
  Select,
  MenuItem,
  SelectChangeEvent,
  ListItemIcon,
} from "@mui/material";
import { FlagIcon } from "react-flag-kit";
import { useTranslation } from "react-i18next";


const LanguageSelector = () => {
  const { i18n } = useTranslation();
  const handleLanguageChange = (event: SelectChangeEvent<string>) => {
    i18n.changeLanguage(event.target.value)
  };

  return (
    <Select value={i18n.language} onChange={handleLanguageChange}>
      <MenuItem value="fr">
        <ListItemIcon>
          <FlagIcon code="FR" size={20}/>
          FR
        </ListItemIcon>
      </MenuItem>
      <MenuItem value="en">
        <ListItemIcon>
          <FlagIcon code="US" size={20}/>
          EN
        </ListItemIcon>
      </MenuItem>
    </Select>
  );
};

export default LanguageSelector;
