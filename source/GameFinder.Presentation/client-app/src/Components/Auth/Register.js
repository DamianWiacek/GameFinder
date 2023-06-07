import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import {useState} from "react";
import api from '../../api/GameFinder'
import { useNavigate } from "react-router-dom";
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';

function Register() {
    const navigate = useNavigate();
    const [password, setPwd] = useState('');
    const [email, setEmail] = useState('');
    const [name, setName] = useState('');
    const [surname, setSurname] = useState('');
    const [phone, setPhone] = useState('');
    const [birthdate, setBirthDate] = useState('');
    const [role, setRole] = useState('');
    
    const handleSubmit = async (e) => {
      e.preventDefault();
        try{
            const response = await api.post('/Register',
            {
                newUser: {
                  email: email,
                  password: password,
                  name: name,
                  surname: surname,
                  phone: phone,
                  birthday: birthdate,
                  roleId: role
                }},
                {
                    headers: { 'Content-Type': 'application/json' }
                }                 
            ); 
            navigate("/login");
        }        
        catch(error){
          toast.error("Użytkownik z tym emailem już istnieje lub coś poszło nie tak") 
            console.log(error);
        }
    }

  return (
    <div className="m-3">
    <Form onSubmit={handleSubmit} >
      <Form.Group className="mb-3" controlId="email">
        <Form.Label>Email address</Form.Label>
        <Form.Control value={email} onChange={(e) => setEmail(e.target.value)} type="email" placeholder="Enter email" />
      </Form.Group>

      <Form.Group className="mb-3" controlId="password">
        <Form.Label>Password</Form.Label>
        <Form.Control value={password} onChange={(e) => setPwd(e.target.value)} type="password" placeholder="Password" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="name">
        <Form.Label>Name</Form.Label>
        <Form.Control value={name} onChange={(e) => setName(e.target.value)} type="text" placeholder="Enter name" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="surname">
        <Form.Label>Surname</Form.Label>
        <Form.Control value={surname} onChange={(e) => setSurname(e.target.value)} type="text" placeholder="Enter surname" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="phone">
        <Form.Label>Phone</Form.Label>
        <Form.Control value={phone} onChange={(e) => setPhone(e.target.value)} type="tel" placeholder="Enter phone" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="birthdate">
        <Form.Label>Birthdate</Form.Label>
        <Form.Control value={birthdate} onChange={(e) => setBirthDate(e.target.value)} type="date" placeholder="Enter birthdate" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="role">
        <Form.Label>Role</Form.Label>
        <Form.Control value={role} onChange={(e) => setRole(e.target.value)} type="text" placeholder="Choose 1 or 2" />
      </Form.Group>
      <Button variant="primary" type="submit">
        Submit
      </Button>
      <ToastContainer />
    </Form>
    </div>
  );
}

export default Register;