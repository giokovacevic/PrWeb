import { useEffect, useState } from 'react'
import './App.css'
import { API_URL } from './utils/Config';
import { Navigate, Route, BrowserRouter as Router, Routes} from 'react-router-dom';
import Login from './pages/auth/Login';
import Register from './pages/auth/Register';
import { AuthProvider } from './contexts/AuthContext';
import PlayerQuizzes from './pages/user/player/PlayerQuizzes';
import AdminEdit from './pages/user/admin/AdminEdit';
import ProtectedRoute from './components/auth/ProtectedRoute';
import PlayerRankings from './pages/user/player/PlayerRankings';
import PlayerQuiz from './pages/user/player/PlayerQuiz';
import PlayerResults from './pages/user/player/PlayerResults';
import PlayerQuizWrapper from './pages/user/player/PlayerQuizWrapper';
import AdminResults from './pages/user/admin/AdminResults';
import AdminCreate from './pages/user/admin/AdminCreate';



function App() {

  return (
    <AuthProvider>
      <Router>
        <Routes>
          <Route path="/" element={<Login/>}></Route>
          <Route path="/auth/login" element={<Login/>}></Route>
          <Route path="/auth/register" element={<Register/>}></Route>
          
          <Route path="/player" element={<Navigate to="/player/quizzes"/>}></Route>
          <Route path="/admin" element={<Navigate to="/admin/edit"/>}></Route>

          <Route path="/player/quizzes" element={<ProtectedRoute requiredRole='player'><PlayerQuizzes /></ProtectedRoute>}></Route>
          <Route path="/player/quizzes/:quizId" element={<ProtectedRoute requiredRole='player'><PlayerQuizWrapper /></ProtectedRoute>}></Route>
          <Route path="/player/results" element={<ProtectedRoute requiredRole='player'><PlayerResults /></ProtectedRoute>}></Route>
          <Route path="/player/rankings" element={<ProtectedRoute requiredRole='player'><PlayerRankings /></ProtectedRoute>}></Route>

          <Route path="/admin/edit" element={<ProtectedRoute requiredRole='admin'><AdminEdit /></ProtectedRoute>}></Route>
          <Route path="/admin/results" element={<ProtectedRoute requiredRole='admin'><AdminResults /></ProtectedRoute>}></Route>
          <Route path="/admin/create" element={<ProtectedRoute requiredRole='admin'><AdminCreate /></ProtectedRoute>}></Route>
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