# Simple Booking System - Backend

## Overview
This is the backend service for the Simple Booking System, built using ASP.NET Core and C#. It handles resource management, booking processes, and validation.

## Technologies Used
- ASP.NET Core
- C#
- Entity Framework Core (Code First)
- SQL Server
- Restful APIs

## Getting Started

### Prerequisites
- .NET SDK
- SQL Server

### Installation
1. Create New Database called `BookingSystem`
2. Open Package Manager Console and write command `Update-Database`

### API Endpoints
- `GET /api/resources` - Get all resources
- `POST /api/resources/{resourceId}/bookings` - Create a new booking

### Booking Process
- Validates resource availability by checking the amount of quatity that is already booked before.
- Confirms booking and returns a success message.
- If booking quantity exceeds the resource quantity, returns an bad request message.
- Mocks email sending by writing to the console.
