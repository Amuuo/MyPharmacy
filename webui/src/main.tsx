import React from 'react'
import ReactDOM from 'react-dom/client'
import store from './store.ts';
import App from './pages/App/App.tsx'
import './index.css'
import { Provider } from 'react-redux'
import PharmacyAppBar from './MainAppBar.tsx';


ReactDOM.createRoot(document.getElementById('root')!).render(
  <React.StrictMode>
    <Provider store={store}>
      <div style={{display: 'flex', flexDirection: 'column', width: '100%' }}>
        <PharmacyAppBar/>
        <App />
      </div>
    </Provider>
  </React.StrictMode>,
)
