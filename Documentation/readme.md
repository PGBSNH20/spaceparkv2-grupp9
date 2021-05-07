# Documentation


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
    
/Parking/{name}/All
    Listar alla parkeringar som personen har gjort
    
    
POST Methods:

/Parking/Register
    Registrerar en parkering
    
PUT Methods:

/Parking/{id}
    Betalar parkering

```

## Admin Specifika Endpoints
``` 
GET Methods:

/SpacePorts
    Listar alla existerande SpacePorts
    
/Parking/{id}
    Får ut en specifik SpacePort
    
    
PUT Methods:

/SpacePorts/{id}
    Uppdaterar vad man vill i SpacePort objektet
    
    
POST Methods:

/SpacePorts
    Skapar en ny SpacePort
    
/SpacePort/{id}
    Tar bort en SpacePort
``` 


