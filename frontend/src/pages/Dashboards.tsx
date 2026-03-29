import { useEffect, useState } from 'react'
import axios from 'axios'

interface Task {
	id: number
	title: string
	isCompleted: boolean
}

export default function Dashboard() {
	const [tasks, setTasks] = useState<Task[]>([])
	const [error, setError] = useState<string | null>(null)

	useEffect(() => {
		const fetchTasks = async () => {
			try {
				const response = await axios.get('http://127.0.0.1:8081/api/tasks')
				setTasks(response.data)
			} catch (err: any) {
				// ZADANIE 4.4: Obsługa i walidacja błędów z API
				if (err.response) {
					setError(`Błąd API: ${err.response.status} - ${err.response.data || 'Nieznany błąd'}`)
				} else {
					setError('Brak połączenia z backendem. Sprawdź, czy Docker działa.')
				}
			}
		}
		fetchTasks()
	}, [])

	return (
		<div style={{ padding: '20px', fontFamily: 'sans-serif' }}>
			<h2>📋 Moje Zadania w Chmurze</h2>
			{error && (
				<div style={{ color: 'red', background: '#fee', padding: '10px', borderRadius: '5px' }}>🚨 {error}</div>
			)}

			<ul style={{ listStyleType: 'none', padding: 0 }}>
				{tasks.map(task => (
					<li key={task.id} style={{ padding: '10px', borderBottom: '1px solid #ccc' }}>
						{task.isCompleted ? '✅' : '⏳'} <strong>{task.title}</strong>
					</li>
				))}
			</ul>
		</div>
	)
}
