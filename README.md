# Comp2001

# Micro-Service Authentication System

## Introduction

This repository outlines a micro-service architecture for a Server Web API and Client MVC app, with a specific focus on implementing secure login/logout functionalities using sessions. The documentation provides an overview of the system setup, API endpoints, and communication methods between services. Explore the associated GitHub repository for code implementation and interact with the live-hosted micro-service to see these functionalities in action.

## Features

- Micro-service architecture for a Server Web API and Client MVC app
- Secure login/logout functionalities using sessions
- Well-defined API endpoints for user authentication and session management
- Visual representation of the database model with logical ERD and UML diagrams
- Legal, social, ethical, and professional considerations for information privacy and security

## Design

### Database Diagram

Database Diagram (https://github.com/eimhanguelo/Comp2001/tree/main/APIProject/APIProject/Models))


## Legal, Social, Ethical, and Professional Considerations

The design and implementation address crucial aspects such as information privacy, integrity, security, and data preservation. This includes enforcing HTTPS, proper error handling, and compliance with data protection laws.

## Implementation

### Server API Profiles CURD

Profiles CURD operations are efficiently handled on the server, allowing for the creation, updating, retrieval, and deletion of user profiles.

### Client API Profiles CURD

Corresponding client-side operations mirror the server's functionality, creating a seamless experience for users interacting with their profiles.

## Evaluation

### User Login/Logout

Login View (https://localhost:44326)
Successful Login View (https://localhost:44326/Home/Index)

Unauthorized Login
The "Unauthorized Login" scenario occurs when a user attempts to access a restricted area or perform an action without proper authentication. 
In the context of the micro-service architecture outlined in this repository, unauthorized login is effectively managed through secure session practices and proper API endpoint design.

### User Operation

Create View (https://localhost:44326/User/Create)
Edit User] (https://localhost:44326/User/Edit/1)
View User Details (https://localhost:44326/User/Details/2)
Delete User Details (https://localhost:44326/User/Delete/1)

### Trail Operations

Create View (https://localhost:44326/Trail/Create)
Edit View (https://localhost:44326/Trail/Edit/1))
Details View (https://localhost:44326/Trail/Details/1)
Delete View (https://localhost:44326/Trail/Delete/4)

## Conclusion

In conclusion, this repository offers a comprehensive micro-service architecture for a secure and scalable authentication system. The documentation provides a detailed understanding of the design, implementation, and adherence to legal and ethical standards. Interact with the live-hosted micro-service and explore the associated GitHub repository to experience these functionalities firsthand.

