# BlueHarvest API

## Overview

The **BlueHarvest API** is a backend service that allows existing customers to open a **current account** and retrieve customer information. If an initial deposit is provided, a transaction is automatically recorded.  

This projct is built using **C# and ASP.NET Core**.

## Features

- **Open a Current Account:**  
  - Accepts `customerID` and `initialCredit` as input.
  - Creates a new **current account** for the customer.
  - Automatically processes an initial transaction if `initialCredit` is greater than `0`.

- **Retrieve Customer Information:**  
  - Fetches details including **Name, Surname, Accounts, and Transactions**.

- **Create Dummy Users:**  
  - A helper endpoint to populate the system with test customers.

## Technologies Used

- **C# (.NET Core)**
- **ASP.NET Core Web API**
- **Dependency Injection**
- **In-Memory Data Storage**

## Endpoints

### 1. Open a Current Account
- **URL:** `POST /api/account`
- **Request Body:**
  ```json
  {
    "customerId": 123,
    "initialDeposit": 1000
  }

### 2. Retrieve Customer Information
- **URL:** GET /api/customer/{customerId}

### 3. Create Dummy Users
- **URL:** POST /api/customer
- for testing purposes run the create dummy users api and use customerId from 1-5

**Setup & Running Locally**

**Prerequisites**
.NET SDK (latest version)
Git

**Clone the Repository**
git clone https://github.com/aamena-bohra/BlueHarvestAPI-Backend.git
cd blueharvest-api

**Run the API**
dotnet run

**Test the API**
Use Postman to test endpoints.

**Other features**
In-memory storage.
Added Unit Tests.
