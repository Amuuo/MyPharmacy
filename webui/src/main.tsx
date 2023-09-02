import React from 'react'
import ReactDOM from 'react-dom/client'
import store from './store.ts';
import PharmacyManager  from './pages/PharmacyManager/PharmacyManager.tsx';
import './index.scss'
import { Provider } from 'react-redux'
import Header from './shared/Header.tsx';


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <div className="main-container">
        <Header/>
        <PharmacyManager/>
      </div>
    </Provider>
  </React.StrictMode>,
)
