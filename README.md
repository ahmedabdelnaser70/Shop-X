# 🛒 Shop-X
### Full-Stack E-Commerce Application | ASP.NET Core (.NET 8) & Angular 20

![.NET](https://img.shields.io/badge/.NET-10.0-blueviolet)
![Angular](https://img.shields.io/badge/Angular-21-red)
![C%23](https://img.shields.io/badge/C%23-Programming-green)
![TypeScript](https://img.shields.io/badge/TypeScript-Language-blue)
![Architecture](https://img.shields.io/badge/Architecture-Clean%20Architecture-success)
![Status](https://img.shields.io/badge/Project-Portfolio-important)

**Shop-X** is a full-stack E-Commerce web application developed to demonstrate real-world experience with modern **ASP.NET Core backend architecture** and **Angular frontend development**.  
The project reflects production-style design, clean code practices, and scalable application structure.

---

## 📌 Project Summary

- Full-stack E-Commerce system
- Backend: ASP.NET Core (.NET 10)
- Frontend: Angular 21
- Clean, scalable, and maintainable architecture
- Built as a professional portfolio project

---

## 🚀 Key Features

### Backend (.NET)
- Multi-project ASP.NET Core solution using **dotnet CLI**
- Repository Pattern
- Unit of Work Pattern
- Specification Pattern
- Clean separation of concerns using multiple `DbContext` boundaries
- ASP.NET Identity for authentication and authorization
- Role-based access control
- Redis caching for shopping basket
- Order creation and processing workflow
- Stripe payment integration with **3D Secure (EU standards)**
- SignalR for real-time functionality
- Cloud deployment to **Microsoft Azure**

---

### Frontend (Angular)
- Modular Angular architecture with lazy-loaded modules
- Angular Routing
- Angular Reactive Forms
- Reusable and scalable form components
- Multi-step checkout workflow
- Client-side and async validation
- Paging, sorting, searching, and filtering
- UI built using **Angular Material** and **Tailwind CSS**

---

## 🏗️ Architecture Overview

Shop-X follows a clean, scalable, and production-oriented architecture.

### Backend Architecture
- Layered ASP.NET Core solution
- Clear separation of concerns:
  - API Layer
  - Core / Application Layer
  - Infrastructure Layer
- Repository, Unit of Work, and Specification patterns
- Multiple `DbContext` implementations for bounded contexts
- Stateless APIs with Redis-based caching
- Secure authentication using ASP.NET Identity
- Role-based authorization
- External service integrations (Stripe, Redis)

### Frontend Architecture
- Modular Angular structure
- Lazy-loaded feature modules
- Service-based data access
- Reusable UI and form components
- Reactive Forms for complex workflows
- Clean separation between UI, services, and models

### Communication
- RESTful APIs for client–server interaction
- SignalR for real-time features
- Secure payment processing via Stripe (3D Secure)

This architecture is designed for maintainability, scalability, and real-world production readiness.

---

## 🧰 Tech Stack

- ASP.NET Core (.NET 10)
- Angular 21
- C#
- TypeScript
- Redis
- Stripe API
- SignalR
- Angular Material
- Tailwind CSS

---

## 🎯 Project Objective

This project was developed to:
- Demonstrate full-stack development skills
- Apply enterprise-level architectural patterns
- Showcase real-world E-Commerce workflows
- Serve as a professional portfolio and technical evaluation project
