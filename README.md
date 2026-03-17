# 🎉 EventEase Booking System

## 📌 Overview
EventEase is a web-based application designed to simplify event booking and venue management. It allows users to view available events, explore venues, and make bookings بسهولة and efficiently.

The system is built using ASP.NET Core MVC and follows a simple and scalable structure suitable for beginners and real-world applications.

---

## 🚀 Features

- 📅 View available events
- 🏢 View and manage venues
- 📝 Create bookings for events
- 🔗 Link events and venues through bookings
- 🖼️ Display venue images using image URLs
- ✔️ Form validation for required fields

---

## 🛠️ Technologies Used

- **C#**
- **ASP.NET Core MVC**
- **Entity Framework Core**
- **SQL Server / Azure SQL Database**
- **HTML, CSS, Bootstrap**

---

## 🗄️ Database Structure

The system consists of three main tables:

### 1. Events
- EventID (Primary Key)
- EventName
- EventDate
- EventTime
- Description

### 2. Venues
- VenueID (Primary Key)
- VenueName
- Location
- Capacity
- ImageURL

### 3. Bookings
- BookingID (Primary Key)
- EventID (Foreign Key)
- VenueID (Foreign Key)
- UserName
- BookingDate

---

## 🔗 Relationships

- One event can have many bookings
- One venue can have many bookings
- Each booking is linked to one event and one venue

---

## ☁️ Deployment

The application can be deployed using cloud platforms such as Microsoft Azure. Using Platform as a Service (PaaS) allows for faster deployment, automatic scaling, and reduced infrastructure management.

---

