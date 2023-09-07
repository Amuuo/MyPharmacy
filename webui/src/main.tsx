import React from 'react';
import ReactDOM from 'react-dom/client';
import store from './store/store.ts';
import PharmacyManager  from './pages/PharmacyManager/PharmacyManager.tsx';
import './index.scss';
import { Provider } from 'react-redux';
import { BrowserRouter, Routes, Route } from 'react-router-dom';
import Header from './components/Header/Header.tsx';
import ReportManager from './pages/ReportManager/ReportManager.tsx';


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <div className="main-container">
        <Header/>
        <div style={{padding: '3rem'}}>
          <BrowserRouter>
            <Routes>
              <Route path="/" element={<PharmacyManager/>}/>
              <Route path="reports" element={<ReportManager/>}/>            
            </Routes>
          </BrowserRouter>        
        </div>
      </div>
    </Provider>
  </React.StrictMode>,
)
