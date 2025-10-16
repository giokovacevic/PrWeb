import { useEffect, useState } from 'react'
import './App.css'
import { API_URL } from './utils/Config';
import { Navigate, Route, BrowserRouter as Router, Routes} from 'react-router-dom';
import Login from './pages/auth/Login';
import Register from './pages/auth/Register';
import { AuthProvider } from './contexts/AuthContext';
import PlayerQuizes from './pages/user/player/PlayerQuizes';
import AdminEdit from './pages/user/admin/AdminEdit';
import ProtectedRoute from './components/auth/ProtectedRoute';



function App() {

  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route path="/" element={<Login/>}></Route>
          <Route path="/auth/login" element={<Login/>}></Route>
          <Route path="/auth/register" element={<Register/>}></Route>
          
          <Route path="/player" element={<Navigate to="/player/quizes"/>}></Route>
          <Route path="/admin" element={<Navigate to="/admin/edit"/>}></Route>

          <Route path="/player/quizes" element={<ProtectedRoute requiredRole='player'><PlayerQuizes /></ProtectedRoute>}></Route>

          <Route path="/admin/edit" element={<ProtectedRoute requiredRole='admin'><AdminEdit /></ProtectedRoute>}></Route>
        </Routes>
      </Router>
    </AuthProvider>
  )
}

export default App

/*
PLAYER ROUTES:
--------------
View Quizes -> onClick -> do quiz(id)
do quiz {id} NOT IN MENU
view results
show my results
global list

ADMIN ROUTES:
-------------
Categorize quizes
all players results
create new quiz

*/