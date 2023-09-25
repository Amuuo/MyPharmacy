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
                  onClick={handleCollapsedChange}/>
        <MenuItem icon={<GridViewRounded />}>Dashboard</MenuItem>
        <MenuItem icon={<LocalPharmacy />}>Pharmacy</MenuItem>
        <SubMenu label="Drug" icon={<Medication />}>
          <MenuItem icon={<AccountBalanceRounded />}>
            Most Demanded
          </MenuItem>
          <MenuItem icon={<SavingsRounded />}>Most Profitable</MenuItem>
        </SubMenu>
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
