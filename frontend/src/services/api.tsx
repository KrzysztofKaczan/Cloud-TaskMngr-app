import axios from 'axios';

const api = axios.create({
  // Wpisujemy port na sztywno, żeby ominąć problemy ze zmiennymi w Dockerze
  baseURL: 'http://localhost:8081',
});

export default api;