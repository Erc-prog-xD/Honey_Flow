import React from 'react';
import { MapContainer, TileLayer, Marker, Popup, useMapEvents } from 'react-leaflet';
import L from 'leaflet';
import 'leaflet/dist/leaflet.css';

// Correção para ícone padrão do Leaflet no React
import icon from '../assets/img/pin-localizacao.svg';

let DefaultIcon = L.icon({
    iconUrl: icon,
    iconSize: [60, 60],
    iconAnchor: [30, 60], // Ponta inferior centralizada (metade da largura, altura total)
    popupAnchor: [0, -60], // Popup aparece acima do ícone
});

L.Marker.prototype.options.icon = DefaultIcon;

const LocationMarker = () => {
    const [position, setPosition] = React.useState(null);
    const map = useMapEvents({
        locationfound(e) {
            setPosition(e.latlng);
            map.flyTo(e.latlng, map.getZoom());
        },
    });

    React.useEffect(() => {
        map.locate();
    }, [map]);

    return position === null ? null : (
        <Marker position={position}>
            <Popup>Você está aqui</Popup>
        </Marker>
    );
};

const MapArea = () => {
    const initialPosition = [-23.55052, -46.633308];
    const [hives, setHives] = React.useState([]);

    React.useEffect(() => {
        const savedHives = JSON.parse(localStorage.getItem('hf_hives') || '[]');
        setHives(savedHives);
    }, []);

    return (
        <div style={{ width: "100%", height: "100%", minHeight: "350px", position: "relative", overflow: "hidden" }}>
            <MapContainer
                center={initialPosition}
                zoom={13}
                style={{ height: "100%", width: "100%", position: "absolute", top: 0, left: 0 }}
            >
                <TileLayer
                    attribution='&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors'
                    url="https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png"
                />
                <LocationMarker />

                {hives.map(hive => (
                    <Marker key={hive.id} position={[parseFloat(hive.lat), parseFloat(hive.lng)]}>
                        <Popup>
                            <strong>Colmeia #{hive.id.toString().slice(-4)}</strong><br />
                            Apiário: {hive.apiario === '1' ? 'Sertão' : 'Vale'}<br />
                            Ano: {hive.anoColmeia}
                        </Popup>
                    </Marker>
                ))}
            </MapContainer>
        </div>
    );
};

export default MapArea;