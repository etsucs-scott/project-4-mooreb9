# Habit Tracker (Blazor App)

## Overview

A habit tracking web application built with **ASP.NET Core Blazor Server**. Users can create, manage, and track daily habits while viewing progress, streaks, and weekly performance.

---

## Features

* Add, edit, and delete habits
* Mark habits as completed (once per day)
* Automatic streak tracking
* Daily and weekly progress visualization
* Category filtering
* Dashboard with summary metrics

---

## Design

* **OOP Principles**: encapsulation, abstraction, separation of concerns
* **Interface-based service**: `IHabitService`
* **DTO (`HabitDto`)** used for safe JSON serialization

---

## Data Structures

* `List<Habit>` for storage
* `HashSet<DateTime>` for unique completion tracking

---

## Persistence

* Data stored in `habits.json`
* Uses `System.Text.Json` for serialization

---

## Exception Handling

* Try/catch used in service and UI layers
* Prevents crashes during file operations and updates

---

## Testing

* Built with **xUnit**
* 12+ unit tests covering:

  * Habit logic (streaks, completion)
  * Service operations

Run tests:

```bash
dotnet test
```

---

## Platform

* Cross-platform (Windows, macOS, Linux)
* Built with .NET

---

## Technologies

* C#, .NET 9
* Blazor Server
* Bootstrap
* System.Text.Json
* xUnit

---

## Run

```bash
dotnet run
```

---

## Author

Benjamin Moore
