# 🚀 BACKEND

## TwoSum Enterprise

Welcome to the **TwoSum Enterprise** repository! This project leverages **Domain-Driven Design** principles and messaging concepts to elegantly solve the TwoSum problem. 

### 🛠 Key Components:

- **Solution**: The main aggregate root that orchestrates the solution process.
- **SolutionStatus**: An enum that represents the different statuses of the solution (`Started`, `InProgress`, `Solved`).
- **SolutionIteration**: Keeps track of each iteration as we work to solve the problem.
- **Events**: Domain events such as `SolutionCreatedDomainEvent` and `NextSolutionIterationRequested` facilitate communication and state transitions.

## 🏁 How to Run

To run the project locally, we use **Docker Compose** to set up all necessary services and dependencies.

### 📝 Prerequisites

- Docker 🐋
- Docker Compose 🛠️

### ⚙️ Steps

1. Clone the repository:

   ```bash
   git clone https://github.com/RuslanPr0g/twosum-enterprise
   cd twosum-enterprise
   ```

2. Build and run using Docker Compose:

   ```bash
   docker-compose up --build
   ```

   This command will build the required containers and start the project.

## 🌟 Usage

Once the project is up and running, you can interact with it programmatically through its **SWAGGER API** or integrate it into other services that need the TwoSum solution domain logic.

---

# 🌐 FRONTEND

## 🗓 Future Plans

- **Implement the Frontend**: Develop a user-friendly interface to interact with the TwoSum solution.
- **User Authentication**: Integrate authentication features to secure access to the application.
- **Real-time Updates**: Add functionalities for real-time updates to enhance user experience.
- **Testing and Documentation**: Improve testing coverage and provide detailed documentation for users.
- **Feature Enhancements**: Continuously add new features based on user feedback and needs.
```

### Key Changes:
- The TODO section is now a clear plan for future development.
- The overall structure is maintained for easy navigation and understanding. 

Feel free to modify any part or let me know if you need further adjustments!
