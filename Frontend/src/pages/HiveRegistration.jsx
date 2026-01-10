import React, { useState, useEffect } from 'react';
import { useNavigate } from 'react-router-dom';
import { MapContainer, TileLayer, Marker, useMapEvents, useMap } from 'react-leaflet';
import L from 'leaflet';
import 'leaflet/dist/leaflet.css';
import '../assets/css/HiveRegistration.css';

// Components
import Navbar from '../components/Navbar';
import ActionButtons from '../components/ActionButtons';
import ToastCenter from '../components/Toast';

// Custom Marker Icon
import pinIcon from '../assets/img/pin-localizacao.svg';
const customIcon = L.icon({
    iconUrl: pinIcon,
    iconSize: [40, 40],
    iconAnchor: [20, 40],
});

const LocationPicker = ({ onLocationSelect }) => {
    const [position, setPosition] = useState(null);
    const [hasLocated, setHasLocated] = useState(false);
    const map = useMap();

    useEffect(() => {
        if (!hasLocated) {
            map.locate().on("locationfound", function (e) {
                setPosition(e.latlng);
                map.flyTo(e.latlng, 15);
                onLocationSelect(e.latlng.lat, e.latlng.lng);
                setHasLocated(true);
            });
        }
    }, [map, onLocationSelect, hasLocated]);

    useMapEvents({
        click(e) {
            setPosition(e.latlng);
            onLocationSelect(e.latlng.lat, e.latlng.lng);
        },
    });

    return position === null ? null : (
        <Marker position={position} icon={customIcon} />
    );
};

const HiveRegistration = () => {
    const navigate = useNavigate();
    const [coords, setCoords] = useState({ lat: '', lng: '' });
    const [toast, setToast] = useState(null);
    const [formData, setFormData] = useState({
        apiario: '',
        anoColmeia: '',
        anoRainha: ''
    });

    const showToast = (message, type) => {
        setToast({ message, type });
    };

    const handleLocationSelect = (lat, lng) => {
        setCoords({ lat: lat.toFixed(6), lng: lng.toFixed(6) });
    };

    const handleBack = () => {
        navigate('/dashboard');
    };

    const handleSave = () => {
        if (!formData.apiario || !formData.anoColmeia || !coords.lat) {
            showToast('Por favor, preencha as informações básicas e selecione o local no mapa.', 'error');
            return;
        }

        const newHive = {
            id: Date.now(),
            ...formData,
            ...coords,
            createdAt: new Date().toISOString()
        };

        try {
            const existingHives = JSON.parse(localStorage.getItem('hf_hives') || '[]');
            const updatedHives = [...existingHives, newHive];
            localStorage.setItem('hf_hives', JSON.stringify(updatedHives));

            showToast('Colmeia cadastrada com sucesso!', 'success');

            setTimeout(() => {
                navigate('/dashboard');
            }, 1500);
        } catch (error) {
            console.error("Error saving to localStorage:", error);
            showToast('Erro ao salvar os dados. Tente novamente.', 'error');
        }
    };

    return (
        <div className="registration-page">
            <Navbar />

            <main className="reg-content">
                <div className="reg-header-bar">
                    <div className="title-box">
                        <h1>Cadastro de colmeia</h1>
                    </div>
                    <ActionButtons onCancel={handleBack} onSave={handleSave} />
                </div>

                <div className="reg-grid">
                    {/* Left Column: General Info */}
                    <div className="reg-card card-info">
                        <h2>Informações gerais</h2>
                        <div className="input-group">
                            <label>Selecione o apiário</label>
                            <div className="select-with-btn">
                                <select
                                    value={formData.apiario}
                                    onChange={(e) => setFormData({ ...formData, apiario: e.target.value })}
                                >
                                    <option value="">Selecione o apiário</option>
                                    <option value="1">Apiário Sertão</option>
                                    <option value="2">Apiário Vale</option>
                                </select>
                                <button className="add-apiary-btn">+</button>
                            </div>
                        </div>

                        <div className="input-group">
                            <label>Ano da colmeia</label>
                            <input
                                type="text"
                                placeholder=""
                                value={formData.anoColmeia}
                                onChange={(e) => setFormData({ ...formData, anoColmeia: e.target.value })}
                            />
                        </div>

                        <div className="input-group">
                            <label>Ano da rainha</label>
                            <input
                                type="text"
                                placeholder=""
                                value={formData.anoRainha}
                                onChange={(e) => setFormData({ ...formData, anoRainha: e.target.value })}
                            />
                        </div>
                    </div>

                    {/* Right Column: Location */}
                    <div className="reg-card card-location">
                        <h2>Localização</h2>
                        <div className="map-picker-container">
                            <label>Selecione a localização no mapa</label>
                            <div className="mini-map-wrapper">
                                <MapContainer center={[-23.5505, -46.6333]} zoom={13} style={{ height: '100%', width: '100%' }} zoomControl={false}>
                                    <TileLayer url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png" />
                                    <LocationPicker onLocationSelect={handleLocationSelect} />
                                </MapContainer>
                            </div>
                        </div>

                        <div className="coords-row">
                            <div className="input-group">
                                <label>Longitude</label>
                                <div className="coord-input">
                                    <span>X:</span>
                                    <input type="text" value={coords.lng} readOnly />
                                </div>
                            </div>
                            <div className="input-group">
                                <label>Latitude</label>
                                <div className="coord-input">
                                    <span>Y:</span>
                                    <input type="text" value={coords.lat} readOnly />
                                </div>
                            </div>
                        </div>
                    </div>
                </div>
            </main>

            {/* Toast Notification */}
            {toast && (
                <div className="toast-center-container">
                    <ToastCenter
                        message={toast.message}
                        type={toast.type}
                        onClose={() => setToast(null)}
                    />
                </div>
            )}
        </div>
    );
};

export default HiveRegistration;
