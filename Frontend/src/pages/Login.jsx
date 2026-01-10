import React, { useState } from 'react';
import { Mail, Lock, User, CreditCard } from 'lucide-react';
import { Link, useNavigate } from 'react-router-dom';
import '../assets/css/Login.css';
import logoBee from '../assets/img/logo_hf.svg';
import ToastCenter from '../components/Toast';

// --- Componente Input ---
const Input = ({ icon: Icon, prefix, ...props }) => {
  return (
    <div className="input-wrapper">
      {Icon && <Icon className="input-icon" size={20} />}
      {prefix && <span className="input-prefix">{prefix}</span>}
      <input className="input-field" {...props} />
    </div>
  );
};

const Login = () => {
  const [isLogin, setIsLogin] = useState(true);
  const [toast, setToast] = useState(null);

  const navigate = useNavigate();

  // Login state
  const [loginEmail, setLoginEmail] = useState('');
  const [loginPassword, setLoginPassword] = useState('');

  // Register state
  const [name, setName] = useState('');
  const [cpf, setCpf] = useState('');
  const [registerEmail, setRegisterEmail] = useState('');
  const [registerPassword, setRegisterPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');

  const showToast = (message, type) => {
    setToast({ message, type });
  };

  const formatCPF = (value) => {
    const digits = value.replace(/\D/g, '').slice(0, 11);
    if (digits.length <= 3) return digits;
    if (digits.length <= 6) return digits.replace(/(\d{3})(\d+)/, '$1.$2');
    if (digits.length <= 9) return digits.replace(/(\d{3})(\d{3})(\d+)/, '$1.$2.$3');
    return digits.replace(/(\d{3})(\d{3})(\d{3})(\d{1,2})/, '$1.$2.$3-$4');
  };

  const handleCpfChange = (e) => {
    setCpf(formatCPF(e.target.value));
  };

  const handleLoginSubmit = (e) => {
    e.preventDefault();
    console.log('login', { loginEmail, loginPassword });
    showToast('Login realizado com sucesso!', 'success');
    setTimeout(() => navigate('/dashboard'), 700);
  };

  const handleRegisterSubmit = (e) => {
    e.preventDefault();

    if (registerPassword !== confirmPassword) {
      showToast('As senhas não coincidem.', 'error');
      return;
    }

    const cpfDigits = cpf.replace(/\D/g, '');
    if (cpfDigits.length !== 11) {
      showToast('CPF inválido. Digite os 11 dígitos.', 'warning');
      return;
    }

    console.log('register', { name, cpf, registerEmail, registerPassword });
    showToast('Cadastro realizado com sucesso!', 'success');
    setTimeout(() => setIsLogin(true), 700);
  };

  return (
    <div className="login-container">
      <div className="login-card">

        {/* Lado Esquerdo - Amarelo */}
        <div className="left-panel">
          <img src={logoBee} alt="Bee Logo" className="bee-logo" />
        </div>

        {/* Lado Direito - Branco */}
        <div className="right-panel">
          <h1>Bem-vindo</h1>

          <div className="toggle-container">
            <button
              className={`toggle-btn ${isLogin ? 'active' : ''}`}
              onClick={() => setIsLogin(true)}
            >
              Entrar
            </button>
            <button
              className={`toggle-btn ${!isLogin ? 'active' : ''}`}
              onClick={() => setIsLogin(false)}
            >
              Novo usuário
            </button>
          </div>

          {/* Formulário de Login */}
          {isLogin ? (
            <form className="login-form" onSubmit={handleLoginSubmit}>
              <Input
                type="email"
                placeholder="Email"
                icon={Mail}
                value={loginEmail}
                onChange={(e) => setLoginEmail(e.target.value)}
                required
              />
              <Input
                type="password"
                placeholder="Senha"
                icon={Lock}
                value={loginPassword}
                onChange={(e) => setLoginPassword(e.target.value)}
                required
              />

              <div className="form-bottom">
                <Link to="/forgot-password" className="forgot-link">
                  Esqueceu a senha?
                </Link>
              </div>

              <button type="submit" className="submit-btn">
                Entrar
              </button>
            </form>
          ) : (
            /* Formulário de Cadastro */
            <form className="login-form" onSubmit={handleRegisterSubmit}>
              <Input
                type="text"
                placeholder="Nome"
                icon={User}
                value={name}
                onChange={(e) => setName(e.target.value)}
                required
              />
              <Input
                type="text"
                placeholder="CPF"
                icon={CreditCard}
                value={cpf}
                onChange={handleCpfChange}
                required
              />
              <Input
                type="email"
                placeholder="Email"
                icon={Mail}
                value={registerEmail}
                onChange={(e) => setRegisterEmail(e.target.value)}
                required
              />
              <Input
                type="password"
                placeholder="Senha"
                icon={Lock}
                value={registerPassword}
                onChange={(e) => setRegisterPassword(e.target.value)}
                required
              />
              <Input
                type="password"
                placeholder="Confirmar senha"
                icon={Lock}
                value={confirmPassword}
                onChange={(e) => setConfirmPassword(e.target.value)}
                required
              />

              <button type="submit" className="submit-btn">
                Criar conta
              </button>
            </form>
          )}
        </div>
      </div>

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

export default Login;