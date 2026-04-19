import { useState, useEffect } from 'react';
// Zwróć uwagę na ścieżkę do api. Jeśli wklejasz to do Dashboards.tsx, zmień na: import api from '../services/api';
import api from './services/api'; 

function App() {
  const [tasks, setTasks] = useState<any[]>([]);
  const [newTaskName, setNewTaskName] = useState('');

  // Pobieranie zadań przy starcie
  useEffect(() => {
    fetchTasks();
  }, []);

  const fetchTasks = async () => {
    try {
      // Wywołujemy nasz backend!
      const response = await api.get('/api/tasks');
      setTasks(response.data);
    } catch (error) {
      console.error("Błąd pobierania:", error);
    }
  };

  const addTask = async () => {
    if (!newTaskName) return;
    try {
      // Wysyłamy nowe zadanie do bazy danych
      await api.post('/api/tasks', { name: newTaskName });
      setNewTaskName(''); // Czyścimy pole
      fetchTasks(); // Odświeżamy listę!
    } catch (error) {
      console.error("Błąd dodawania:", error);
    }
  };

  return (
    <div style={{ padding: '20px', fontFamily: 'sans-serif' }}>
      <h2>📋 Moje Zadania w Chmurze</h2>
      
      {/* SEKCJA FORMULARZA, KTÓREJ BRAKOWAŁO */}
      <div style={{ marginBottom: '20px' }}>
        <input 
          value={newTaskName} 
          onChange={(e) => setNewTaskName(e.target.value)} 
          placeholder="Wpisz nowe zadanie..." 
          style={{ padding: '8px', marginRight: '10px', width: '250px' }}
        />
        <button onClick={addTask} style={{ padding: '8px 15px', cursor: 'pointer' }}>
          Dodaj zadanie dla jego booooooooooo tak trzeba 
        </button>
      </div>

      {/* LISTA ZADAŃ */}
      <ul style={{ listStyleType: 'none', padding: 0 }}>
        {tasks.map((task: any) => (
          <li key={task.id} style={{ padding: '10px', borderBottom: '1px solid #ccc' }}>
            {task.isCompleted ? "✅" : "⏳"} <strong>{task.name}</strong>
          </li>
        ))}
      </ul>
    </div>
  );
}

export default App;