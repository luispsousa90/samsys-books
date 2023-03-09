import { BrowserRouter as Router, Routes, Route } from 'react-router-dom';
// Pages
import HomePage from './pages/HomePage';
import AddBookPage from './pages/AddBookPage';
import EditBookPage from './pages/EditBookPage';

export default function App() {
  return (
    <Router>
      <Routes>
        <Route path='/' element={<HomePage />} />
        <Route path='/book/add' element={<AddBookPage />} />
        <Route path='/book/edit/:id' element={<EditBookPage />} />
      </Routes>
    </Router>
  );
}
