import axios from 'axios'

const api = axios.create({
	// Wpisujemy port na sztywno, żeby ominąć problemy ze zmiennymi w Dockerze
	baseURL: 'https://cloud-task-mgnmt-fzebftfqcxcuc3d9.swedencentral-01.azurewebsites.net',
})

export default api
