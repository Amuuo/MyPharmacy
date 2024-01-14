import Header from './components/shared/Header/Header.tsx';
import SidebarNav from './components/shared/Sidebar/SidebarNav.tsx';
import { Outlet } from 'react-router-dom';

export default function Layout () {
    return (
        <>
            <Header />
            <SidebarNav />
            <Outlet/>            
        </>
    );
}


