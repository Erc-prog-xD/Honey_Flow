import React from 'react';
import { Hexagon, DollarSign, TrendingDown, BarChart3, Store } from 'lucide-react';
import Sidebar from '../components/Sidebar';
import MapArea from '../components/MapArea';
import DashboardCard from '../components/DashboardCard';
import Navbar from '../components/Navbar';
import AddMenu from '../components/AddMenu';
import '../assets/css/Dashboard.css';

// Logo
import logoText from '../assets/img/logo-hf-completo.svg';

const Dashboard = () => {
    const [showAddMenu, setShowAddMenu] = React.useState(false);

    return (
        <div className="dashboard-layout">
            <Navbar />
            <div className="top-section">
                <div className="sidebar-wrapper">
                    <Sidebar />
                </div>
                <div className="map-wrapper">
                    <MapArea />
                    <button className="map-add-btn" onClick={() => setShowAddMenu(true)}>+</button>
                </div>
            </div>

            <div className="bottom-section">

                <div className="dashboard-brand-header">
                    <img src={logoText} alt="HoneyFlow" className="brand-logo-large" />
                </div>

                <div className="section-container">
                    <div className="section-divider">Registros</div>
                    <div className="cards-grid">
                        <DashboardCard title="Registro de Produção" icon={Hexagon} />
                        <DashboardCard title="Registro de Vendas" icon={DollarSign} />
                        <DashboardCard title="Registro de Perdas" icon={TrendingDown} />
                    </div>
                </div>

                <div className="section-container">
                    <div className="section-divider">Relatórios</div>
                    <div className="cards-grid">
                        <DashboardCard title="Desempenho de apiário" icon={BarChart3} />
                        <DashboardCard title="Relatório de Vendas" icon={Store} />
                    </div>
                </div>
            </div>

            {showAddMenu && <AddMenu onClose={() => setShowAddMenu(false)} />}
        </div>
    );
};

export default Dashboard;