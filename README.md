# BACKEND
# TwoSum Solution Domain

This repository contains the logic for managing the solution to the TwoSum problem using Domain-Driven Design principles and messaging concepts.

### Key Components:

- **Solution**: Represents the main aggregate root that manages the solution process.
- **SolutionStatus**: Enumerates the status of the solution (`Started`, `InProgress`, `Solved`).
- **SolutionIteration**: Tracks each iteration in attempting to solve the problem.
- **Events**: Domain events like `SolutionCreatedDomainEvent` and `NextSolutionIterationRequested` manage communication and state transitions.

## How to Run

To run the project locally, Docker Compose is used for setting up necessary services and dependencies.

### Prerequisites

- Docker
- Docker Compose

### Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/RuslanPr0g/twosum-enterprise
   cd repository
   ```

2. Build and run using Docker Compose:

   ```bash
   docker-compose up --build
   ```

   This command will build the necessary containers and start the project.

## Usage

Once the project is running, you can interact with it programmatically through its SWAGGER API or integrate it into other services that require the TwoSum solution domain logic.


# FRONTEND
... TODO