const API_BASE_URL = 'http://localhost:8080';

export const apiFetch = async (endpoint, options = {}) => {
    const token = localStorage.getItem('Token');

    const response = await fetch(`${API_BASE_URL}${endpoint}`, {
        ...options,
        headers: {
            'Content-Type': 'application/json',
            ...(token && { Authorization: `Bearer ${token}` }),
            ...options.headers,
        },
    });

    if (!response.ok) {
        const error = await response.json();
        throw error;
    }

    return response.json();
};
