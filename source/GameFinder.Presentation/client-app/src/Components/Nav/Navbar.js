import Button from 'react-bootstrap/Button';
import Container from 'react-bootstrap/Container';
import Form from 'react-bootstrap/Form';
import Nav from 'react-bootstrap/Nav';
import Navbar from 'react-bootstrap/Navbar';
import NavDropdown from 'react-bootstrap/NavDropdown';
import { Link } from 'react-router-dom';
import { useEffect, useState } from "react";
import Cookies from 'js-cookie';
import api from '../../api/GameFinder'

export default function NavbarComponent() {
  const [token, setToken] = useState(null);
   function IsSignedIn(){
    return token != null;
  }
  async function Logout(){
    localStorage.clear();
    setToken(await JSON.parse(localStorage.getItem('token')))
  }
   async function Load() {
    setToken(await JSON.parse(localStorage.getItem('token')))
  }
  useEffect(() => { 
    (async () =>  Load())();
  }, [token]);

  return (
    <Navbar bg="light" expand="lg">
      <Container fluid>
        <Navbar.Brand as={Link} to="#">GameFinder</Navbar.Brand>
        <Navbar.Toggle aria-controls="navbarScroll" />
        <Navbar.Collapse id="navbarScroll">
          <Nav
            className="me-auto my-2 my-lg-0"
            style={{ maxHeight: '100px' }}
            navbarScroll
          >
            <Nav.Link as={Link} to="/">Home</Nav.Link>
            <Nav.Link as={Link} to="/rankings">Rankings</Nav.Link>
            <Nav.Link as={Link} to="/users">Players</Nav.Link>
            <Nav.Link as={Link} to="/addgame">Create game</Nav.Link>
            <Nav.Link as={Link} to="/addcourt">Add Court</Nav.Link>
          </Nav>
          <Nav
            className=""
            style={{ maxHeight: '100px' }}
            navbarScroll
          >
            {IsSignedIn() ? (
              <>
                <Button onClick={Logout} variant="outline-danger">Logout</Button>
              </>
            ) : (
              <>
                <Nav.Link as={Link} to="/register">
                  Register
                </Nav.Link>
                <Nav.Link as={Link} to="/login">
                  Login
                </Nav.Link>
              </>
            )}
          </Nav>
        </Navbar.Collapse>
      </Container>
    </Navbar>
  );
}