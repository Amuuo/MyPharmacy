import {
  Sidebar,
  SubMenu,
  Menu,
  MenuItem,
} from "react-pro-sidebar";
import styles from "./SidebarNav.module.scss";
import {
  MenuRounded,
  GridViewRounded,
  BarChartRounded,
  TimelineRounded,
  BubbleChartRounded,
  AccountBalanceRounded,
  SavingsRounded,
  SettingsApplicationsRounded,
  AccountCircleRounded,
  ShieldRounded,
  NotificationsRounded,
  LogoutRounded,
  LocalShipping,
  Medication,
  LocalPharmacy
} from "@mui/icons-material";
import { sidebarStore, toggleCollapsed } from "../../../stores/sidebarStore";
import { useStore } from "effector-react";
import { NavLink } from "react-router-dom";



export default function SidebarNav() {

  const { collapsed } = useStore(sidebarStore);

  const handleCollapsedChange = () => {
    toggleCollapsed();
  };

  return (
    <Sidebar
      className={styles.sidebar}      
      collapsed={collapsed}    
      collapsedWidth="4.5rem"
      width="12rem"      
    >
      <Menu className={styles.menu}>
        <MenuItem icon={<MenuRounded />}
                  onClick={ ()=> toggleCollapsed() }/>
        <MenuItem icon={<GridViewRounded /> }>Dashboard</MenuItem>
        <MenuItem icon={<NavLink to="/"><LocalPharmacy /></NavLink>}><NavLink to="/"> Pharmacy</NavLink> </MenuItem>        
          <MenuItem icon={<NavLink  to="pharmacists"><Medication  /></NavLink>}>
            <NavLink  to="pharmacists">Pharmacists</NavLink>
          </MenuItem>
          <MenuItem icon={<NavLink to="/pharmaciesTest"><SavingsRounded /></NavLink>}><NavLink to="/pharmaciesTest">Most Profitable</NavLink></MenuItem>        
        <MenuItem icon={<LocalShipping />}>Delivery</MenuItem>
        <SubMenu label="Charts" icon={<BarChartRounded />}>
          <MenuItem icon={<TimelineRounded />}>Monthly Sales</MenuItem>
          <MenuItem icon={<BubbleChartRounded />}>Yearly Sales</MenuItem>
        </SubMenu>
        <SubMenu label="Settings" icon={<SettingsApplicationsRounded />}>
          <MenuItem icon={<AccountCircleRounded />}>Account</MenuItem>
          <MenuItem icon={<ShieldRounded />}>Privacy</MenuItem>
          <MenuItem icon={<NotificationsRounded />}>
            Notifications
          </MenuItem>
        </SubMenu>
        <MenuItem icon={<LogoutRounded />}>Logout</MenuItem>
      </Menu>
    </Sidebar>
  );
};
