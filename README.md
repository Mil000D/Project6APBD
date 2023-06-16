# Project 6 : Summary
In this project following steps are needed to complete it succesfully.
1. Create a Rest API app.
2. Prepare an endpoint using EntityFramework Core according to the requirements below: 
* Endpoint responding to HTTP GET request sent to `/api/trips`. 
* Endpoint should return a list of trips in descending order by the column `DateTo`.
* The following is an example of the format of the returned data:
  
  ```
   {
       "Name": "ABC",
       "Description": "Lorem ipsum...",
       "DateFrom": "",
       "DateTo": "",
       "MaxPeople": 20,
       "Countries": [
           {
              "Name": "Poland"
           },
           {
              "Name": "Germany"
           },
       ],
       "Clients": [
           {
              "FirstName": "John",
              "LastName": "Smith"
           },
           {
              "FirstName": "Jake",
              "LastName": "Doe"
           }
       ]      
   }
   ```
3. Prepare an enpoint that allows you to delete customer data.
* Endpoint accepting data sent to an HTTP DELETE request at
`/api/clients/{idClient}`. 
* The endpoint should first check if the customer does not have any assigned. 
tours. If the customer has at least one assigned tour - we return an error and 
the deletion fails. 
4. Prepare a tip to assign the client to the tour. 
* The endpoint should accept HTTP POST requests sent to
`/api/trips/{idTrip}/clients`. 
* Parameters sent in the body of the request should look as follows (the format 
of the data may be different):
   ```
   {
       "FirstName": "John",
       "LastName": "Doe",
       "Email": "doe@wp.pl",
       "Telephone": "543-323-542",
       "Pesel": "0123456789",
       "IdTrip": 10,
       "PaymentDate": "4/20/2021"
   }
   ```

* After accepting the data, we check: 
  * Whether a customer with a given `PESEL` number exists. If not, we add him to the database.
  * Whether the customer is not already enrolled in the said tour - in that case we return an error.
  * Does the trip exist - if not - we return an error.
* `PaymentDate` can have a null value, for those clients who have not yet paid 
for the trip. In addition, the `RegisteredAt` column in the `Client_Trip` table 
should be equal to the current time the request was processed.
5. Remember correct variable/method/class names.
6. Use additional models for the data returned and received by the endpoints – DTO 
(ang. Data Transfer Object). 
7. Remember abour SOLID and DI.
