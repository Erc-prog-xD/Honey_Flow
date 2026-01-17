import React, { useState } from 'react';
import '../assets/css/Menu.css';

const Menu = () => {
  const [showAdd, setShowAdd] = useState(true);

  return (
    <div className="menu-page">
      {/* header removed - modal opens on page load */}

      <main className="content">
        {showAdd && (
          <div className="add-modal" role="dialog" aria-modal="true">
            <div className="add-card">
              <div className="add-header">
                <span>Adicionar</span>
                <button className="close-x" onClick={() => setShowAdd(false)}>×</button>
              </div>
              <div className="add-content">
                <button className="btn-menu btn-yellow" style={{ width: '100%' }}>Colmeia</button>
                <button className="btn-menu btn-orange" style={{ width: '100%' }}>Apiário</button>
              </div>
            </div>
          </div>
        )}

      </main>
    </div>
  );
};

export default Menu;