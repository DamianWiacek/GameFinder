import api from '../../api/GameFinder'
import { useEffect, useState } from "react";
import Dropdown from 'react-bootstrap/Dropdown';
import DropdownButton from 'react-bootstrap/DropdownButton';
import Stack from 'react-bootstrap/Stack';
import Button from 'react-bootstrap/Button';
import Card from 'react-bootstrap/Card';
import { useNavigate } from "react-router-dom";
import Form from 'react-bootstrap/Form';
import { ToastContainer, toast } from 'react-toastify';
import 'react-toastify/dist/ReactToastify.css';
import {getSportName} from './Helpers/getSportName';


function GameComponent() {
  const navigate = useNavigate();
  const [games, setGames] = useState([]);
  const paths = {
    '1': require('../../Images/1.jpg'),
    '2': require('../../Images/2.jpg'),
    '3': require('../../Images/3.jpg'),
    '4': require('../../Images/4.jpg'),
  }
  const [addresses, setAddresses] = useState([]);
  const [gameDetails, setgameDetails] = useState([]);
  const [selectedCity, setSelectedCity] = useState(null);
  const [selectedGameType, setSelectedGameType] = useState(null);
  const [searchQuery, setSearchQuery] = useState(null);
  const [selectedGameTypeName, setSelectedGameTypeName] = useState("Select Sport");
  const token = JSON.parse(localStorage.getItem('token'));

  const handleGameTypeSelect = (eventKey) => {
    const sportId = parseInt(eventKey); // Convert eventKey to an integer
    const sportName = getSportName(sportId); // Get the sport name based on the selected event key
    setSelectedGameType(sportId);
    setSelectedGameTypeName(sportName); // Update the selected sport name
    Load(selectedCity, String(sportId));
  };
  
  async function FindAddress(courtIdToFind) {
    const result = await api.get("/GetAddress"
      , { params: { courtId: courtIdToFind } });
    return result.data
  }
  async function GetGameDetails(gameDetailsToFind) {
    const result = await api.get("/GetAllGameUsers"
      , { params: { gameId: gameDetailsToFind } });
    return result.data
  }
  async function JoinToGame(gameId) {
    if (token == null) {  
      navigate("/login");
      throw new Error('Token is null');
    }

    try {
      const response = await api.post(
        '/AddUserToGame',
        {
          newGameDetailsDto: {
            userId: JSON.parse(localStorage.getItem('userId')),
            gameId: gameId
          }
        },
        {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
          }
        }
      );
      // Update the gameDetails state with the latest data
      const updatedGameDetails = await GetGameDetails(gameId);
      setgameDetails((prevGameDetails) => {
        const updatedDetails = [...prevGameDetails];
        const gameIndex = games.findIndex((game) => game.gameId === gameId);
        updatedDetails[gameIndex] = updatedGameDetails;
        return updatedDetails;
      });
      toast.success("Dołączyłeś do gry")
    } catch (error) {
      console.log(error);
    }
  }

  async function LeaveGame(gameId) {
    try {
      const response = await api.delete(
        '/DeleteUserFromGame',
        {
          headers: {
            'Content-Type': 'application/json',
            'Authorization': `Bearer ${token}`
          },
          data: {
            gameId: gameId,
            userId: JSON.parse(localStorage.getItem('userId'))
          }
        }
      );
      // Update the gameDetails state with the latest data
      const updatedGameDetails = await GetGameDetails(gameId);
      setgameDetails((prevGameDetails) => {
        const updatedDetails = [...prevGameDetails];
        const gameIndex = games.findIndex((game) => game.gameId === gameId);
        updatedDetails[gameIndex] = updatedGameDetails;
        return updatedDetails;
      });
      toast.success("Opuściłeś grę")
    } catch (error) {
      console.log(error);
    }
  }

  useEffect(() => {
    (async () => {
      await Load();
    })();
  }, [searchQuery]);

  async function Load(city, gameType) {
    let result;
    if (searchQuery) {
      result = await api.get("/GetAllGamesFromQuery", { params: { search: searchQuery } });
      console.log('elo')
    }
    else {
      result = await api.get("/GetAllGames");
    }
    let filteredGames = result.data;
    setSelectedCity(city)
    setSelectedGameType(gameType)
    const addressesRes = await Promise.all(filteredGames.map(game => FindAddress(game.courtId)));
    setAddresses(addressesRes);

    if (city) {
      filteredGames = filteredGames.filter((game, index) => {
        return addressesRes[index] && addressesRes[index].city === city;
      });
    }

    if (gameType) {
      filteredGames = filteredGames.filter(game => String(game.sportId) === gameType);
    }

    const gameDetailsRes = await Promise.all(filteredGames.map(game => GetGameDetails(game.gameId)));
    const addressesRes2 = await Promise.all(filteredGames.map(game => FindAddress(game.courtId)));
    setGames(filteredGames);
    setAddresses(addressesRes2);
    setgameDetails(gameDetailsRes);
  }
  return (
    <div>
      <h1 className='m-2'>Upcoming Games</h1>
      <Form className="d-flex ">
        <Form.Control
          type="search"
          placeholder="Search address"
          className="m-2"
          aria-label="Search"
          onChange={(event) => setSearchQuery(event.target.value)}
        />
      </Form>
      <Stack direction="horizontal" gap={5} className='justify-content-center mb-2'>
        <DropdownButton
          onSelect={(eventKey) => Load(eventKey, selectedGameType)}
          id="dropdown-city"
          title={selectedCity ? selectedCity : "Select City"}
        >
          <Dropdown.Item eventKey={null}>All</Dropdown.Item>
          <Dropdown.Item eventKey="Katowice">Katowice</Dropdown.Item>
          <Dropdown.Item eventKey="Chorzów">Chorzów</Dropdown.Item>
          <Dropdown.Item eventKey="Ruda Śląska">Ruda Śląska</Dropdown.Item>
        </DropdownButton>

        <DropdownButton
          onSelect={handleGameTypeSelect}
          id="dropdown-game-type"
          title={selectedGameTypeName}
        >
          <Dropdown.Item eventKey={null}>All</Dropdown.Item>
          <Dropdown.Item eventKey={1}>Piłka Nożna</Dropdown.Item>
          <Dropdown.Item eventKey={2}>Koszykówka</Dropdown.Item>
          <Dropdown.Item eventKey={3}>Siatkowka</Dropdown.Item>
          <Dropdown.Item eventKey={4}>Tenis</Dropdown.Item>
        </DropdownButton>
      </Stack>
      <div>
        {games.map((game, index) => {
          const isUserParticipating = gameDetails[index] && gameDetails[index].some(user => user.userId === JSON.parse(localStorage.getItem('userId')));

          return (
            <Card className="text-center mb-2">
              <Card.Header>{game.start.slice(0, 10)}</Card.Header>
              <Card.Body className="d-flex flex-row justify-content-between align-items-center">
                <Card.Text ><img src={paths[game.sportId]} alt="sport logo" width="150" height="150" ></img></Card.Text>
                <div className='d-flex flex-column justify-content-around'>
                  <Card.Header>{game.start.slice(10, 16).replace('T', ' ')} - {game.predictedEnd.slice(10, 16).replace('T', ' ')} </Card.Header>
                  <Card.Text>Users participating: {gameDetails[index] && `${gameDetails[index].length}`} </Card.Text>
                  {isUserParticipating ? (
                    <Button onClick={(e) => { LeaveGame(game.gameId) }} variant="danger">
                      Leave
                    </Button>
                  ) : (
                    <Button onClick={(e) => { JoinToGame(game.gameId) }} variant="primary">
                      Join
                    </Button>
                  )}
                  <ToastContainer />
                </div>
                <div style={{ width: '150px' }}>
                    <Card.Text>
                      {addresses[index] && `${addresses[index].city}`}
                    </Card.Text>
                    <Card.Text>
                      {addresses[index] && `${addresses[index].postalCode}`}
                    </Card.Text>
                    <Card.Text>
                      {addresses[index] && `${addresses[index].street}`}
                    </Card.Text>
                </div>
              </Card.Body>
            </Card>
          );
        })}
      </div>
    </div>
    
  );
}

export default GameComponent;
