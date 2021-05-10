# Documentation

## Introduktion

Välkommen till SpacePark API. Det är ett API för att kunna hantera parkeringar i SpacePark program. 


Detta API använder [SWAPI](https://swapi.dev/) för att evalidera att personerna och deras farkoster är ifrån The Star Wars Universe.

## Bas URL


Bas URL:en är basen för hela APIET och utan den fungerar inte APIET.

```
https://localhost:5001/api/
```


## Autentisering 

Det är ett stängt API, kontakta Kristian för API-nyckel.


## User Endpoints
```  
GET Methods:

/Parking/SpacePorts
    Listar alla SpacePorts som finns
    
/Parking/{name}/Current
    Skriver ut nuvarande parkering för personen
    
    Exempel:
    /Parking/Boba_Fett/Current
        
    
/Parking/{name}/All
    Listar alla parkeringar som personen har gjort
    
    Exempel:
    /Parking/R2-D2/All
    
POST Methods:

/Parking/Register
    Registrerar en parkering
    
PUT Methods:

/Parking/{id}
    Betalar parkering
    
    Exempel:
    /Parking/5
    

```
## Exempel på begäran
```
https://localhost:5001/api/Parking/register
``` 

Det här är ett exempel på vad som kan skickas med i HTTP request bodyn

    "PersonName": "Boba Fett",
    "StarShip": "X-Wing",
    "arrivalTime": "2021-03-29T13:37:00",
    "SpacePortID": 3


## Exempel på svar:
```
HTTP/1.1 201 Created
Connection: close
Date: Fri, 07 May 2021 10:56:10 GMT
Content-Type: application/json; charset=utf-8
Server: Kestrel
Content-Length: 184
Location: https://localhost:5001/api/Parking/14

{
  "id": 14,
  "personName": "Boba Fett",
  "starShip": "X-Wing",
  "arrivalTime": "2021-03-29T13:37:00",
  "paid": false,
  "spacePort": {
    "id": 3,
    "name": "Pretty Cool SpacePort",
    "totalCapacity": 5,
    "parking": []
  }
}
```


## Admin Specifika Endpoints
``` 
GET Methods:

/SpacePorts
    Listar alla existerande SpacePorts
    
/Parking/{id}
    Får ut en specifik SpacePort
    
    Exempel:
    /Parking/3
    
    
    
PUT Methods:

/SpacePorts/{id}
    Uppdaterar vad man vill i SpacePort objektet
    
    Exempel:
    /SpacePorts/2
    
POST Methods:

/SpacePorts
    Skapar en ny SpacePort
    
/SpacePorts/{id}
    Tar bort en SpacePort
    
    Exempel:
    /SpacePorts/4
    
``` 


