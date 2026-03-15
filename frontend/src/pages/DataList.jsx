import React, { useEffect, useState } from 'react';
import axios from 'axios';

const DataList = () => {
  const [data, setData] = useState([]);

  useEffect(() => {
    axios.get(`${import.meta.env.VITE_API_URL}/data`)
      .then(response => setData(response.data))
      .catch(error => console.error('Error fetching data', error));
  }, []);

  return (
    <div>
      <h1>Lista Danych</h1>
      <ul>
        {data.map(item => (
          <li key={item.id}>{item.name}</li>
        ))}
      </ul>
    </div>
  );
};

export default DataList;