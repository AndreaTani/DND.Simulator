# DND.Simulator
[![License](https://img.shields.io/badge/License-MIT-green)](LICENSE)

## 🚀 Overview

This project is a decoupled, event-driven application designed to model and manage the complex combat and character mechanics of Dungeons & Dragons 5th Edition. It aims to separate business logic (the D&D rules) from infrastructure and presentation concerns, providing a highly reliable and testable core.

The application adheres strictly to the principles of **Clean Architecture** and uses **Domain Events** as the primary mechanism for state change, reaction, and communication between components.

## 📐 Architecture and Design

The application is structured into clear, decoupled layers, ensuring the core **Domain** remains independent of technology and UI implementation. 

### 1. Domain (`DND.Domain`)

* **Heart of the Application:** Contains the D&D rules, the `Creature` Aggregate Root, Value Objects (like `Condition`, `DamageType`), and the `IDomainEvent` definitions.
* **The Domain knows nothing about:** Databases, WPF, Controllers, or Handlers.
* **Key Principle:** The Domain *publishes facts* (Domain Events) about what happened, such as `CreatureHPChangedEvent` or `CreatureSensesAddedEvent`.

### 2. Application (`DND.Application`)

* **The "Use Case" Layer:** Contains interfaces (Contracts) and business logic orchestrators (Handlers/Services).
* **Handlers:** Classes that implement `IDomainEventHandler<TEvent>`. They subscribe to Domain Events and execute the required response logic (e.g., checking for death, updating the UI).
* **Services:** Define the contracts for communication with infrastructure (e.g., `ICreatureService`, `IUIDispatcher`, `ILoggingService`).

### 3. Infrastructure (`DND.Infrastructure`)

* **Implementation Layer:** Provides concrete implementations for Application contracts (e.g., `CreatureRepository`, `SqlDbContext`, `DiceRollerService`).
* **Crucial Role:** The Repository implementation is responsible for both **persisting** the Aggregate Root's state and **dispatching** any Domain Events raised by the Aggregate.

### 4. Presentation (`DND.Simulator.UI`)

* **The User Interface:** WPF project that handles user interaction, data binding, and displays the game state.
* **Key Service:** Contains the concrete `UIDispatcher` service, which implements the `IUIDispatcher` contract from the Application layer, allowing safe, cross-thread updates to the UI.

## ✅ Implemented Features & Mechanics

The following mechanics have been built using a TDD approach, ensuring complete separation of concerns and test coverage:

### Combat Status Lifecycle

* **HP Tracking and Critical Status:**
    * Handles `CreatureHPChangedEvent`.
    * Automatically determines and applies `Condition.Unconscious`, `Condition.Dying` (HP $\le 0$), and `Condition.Dead` (HP $\le -\text{Max HP}$).
* **Death Saves:**
    * `InitializeDeathSavesHandler`: Correctly filters, initializing saves only if the creature is a Player Character.
    * `ProcessDeathSaveRollHandler`: Records player-initiated death save rolls.
* **Revival and Initiative:**
    * The `Creature` Aggregate handles its own revival state change (e.g., setting HP to 1 and removing conditions).
    * `RestoreToCombatHandler`: Handles the `CreatureRevivedEvent` by signaling the `ICombatSessionService` to restore the creature to the initiative order.

### Permanent Attribute Changes (Character Sheet)

We have implemented handlers for permanent changes, ensuring the Character Sheet ViewModel is notified of changes to stats that require a full UI refresh:

* Adding/Removing **Proficiency**
* Adding/Removing **Expertise**
* Adding/Removing **Senses**
* Adding/Removing **Languages**
* Adding/Removing **Damage Immunities**

## 💡 Design Principles in Action

* **Decoupling via Domain Events:** Handlers react to events (`CreatureHPChangedEvent`) without knowing who fired them. This allows components to be added, removed, or changed without affecting the core logic.
* **Single Responsibility Principle (SRP):** Each Handler has one purpose:
    * **Example:** `InitializeDeathSavesHandler` only checks the creature type and calls the Manager. It does *not* log or check HP status.
* **Cross-Cutting Concerns (Logging/UI):** We adopted the following strategy:
    1.  **Logging:** `ILoggingService` is injected directly into business handlers (e.g., `HPDrivenStatusHandler`) where the core action occurs.
    2.  **UI Notification:** We use **Handler Multiplexing**. A single handler (`NotifyCharacterSheetUpdateHandler`) is responsible for *all* UI refresh notifications and is subscribed to multiple permanent attribute change events, preventing the need for dozens of redundant notification handlers.
* **Async/Await Convention:** All asynchronous methods are correctly named with the `Async` suffix (e.g., `HandleAsync`), following the standard .NET convention.
