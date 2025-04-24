# PPSRRegistrations Solution

This repository contains two main applications:

- **PPSRRegistrations.api** – A .NET 8 Web API for managing registration data.
- **PPSRRegistrations** – A React (Vite-based) application serving as the UI for interacting with the API.

---

## 🧩 Project Structure

```
/
├── PPSRRegistrations.api/
│   └── src/                      # .NET API projects
│   └── tests/                    # .NET unit test projects
│   └── PPSRRegistrations.sln
│   └── Dockerfile                # Dockerfile for API
├── PPSRRegistrations/            # React app (Vite)
│   ├── src/
│   ├── index.html
│   └── package.json
│   └── Dockerfile                # Dockerfile for frontend
```

---

## 🚀 Requirements

- [.NET SDK 8.0](https://dotnet.microsoft.com/download)
- [Node.js 18+](https://nodejs.org/)
- [Docker](https://www.docker.com/)

---

## 🔧 Running the Applications Manually

### 1. Run the .NET Web API

```bash
cd PPSRRegistrations.api/src/PPSRRegistrations.API
dotnet run
```

- By default, it runs on: `https://localhost:7225`
- Access Swagger at: `https://localhost:7225/swagger`

### 2. Run the React Frontend

```bash
cd PPSRRegistrations
npm install
npm run dev
```

- It runs on: `http://localhost:5173`

> ⚠️ Make sure the frontend is configured to call the API on `https://localhost:7225` at .env

---

## 🐳 Running with Docker

### 1. Build and Run the API Container

```bash
docker build -t ppsr-api ./PPSRRegistrations.api
docker run -d -p 5001:8080 --name ppsr-api ppsr-api
```

- API available at: `http://localhost:5001/swagger`

---

### 2. Build and Run the Frontend Container

```bash
docker build -t ppsr-html ./PPSRRegistrations
docker run -d -p 5002:80 --name ppsr-html ppsr-html
```

- Frontend available at: `http://localhost:5002`

> 🧠 Ensure the frontend is making requests to `http://localhost:5002` (in production mode) – you may need to adjust the `vite.config.js` or use environment variables during build time.

---

## 🧪 Running Unit Tests

```bash
dotnet test PPSRRegistrations.sln
```