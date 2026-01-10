import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './pages/Login.jsx';
import ForgotPassword from './pages/ForgotPassword.jsx';
import Menu from './pages/Menu.jsx';
import Dashboard from "./pages/Dashboard";
import HiveRegistration from './pages/HiveRegistration';

import './assets/css/global.css';

function App() {
  return (
    <Router>
      <Routes>
        <Route path="/" element={<Login />} />
        <Route path="/forgot-password" element={<ForgotPassword />} />
        <Route path="/menu" element={<Menu />} />
        <Route path="/dashboard" element={<Dashboard />} />
        <Route path="/cadastro-colmeia" element={<HiveRegistration />} />
      </Routes>
    </Router>
  );
}

export default App;