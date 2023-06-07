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
import { useNavigate } from "react-router-dom";

export default function NavbarComponent() {
  const token = JSON.parse(localStorage.getItem('token'))
  const navigate = useNavigate();
   function IsSignedIn(){
    return token != null;
  }
  async function getUserData() {
  try {
    const response = await api.get('/GetUser', {
      headers: {
        Authorization: `Bearer ${token}` // set the token in the request headers
      }
    });

    if (!response.ok) {
      throw new Error('Error retrieving user data');
    }

    const data = await response.json();
    console.log(data);
  } catch (error) {
    console.error(error);
  }
}
  async function Logout(){
    localStorage.clear();
    navigate("login");
  }

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
            {
            token ? (
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