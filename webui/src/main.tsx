import React from 'react';
import ReactDOM from 'react-dom/client';
import PharmacyManager  from './pages/PharmacyManager/PharmacyManager.tsx';
import './styles/index.scss';
import { Route, createBrowserRouter, createRoutesFromElements, RouterProvider } from 'react-router-dom';
import Header from './components/shared/Header/Header.tsx';
import ReportManager from './pages/ReportManager/ReportManager.tsx';
import PharmacistManager from './pages/PharmacistManager/PharmacistManager.tsx';
import SidebarNav from './components/shared/Sidebar/SidebarNav.tsx';
import WarehousePage from './pages/WarehousePage/WarehousePage.tsx';
import DeliveryManager from './pages/DeliveryManager/DeliveryManager.tsx';


const router = createBrowserRouter(
  createRoutesFromElements(
    <Route path="/" >
      <Route index element={<PharmacyManager/>} />
      <Route path="/pharmacists" Component={PharmacistManager} />
      <Route path="/deliveries" Component={DeliveryManager} />
      <Route path="/reports" Component={ReportManager} />   
      <Route path="/warehouse" Component={WarehousePage} />
    </Route>
  )
);

ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
      <Header/>      
      <SidebarNav/>  
      <RouterProvider router={router}/>                     
  </React.StrictMode>,
)
