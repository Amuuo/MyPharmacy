import React from 'react';
import ReactDOM from 'react-dom/client';
import store from './store/store.ts';
import PharmacyManager  from './pages/PharmacyManager/PharmacyManager.tsx';
import './index.scss';
import { Provider } from 'react-redux';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Header from './components/Header/Header.tsx';
import ReportManager from './pages/ReportManager/ReportManager.tsx';
import PharmacistManager from './pages/PharmacistManager/PharmacistManager.tsx';


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>      
      <Header/>        
      <BrowserRouter>
        <Routes>
          <Route path="/" Component={PharmacyManager} />
          <Route path="/pharmacists" Component={PharmacistManager} />
          <Route path="/reports" Component={ReportManager} />            
        </Routes>
      </BrowserRouter>                   
    </Provider>
  </React.StrictMode>,
)
