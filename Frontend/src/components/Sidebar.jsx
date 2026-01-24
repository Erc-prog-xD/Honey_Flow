import { Archive, Hexagon, ChevronDown, ChevronRight } from 'lucide-react';
import { useEffect, useState } from 'react';
import {
    buscarApiarios,
    buscarColmeiasDoApiario
} from '../services/apiarioService';
import '../assets/css/Sidebar.css';

/* ===========================
   ITEM DO APIÁRIO
=========================== */
const SidebarItem = ({ apiario }) => {
    const [isOpen, setIsOpen] = useState(false);
    const [colmeias, setColmeias] = useState([]);
    const [loading, setLoading] = useState(false);

    const toggleOpen = async () => {
        const abrir = !isOpen;
        setIsOpen(abrir);

        if (abrir && colmeias.length === 0) {
            setLoading(true);
            try {
                const response = await buscarColmeiasDoApiario(apiario.id);
                console.log(response);
                // response.data É o array
                setColmeias(response.dados ?? []);
            } catch (error) {
                console.error('Erro ao buscar colmeias', error);
            } finally {
                setLoading(false);
            }
        }
    };

    return (
        <div className="sidebar-group">
            <div className="sidebar-header" onClick={toggleOpen}>
                {isOpen ? <ChevronDown size={16} /> : <ChevronRight size={16} />}
                <Archive className="icon-apiary" size={18} />
                <span>Apiário Id#{apiario.id}</span>
            </div>

            {isOpen && (
                <div className="sidebar-content">
                    {loading && <span>Carregando...</span>}

                    {!loading && colmeias.length === 0 && (
                        <span>Nenhuma colmeia</span>
                    )}

                    {!loading && colmeias.map(colmeia => (
                        <HiveItem
                            key={colmeia.Id}
                            name={`Colmeia Ano: ${colmeia.anoColmeia}`}
                        />
                    ))}
                </div>
            )}
        </div>
    );
};

/* ===========================
   ITEM DA COLMEIA
=========================== */
const HiveItem = ({ name }) => (
    <div className="hive-item">
        <Hexagon className="icon-hive" size={16} />
        <span>{name}</span>
    </div>
);

/* ===========================
   SIDEBAR
=========================== */
const Sidebar = () => {
    const [apiarios, setApiarios] = useState([]);
    const [loading, setLoading] = useState(true);

    useEffect(() => {
        const carregarApiarios = async () => {
            try {
                const response = await buscarApiarios();

                // GARANTIA DE ARRAY
                console.log(apiarios);
                setApiarios(response.dados ?? []);
            } catch (error) {
                console.error('Erro ao buscar apiários', error);
                setApiarios([]);
            } finally {
                setLoading(false);
            }
        };

        carregarApiarios();
    }, []);

    if (loading) {
        return <div className="sidebar-container">Carregando...</div>;
    }

    return (
        <div className="sidebar-container">
            {apiarios.length === 0 && (
                <span>Nenhum apiário encontrado</span>
            )}

            {apiarios.map(apiario => (
                <SidebarItem
                    key={apiario.id}
                    apiario={apiario}
                />
            ))}
        </div>
    );
};

export default Sidebar;
