import ReactDOM from 'react-dom/client';
import PharmacyManager  from './pages/PharmacyManager/PharmacyManager.tsx';
import './styles/index.scss';
import { Route, createBrowserRouter, createRoutesFromElements, RouterProvider, BrowserRouter, Routes } from 'react-router-dom';
import ReportManager from './pages/ReportManager/ReportManager.tsx';
import PharmacistManager from './pages/PharmacistManager/PharmacistManager.tsx';
import WarehousePage from './pages/WarehousePage/WarehousePage.tsx';
import DeliveryManager from './pages/DeliveryManager/DeliveryManager.tsx';
import Layout from './layout.tsx';
import PharmacyTestPage from './pages/PharmacyTestPage/PharmacyTestPage.tsx';


// const router = createBrowserRouter(
//   createRoutesFromElements(
//     <Route path="/" element={<Layout/>}>
//       <Route index element={<PharmacyManager/>} />
//       <Route path="/pharmacists" element={<PharmacistManager/>} />
//       <Route path="/deliveries" element={<DeliveryManager/>} />
//       <Route path="/reports" element={<ReportManager/>} />   
//       <Route path="/warehouse" element={<WarehousePage/>} />
//     </Route>
//   )
// );

const router = (
  <BrowserRouter>
    <Routes>
      <Route path="/" element={<Layout/>}>
        <Route index element={<PharmacyManager/>} />
        <Route path="/pharmacists" element={<PharmacistManager/>} />
        <Route path="/deliveries" element={<DeliveryManager/>} />
        <Route path="/reports" element={<ReportManager/>} />   
        <Route path="/warehouse" element={<WarehousePage/>} />
      </Route>
    </Routes>
  </BrowserRouter>
);


ReactDOM.createRoot(document.getElementById('root')!).render(                 
    // <RouterProvider router={router}/>     
    <BrowserRouter>
    <Routes>
      <Route path="/" element={<Layout/>}>
        <Route index element={<PharmacyManager/>} />
        <Route path="/pharmacists" element={<PharmacistManager/>} />
        <Route path="/deliveries" element={<DeliveryManager/>} />
        <Route path="/reports" element={<ReportManager/>} />   
        <Route path="/warehouse" element={<WarehousePage/>} />
        <Route path="/pharmaciesTest" element={<PharmacyTestPage/>} />
      </Route>
    </Routes>
  </BrowserRouter>
)
