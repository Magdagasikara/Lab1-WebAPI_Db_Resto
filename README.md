# Lab1-WebAPI_Db_Resto

Initial Db-plan:  
![text](https://github.com/Magdagasikara/Lab1-WebAPI_Db_Resto/blob/master/ER.png)
Changes compared with the scheme: "Table per hour" has become "TableBooking" and I didn't have time yet to implement Meal categories and ingredients as they weren't part of our assignment.

  
___
**CUSTOMER**  
___

**POST** - /api/Customer/**AddCustomer**  
request body:  
```
{  
  "name": "Aldor B",  
  "email": "aldor@b.c",  
  "phoneNumber": "07666"  
}
```
  
**DELETE** - /api/Customer/**DeleteCustomer**  
request body:  
```
{  
  "email": "aldor@b.c"  
}  
```
If wrong email: 404 with response body "No customer with aldor@b.c"  

**GET** - /api/Customer/**GetAllCustomers**  
response ex:  
```
[  
  {  
    "name": "Magda",  
    "email": "magda@m.m",  
    "phoneNumber": "076"  
  },  
  {  
    "name": "Aldor B",  
    "email": "aldor@b.c",  
    "phoneNumber": "07666"  
  }  
]  
```

**POST** - /api/Customer/**GetCustomerByEmail**  
request body:  
```
{  
  "email": "aldor@b.c"  
}  
```
If wrong email: 404 with response body "No customer with aldor@b.c"  

**PATCH** - /api/Customer/**UpdateCustomer**  
request body:  
```
{  
  "email": "magda@m.m",  
  "name": "Magda",  
  "updatedEmail": "magda@kubien.m",  
  "phoneNumber": "076"  
}  
```
or enough:  
```
{  
  "email": "magda@kubien.m",  
  "name": "Magda KKK",  
  "phoneNumber": "076"  
}  
```

___
**TABLES**  
___

**POST** - /api/Tables/**AddTable**  
request body:  
```
{  
  "tableNumber": 5,  
  "amountOfPlaces": 7  
}  
```

**DELETE** - /api/Tables/**DeleteTable**  
request body:  
```
{  
  "tableNumber": 5  
}  
```
if table doesnt exist: 404 "No table with 5" // a nicer message would be good of course  

**GET** - /api/Tables/**GetALlTables**  
response body:  
```
[  
  {  
    "tableNumber": 1,  
    "amountOfPlaces": 4  
  },  
  {  
    "tableNumber": 2,  
    "amountOfPlaces": 2  
  },  
  {  
    "tableNumber": 3,  
    "amountOfPlaces": 4  
  },  
  {  
    "tableNumber": 4,  
    "amountOfPlaces": 2  
  },  
  {  
    "tableNumber": 5,  
    "amountOfPlaces": 7  
  }  
]  
```

**GET** - /api/Tables/**GetFreeTables**  
parameters:  
time, ex: 2024-09-01T15:29:52.9541091  
reservationHours, ex: 2  
reponse body:  
```
[  
  {  
    "tableNumber": 4,  
    "amountOfPlaces": 2  
  }  
]  
```

**GET** - /api/Tables/**GetFreePlaces**  
parameters:  
time, ex: 2024-09-01T15:29:52.9541091  
reservationHours, ex: 2  
reponse body: 2  

**POST** - /api/Tables/**GetTableByTableNr**  
request body:  
```
{  
  "tableNumber": 3  
}  
```
response body:  
```
{  
  "tableNumber": 3,  
  "amountOfPlaces": 4  
}  
```
if table doesnt exist: 404 "No table with 5"   

**PATCH** - /api/Tables/**UpdateTable**  
request body:  
```
{  
  "tableNumber": 3,  
  "amountOfPlaces": 10,  
  "updatedTableNumber": 3  
}  
```
or only  
```
{  
  "tableNumber": 3,  
  "amountOfPlaces": 11  
}  
```

___
**BOOKINGS**  
___

**POST** - /api/Bookings/**AddBooking**  
request body:  
```
{  
  "timeStamp": "2024-09-01T15:44:42.649Z",  
  "amountOfGuests": 3,  
  "reservationStart": "2024-09-01T17:44:42.649Z",  
  "reservationDurationInHours": 1,  
  "email": "magda@kubien.m"  
}  
```
if wrong email: 404 "No customer with magda@m.m" 

**DELETE** - /api/Bookings/**DeleteBooking/{bookingNumber}**  
parameter bookingNumber ex 3020240903044317

**GET** - /api/Bookings/**GetAllBookings**  
response body:  
```
[  
{  
    "bookingNumber": "1020240903044235",  
    "email": "magda@m.m",  
    "timeStamp": "2024-09-03T16:42:35.7138329",  
    "amountOfGuests": 5,  
    "reservationStart": "2024-09-03T19:42:17.026",  
    "reservationEnd": "2024-09-03T21:42:17.026",  
    "tables": [  
      {  
        "tableNumber": 1,  
        "amountOfPlaces": 4  
      },  
      {  
        "tableNumber": 2,  
        "amountOfPlaces": 2  
      }  
    ]  
  },  
  {  
    "bookingNumber": "3020240903044250",  
    "email": "aldor@a.a",  
    "timeStamp": "2024-09-03T16:42:50.5474407",  
    "amountOfGuests": 5,  
    "reservationStart": "2024-09-03T19:42:17.026",  
    "reservationEnd": "2024-09-03T21:42:17.026",  
    "tables": [  
      {  
        "tableNumber": 3,  
        "amountOfPlaces": 4  
      },  
      {  
        "tableNumber": 4,  
        "amountOfPlaces": 2  
      }  
    ]  
  },  
  {  
    "bookingNumber": "3020240903044317",  
    "email": "aldor@a.a",  
    "timeStamp": "2024-09-03T16:43:17.3772856",  
    "amountOfGuests": 2,  
    "reservationStart": "2024-09-03T21:45:19.026",  
    "reservationEnd": "2024-09-03T22:45:19.026",  
    "tables": [  
      {  
        "tableNumber": 2,  
        "amountOfPlaces": 2  
      }  
    ]  
  }  
]    
```

**GET** - /api/Bookings/**GetBooking/{bookingNumber}**    
parameter bookingNumber ex 1020240903080136  
response body:  
```
{  
  "bookingNumber": "1020240903080136",  
  "email": "magda@m.m",  
  "timeStamp": "2024-09-03T20:01:36.3464983",  
  "amountOfGuests": 7,  
  "reservationStart": "2024-09-30T18:01:16.564",  
  "reservationEnd": "2024-09-30T20:01:16.564",  
  "tables": [  
    {  
      "tableNumber": 1,  
      "amountOfPlaces": 4  
    },  
    {  
      "tableNumber": 3,  
      "amountOfPlaces": 4  
    }  
  ]  
}  
```

**GET** - /api/Bookings/**GetBookingWithoutTables/{bookingNumber}**    
parameter bookingNumber ex 1020240903080136  
response body:  
```
{  
  "bookingNumber": "1020240903080136",  
  "email": "magda@m.m",  
  "timeStamp": "2024-09-03T20:01:36.3464983",  
  "amountOfGuests": 7,  
  "reservationStart": "2024-09-30T18:01:16.564",  
  "reservationEnd": "2024-09-30T20:01:16.564"  
}  
```

___
**MEALS**  
___

**POST** - /api/Meals/**AddMeal**  
request body:  
```
{  
  "name": "Bigos",  
  "description": "Polsk nationalrätt!",  
  "isAvailable": false,  
  "price": 1000  
}  
```

**DELETE** - /api/Meals/**DeleteMeal/{mealId}**  
parameter mealId ex 1  

**GET** - /api/Meals/**GetAllMeals**
response body:  
```
[  
 {  
    "name": "Bigos",  
    "description": "Polsk nationalrätt!",  
    "isAvailable": false,  
    "price": 1000  
  }  
]  
```

**GET** - /api/Meals/**GetMealById/{mealId}**  
parameter mealId ex 3  
response body:  
```
{  
  "name": "Bigos",   
  "description": "Polsk nationalrätt!",  
  "isAvailable": true,  
  "price": 666  
}  
```

**PATCH** - /api/Meals/**UpdateMeal**  
```
{  
  "id": 3,  
  "name": "Bigos",   
  "description": "Polsk nationalrätt fast inte den godaste - eller?",  
  "isAvailable": false,  
  "price": 999  
}  
```
// need to update to get 404 when requesting a non-existant Id  


todo:
- only half-hours possible in booking system  
- default checking for available tables NOW  
- ViewModels - other names to be shown?  
- status codes, status codes, status codes...

kolla igen  
- IActionResult vs ActionResult  

egentligen varje add/delete/update booking borde uppdatera tabell med antalet lediga platser per halvtimme som ligger tillgänglig i frontend
