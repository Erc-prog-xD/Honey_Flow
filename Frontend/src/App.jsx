import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
import Login from './pages/Login.jsx';
import ForgotPassword from './pages/ForgotPassword.jsx';
import Menu from './pages/Menu.jsx';
import Dashboard from "./pages/Dashboard";
import HiveRegistration from './pages/HiveRegistration';
import PrivateRoute from './routes/PrivateRoute';

import './assets/css/global.css';

function App() {
  return (
    <Router>
      <Routes>

        {/* Rotas públicas */}
        <Route path="/" element={<Login />} />
        <Route path="/forgot-password" element={<ForgotPassword />} />

        {/* Rotas protegidas */}
        <Route element={<PrivateRoute />}>
          <Route path="/menu" element={<Menu />} />
          <Route path="/dashboard" element={<Dashboard />} />
          <Route path="/cadastro-colmeia" element={<HiveRegistration />} />
        </Route>

      </Routes>
    </Router>
  );
}

export default App;
