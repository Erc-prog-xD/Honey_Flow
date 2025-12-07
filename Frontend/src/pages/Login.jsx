import React, { useState } from 'react';
import { Mail, Lock, User, CreditCard, Phone } from 'lucide-react';
import { Link } from 'react-router-dom';
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
  const API_BASE_URL = 'http://localhost:8080';
  const [isLogin, setIsLogin] = useState(true);
  const [toast, setToast] = useState(null);
  const [loading, setLoading] = useState(false); // Novo estado de carregamento

  // Login state
  const [loginEmail, setLoginEmail] = useState('');
  const [loginPassword, setLoginPassword] = useState('');

  // Register state
  const [name, setName] = useState('');
  const [cpf, setCpf] = useState('');
  const [registerCelular, setCelular] = useState('');
  const [registerEmail, setRegisterEmail] = useState('');
  const [registerPassword, setRegisterPassword] = useState('');
  const [confirmPassword, setConfirmPassword] = useState('');
  const [registerLoading, setRegisterLoading] = useState(false); // Novo estado de carregamento para o Register

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

  // --- Função de Login Atualizada ---
  const handleLoginSubmit = async (e) => {
    e.preventDefault();
    setLoading(true);

    const loginData = {
      email: loginEmail,
      password: loginPassword,
    };

    try {
      const response = await fetch(`${API_BASE_URL}/api/Auth/Login`, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(loginData),
      });

      if (response.ok) {
        // Assume que a API retorna um JSON com o token ou dados do usuário
        const data = await response.json();
        console.log('Login successful', data);
        
        // Exemplo: Salvar token no Local Storage
        // localStorage.setItem('authToken', data.token); 
        
        showToast('Login realizado com sucesso!', 'success');
        // Redirecionar para o dashboard, por exemplo
        // history.push('/dashboard'); 

      } else {
        // Tratar erros (ex: credenciais inválidas)
        const errorData = await response.json();
        console.error('Login failed', errorData);

        // Exibir mensagem de erro da API ou uma mensagem genérica
        showToast(errorData.message || 'Credenciais inválidas ou erro no servidor.', 'error');
      }
    } catch (error) {
      console.error('Network or CORS error:', error);
      showToast('Erro de conexão. Verifique o servidor da API.', 'error');
    } finally {
      setLoading(false);
    }
  };

 // --- Função de Registro Atualizada ---
  const handleRegisterSubmit = async (e) => {
    e.preventDefault();
    setRegisterLoading(true);

    if (registerPassword !== confirmPassword) {
      showToast('As senhas não coincidem.', 'error');
      setRegisterLoading(false);
      return;
    }

    const cpfDigits = cpf.replace(/\D/g, '');
    if (cpfDigits.length !== 11) {
      showToast('CPF inválido. Digite os 11 dígitos.', 'warning');
      setRegisterLoading(false);
      return;
    }

    // Dados que serão enviados para o endpoint de registro (ajuste conforme o DTO da sua API)
    const registerData = {
      Nome: name,
      Cpf: cpfDigits, // Enviar apenas os dígitos do CPF
      Email: registerEmail,
      Celular: registerCelular,
      Password: registerPassword,
      // Se sua API exigir confirmação de senha no DTO, adicione aqui
      // confirmPassword: confirmPassword 
    };
    
    // Assumindo a rota /api/Auth/Register para o cadastro
    const REGISTER_URL = `${API_BASE_URL}/api/Auth/Register`; 

    try {
      const response = await fetch(REGISTER_URL, {
        method: 'POST',
        headers: {
          'Content-Type': 'application/json',
        },
        body: JSON.stringify(registerData),
      });

      if (response.ok) {
        // Assume que a API retorna sucesso no cadastro
        const data = await response.json();
        console.log('Registration successful', data);
        
        showToast('Cadastro realizado com sucesso! Faça login para continuar.', 'success');
        
        // Opcional: Mudar para a aba de login após o sucesso
        setIsLogin(true); 

      } else {
        // Tratar erros de validação (ex: email já em uso)
        const errorData = await response.json();
        console.error('Registration failed', errorData);
        showToast(errorData.message || 'Falha no cadastro. Verifique os dados.', 'error');
      }
    } catch (error) {
      console.error('Network or CORS error:', error);
      showToast('Erro de conexão. Verifique o servidor da API.', 'error');
    } finally {
      setRegisterLoading(false);
    }
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

              <button type="submit" className="submit-btn" disabled={loading}>
                {loading ? 'Entrando...' : 'Entrar'}
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
                type="celular"
                placeholder="Celular"
                icon={Phone}
                value={registerCelular}
                onChange={(e) => setCelular(e.target.value)}
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

             <button type="submit" className="submit-btn" disabled={registerLoading}>
                {registerLoading ? 'Criando...' : 'Criar conta'}
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