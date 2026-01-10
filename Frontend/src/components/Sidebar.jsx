import React, { useState } from 'react';
import { Archive, Hexagon, ChevronDown, ChevronRight } from 'lucide-react';
import '../assets/css/Sidebar.css'; // Vamos criar esse CSS abaixo

const SidebarItem = ({ title, children }) => {
    const [isOpen, setIsOpen] = useState(true);

    return (
        <div className="sidebar-group">
            <div className="sidebar-header" onClick={() => setIsOpen(!isOpen)}>
                {isOpen ? <ChevronDown size={16} /> : <ChevronRight size={16} />}
                <Archive className="icon-apiary" size={18} />
                <span>{title}</span>
            </div>
            {isOpen && <div className="sidebar-content">{children}</div>}
        </div>
    );
};

const HiveItem = ({ name }) => (
    <div className="hive-item">
        <Hexagon className="icon-hive" size={16} />
        <span>{name}</span>
    </div>
);

const Sidebar = () => {
    return (
        <div className="sidebar-container">
            <SidebarItem title="Apiário 1">
                <HiveItem name="Colmeia 1" />
                <HiveItem name="Colmeia 2" />
                <HiveItem name="Colmeia 3" />
                <HiveItem name="Colmeia 4" />
            </SidebarItem>

            <SidebarItem title="Apiário 2">
                <HiveItem name="Colmeia 1" />
                <HiveItem name="Colmeia 2" />
            </SidebarItem>
        </div>
    );
};

export default Sidebar;