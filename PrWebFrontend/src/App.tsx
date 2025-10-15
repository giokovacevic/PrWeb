import { useEffect, useState } from 'react'
import './App.css'
import { API_URL } from './utils/Config';
import { Route, BrowserRouter as Router, Routes} from 'react-router-dom';
import LoginLayout from './pages/auth/LoginLayout';
import RegisterLayout from './pages/auth/RegisterLayout';



function App() {

  return (
    <Router>
      <Routes>
        <Route path='/' element={<LoginLayout/>}></Route>
        <Route path='/auth/login' element={<LoginLayout/>}></Route>
        <Route path='/auth/register' element={<RegisterLayout/>}></Route>
      </Routes>
    </Router>
  )
}

export default App
