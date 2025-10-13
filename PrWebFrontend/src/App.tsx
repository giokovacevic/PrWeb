import { useEffect, useState } from 'react'
import './App.css'
import { API_URL } from './utils/Config';

interface Person{
  id: number;
  name: string;
}

function App() {
  const [data, setData] = useState<Person[]>([]);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try{
      const response = await fetch(`${API_URL}/Person/all`);
      const data = await response.json();
      console.log(data);
      setData(data);
    }catch(error) {
      console.log(error);
    }
  } 

  return (
    <>
      <div>
        {data.map((value) => <div key={value.id}>
          <div>{value.id}</div>
          <div>{value.name}</div>
        </div>)}
      </div>
    </>
  )
}

export default App
