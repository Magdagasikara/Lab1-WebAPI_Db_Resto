# Lab1-WebAPI_Db_Resto

Initial Db-plan:  
![text](https://github.com/Magdagasikara/Lab1-WebAPI_Db_Resto/blob/master/ER.png)
Changes compared with the scheme: "Table per hour" has become "TableBooking" and I didn't have time yet to implement Meal categories and ingredients as they weren't part of our assignment.

  
CUSTOMER  

POST - /api/Customer/AddCustomer  
request body:  
```
{  
  "name": "Aldor B",  
  "email": "aldor@b.c",  
  "phoneNumber": "07666"  
}
```
  
DELETE - /api/Customer/DeleteCustomer  
request body:  
```
{  
  "email": "aldor@b.c"  
}  
```
If wrong email: 404 with response body "No customer with aldor@b.c"  

GET - /api/Customer/GetAllCustomers  
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

POST - /api/Customer/GetCustomerByEmail  
request body:  
```
{  
  "email": "aldor@b.c"  
}  
```
If wrong email: 404 with response body "No customer with aldor@b.c"  

PATCH - /api/Customer/UpdateCustomer  
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

TABLES  

POST - /api/Tables/AddTable  
request body:  
```
{  
  "tableNumber": 5,  
  "amountOfPlaces": 7  
}  
```
// wrong error handling when the same number again! :/  

DELETE - /api/Tables/DeleteTable  
request body:  
```
{  
  "tableNumber": 5  
}  
```
if table doesnt exist: 404 "No table with 5" // a nicer message would be good of course  

GET - /api/Tables/GetALlTables  
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

GET - /api/Tables/GetFreeTables  
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

GET - /api/Tables/GetFreePlaces  
parameters:  
time, ex: 2024-09-01T15:29:52.9541091  
reservationHours, ex: 2  
reponse body: 2  

POST - /api/Tables/GetTableByTableNr  
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

PATCH - /api/Tables/UpdateTable  
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

BOOKINGS  

POST - /api/Bookings/AddBooking  
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
if wrong email: 500 "No customer with magda@m.m" // should be 404 or personalized error  
// problems now again!!  

GET - /api/Bookings/GetAllBookings  
response body:  
```
[  
  {  
    "bookingNumber": 120240901,  
    "email": "jocke@j.j",  
    "timeStamp": "2024-09-01T14:29:52.9541192",  
    "amountOfGuests": 0,  
    "reservationStart": "2024-09-01T14:29:52.9541091",  
    "reservationEnd": "2024-09-01T16:29:52.9541183",  
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
    "bookingNumber": 220240901,  
    "email": "jocke@j.j",  
    "timeStamp": "2024-09-01T14:29:52.9541375",  
    "amountOfGuests": 6,  
    "reservationStart": "2024-09-01T14:29:52.954136",  
    "reservationEnd": "2024-09-01T16:29:52.9541366",  
    "tables": [  
      {  
        "tableNumber": 3,  
        "amountOfPlaces": 11  
      }  
    ]  
  },  
  {  
    "bookingNumber": 2020240901,  
    "email": "jocke@j.j",  
    "timeStamp": "2024-09-01T16:24:06.476505",  
    "amountOfGuests": 2,  
    "reservationStart": "2024-09-01T14:23:55.017",  
    "reservationEnd": "2024-09-01T15:23:55.017",  
    "tables": [  
      {  
        "tableNumber": 4,  
        "amountOfPlaces": 2  
      }  
    ]  
  },  
  {  
    "bookingNumber": 1020240901,  
    "email": "magda@kubien.m",  
    "timeStamp": "2024-09-01T16:27:03.4756014",  
    "amountOfGuests": 51,  
    "reservationStart": "2024-09-01T14:26:32.373",  
    "reservationEnd": "2024-09-01T15:26:32.373",  
    "tables": []  
  }  
]  
```
// the last one indicates a problem being solved (return "no tables" instead of continuing with an empty list)  


MEALS  

POST - /api/Meals/AddMeal  
request body:  
```
{  
  "name": "Bigos",  
  "description": "Polsk nationalrätt!",  
  "isAvailable": false,  
  "price": 1000  
}  
```

DELETE - /api/Meals/DeleteMeal  
parameter mealId ex 1  

GET - /api/Meals/GetAllMeals
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

GET - /api/Meals/GetMealById  
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

PATCH - /api/Meals/UpdateMeal
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
- add booking return 404 if user not found  
- if no empty tables, dont throw an exception but handle empty list of tables instead  
- if reservation duration==0 ignore directly and return error  
- addtable handle when the same table number  
- ViewModels - other names to be shown?  

kolla igen  
- IActionResult vs ActionResult  

egentligen varje add/delete/update booking borde uppdatera tabell med antalet lediga platser per halvtimme som ligger tillgänglig i frontend
