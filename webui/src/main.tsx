import React from 'react'
import ReactDOM from 'react-dom/client'
import store from './store.ts';
import PharmacyManager from './pages/App/PharmacyManager.tsx'
import './index.css'
import { Provider } from 'react-redux'
import Header from './shared/Header.tsx';


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <div style={{display: 'flex', flexDirection: 'column', width: '100%', justifyContent: 'center' }}>
        <Header/>
        <PharmacyManager/>
      </div>
    </Provider>
  </React.StrictMode>,
)
