import { useEffect, useState } from 'react'
import './App.css'
import { API_URL } from './utils/Config';

interface Role{
  id: number;
  name: string;
}
interface User{
  id: number;
  username: string;
  email: string;
  password: string;
  role: Role;
}

function App() {
  const [data, setData] = useState<User[]>([]);

  useEffect(() => {
    loadData();
  }, []);

  const loadData = async () => {
    try{
      const response = await fetch(`${API_URL}/users/all`);
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
          <div>{value.id}&nbsp;{value.username}&nbsp;{value.email}&nbsp;{value.password}&nbsp;{value.role.id}&nbsp;{value.role.name}</div>
        </div>)}
      </div>
    </>
  )
}

export default App
