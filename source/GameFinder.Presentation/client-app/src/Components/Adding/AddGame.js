import Button from 'react-bootstrap/Button';
import Form from 'react-bootstrap/Form';
import {useState,useEffect} from "react";
import api from '../../api/GameFinder'
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import { useNavigate } from "react-router-dom";

function Addgame() {
    const [sportId, setSportId] = useState('');
    const [start, setStart] = useState('');
    const [predictedEnd, setPredictedEnd] = useState('');
    const [courtId, setCourtId] = useState('');
    const [courts, setCourts] = useState([])
    const token = JSON.parse(localStorage.getItem('token'));
    const navigate = useNavigate();

    const handleSubmit = async (e) => {
      e.preventDefault();
        try{
            const response = await api.post('/AddGame',
            {
                newGameDto: {
                    sportId: sportId,
                    start: start,
                    predictedEnd: predictedEnd,
                    courtId: courtId,
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
    useEffect(() => {
      (async () => {
        await Load();
      })();
    }, []);
  
    async function Load() {
      const result = await api.get("/GetAllCourts");
      setCourts(result.data)
    }

  return (
    <div className="m-3">
    <Form onSubmit={handleSubmit} >
      <Form.Group className="mb-3" controlId="start">
        <Form.Label>Start time</Form.Label>
        <Form.Control value={start} onChange={(e) => setStart(e.target.value)} type="datetime-local" placeholder="Start" />
      </Form.Group>
      <Form.Group className="mb-3" controlId="predictedEnd">
        <Form.Label>Predicted End</Form.Label>
        <Form.Control value={predictedEnd} onChange={(e) => setPredictedEnd(e.target.value)} type="datetime-local" placeholder="End" />
      </Form.Group>
      <Form.Group>
            <Form.Label>Select Court</Form.Label>
      <div className='mb-3'>
      <Form.Select onChange={(e) => setCourtId(e.target.value)}>
        {courts.map((court) => {
          return (  
          <option value={court.courtId}>{court.address.city} ul.{court.address.street}</option>
          );
        })}
        </Form.Select>
      </div>
      
      </Form.Group>
      <Form.Group className="mb-3" controlId="sport">
        <Form.Label>Select Sport</Form.Label>
        <Form.Select onChange={(e) => setSportId(e.target.value)}>
          <option value="1">Soccer</option>
          <option value="2">Basketball</option>
          <option value="3">Volleyball</option>
          <option value="4">Tennis</option>
        </Form.Select>
      </Form.Group>    
      <Button variant="primary" type="submit">
        Submit
      </Button>
      <ToastContainer />
    </Form>
    </div>
  );
}

export default Addgame;