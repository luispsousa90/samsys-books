import { ReactNode } from 'react';
import { Container } from '@mui/material';
// Components
import Navbar from '../components/Navbar';

interface Props {
  children?: ReactNode;
  // any props that come into the component
}

export default function MainLayout({ children }: Props) {
  return (
    <>
      <Navbar />
      <Container maxWidth='lg' sx={{ mt: 4, mb: 4 }}>
        {children}
      </Container>
    </>
  );
}
