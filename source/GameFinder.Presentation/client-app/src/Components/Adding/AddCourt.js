import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import {useState} from "react";
import api from '../../api/GameFinder'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useNavigate } from "react-router-dom";

function AddCourt() {
    const [city, setCity] = useState('');
    const [street, setStreet] = useState('');
    const [postalCode, setPostalCode] = useState('');
    const [courtType, setCourtType] = useState('');
    const token = JSON.parse(localStorage.getItem('token'));
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
      e.preventDefault();
        try{
            const response = await api.post('/AddCourt',
            {
                newCourtDto: {
                    city: city,
                    street: street,
                    postalCode: postalCode,
                    courtType: courtType,
                }},
                {
                    headers: {
                   'Content-Type': 'application/json',
                    'Authorization' : `Bearer ${token}`}
                }                     
            );  
            navigate("/");
        }        
        catch(error){
          toast.error("Coś poszło nie tak")
            console.log(error);
        }
    }

  return (
    <div className="m-3">
    <Form onSubmit={handleSubmit} >
      <Form.Group className="mb-3" controlId="city">
        <Form.Label>City</Form.Label>
        <Form.Control value={city} onChange={(e) => setCity(e.target.value)} type="text" placeholder="City" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="postalCode">
        <Form.Label>Postal Code</Form.Label>
        <Form.Control value={postalCode} onChange={(e) => setPostalCode(e.target.value)} type="text" placeholder="Postal Code" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="street">
        <Form.Label>Street</Form.Label>
        <Form.Control value={street} onChange={(e) => setStreet(e.target.value)} type="text" placeholder="Street" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="courtType">
        <Form.Label>Court Type</Form.Label>
        <Form.Control value={courtType} onChange={(e) => setCourtType(e.target.value)} type="text" placeholder="1 or 2" />
      </Form.Group>   
      <Button variant="primary" type="submit">
        Submit
      </Button>
      <ToastContainer />
    </Form>
    </div>
  );
}

export default AddCourt;