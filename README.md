# 🌍 Horizons Travel Blog

[![ASP.NET](https://img.shields.io/badge/ASP.NET-10.0-512BD4?logo=.net)](https://dotnet.microsoft.com/)
[![Entity Framework](https://img.shields.io/badge/Entity_Framework-10.0-512BD4?logo=.net)](https://learn.microsoft.com/en-us/ef/)
[![PostgreSQL](https://img.shields.io/badge/PostgreSQL-17-4169E1?logo=postgresql)](https://www.postgresql.org/)
[![Docker](https://img.shields.io/badge/Docker-Enabled-2496ED?logo=docker)](https://www.docker.com/)
[![Render](https://img.shields.io/badge/Render-Deployed-46E3B7?logo=render)](https://render.com/)
[![Neon](https://img.shields.io/badge/Neon-Database-00E599?logo=neon)](https://neon.tech/)
[![License](https://img.shields.io/badge/License-MIT-green.svg)](LICENSE)

## 📖 Overview

**Horizons** is a full-featured travel blog and destination discovery platform built with ASP.NET Core MVC. Users can explore destinations, save favorites, leave reviews, and connect with fellow travelers. Administrators have full control over destinations, users, and content.

🔗 **Live Demo:** [https://horizons-qghl.onrender.com](https://horizons-qghl.onrender.com)

---

## ✨ Key Features

| Feature | Description |
|---------|-------------|
| 🌍 **Destination Catalog** | Browse and filter destinations by terrain, continent, and country |
| ❤️ **Favorites** | Save your favorite destinations for later |
| ⭐ **Reviews & Ratings** | Leave reviews and rate destinations |
| 👤 **User Profiles** | Manage personal information and preferences |
| 📬 **Contact Messages** | Send messages to administrators |
| 📢 **System Messages** | Admin → user announcements |
| 🗺️ **Map Integration** | Visualize destinations on an interactive map |
| 👑 **Admin Dashboard** | Full control over destinations, users, and messages |
| 🌓 **Dark/Light Theme** | Toggle between dark and light modes |
| 🏔️ **Terrain Filtering** | Filter destinations by terrain type (Mountain, Beach, Forest, etc.) |

---

## 🛠️ Technology Stack

### Backend

| Technology | Version | Purpose |
|------------|---------|---------|
| **ASP.NET Core** | 10.0 | Web framework |
| **Entity Framework Core** | 10.0 | ORM & data access |
| **PostgreSQL** | 17 | Relational database |
| **Npgsql** | 10.0 | PostgreSQL provider for EF Core |
| **ASP.NET Core Identity** | 10.0 | Authentication & authorization |
| **SendGrid** | Latest | Email service |

### Infrastructure

| Technology | Purpose |
|------------|---------|
| **Docker** | Containerization |
| **Render** | Cloud hosting & deployment |
| **Neon** | Serverless PostgreSQL database |
| **GitHub Actions** | CI/CD pipeline |
| **Docker Hub** | Container registry |

### Frontend

| Technology | Purpose |
|------------|---------|
| **Bootstrap** | 5.3 | Responsive UI |
| **Bootstrap Icons** | 1.11 | Icon library |
| **Leaflet.js** | Latest | Interactive maps |

---

## 🚀 Getting Started

### Prerequisites

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download/dotnet/10.0)
- [PostgreSQL 17](https://www.postgresql.org/download/) (or use Docker)
- [Git](https://git-scm.com/)
- [Docker Desktop](https://www.docker.com/products/docker-desktop/) (recommended)
- [Visual Studio 2022](https://visualstudio.microsoft.com/) or VS Code

### Quick Start with Docker (Recommended)

```bash
# Clone the repository
git clone https://github.com/SilenceMustBeHeard/Horizons.git
cd Horizons

# Create .env file with your credentials
cat > .env << EOF
POSTGRES_DB=HorizonsDB
POSTGRES_USER=postgres
POSTGRES_PASSWORD=your_secure_password
ASPNETCORE_ENVIRONMENT=Production
EOF

# Start the application with Docker Compose
docker compose up --build
```

The application will be available at `http://localhost:8083`.

### Manual Setup (Without Docker)

#### 1. Configure PostgreSQL Connection

Update `appsettings.json` in the `Horizons.Web` project:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=HorizonsDB;Username=postgres;Password=your_password"
  }
}
```

#### 2. Apply Database Migrations

```bash
dotnet ef database update --project Horizons.Data --startup-project Horizons.Web
```

The seeder will automatically create:

- Admin user: `admin@horizons.com` / `Horizons12345!@#$%`
- Manager user: `manager@horizons.com` / `Horizons12345!@#$%`
- Sample terrains and destinations

#### 3. Configure SendGrid (for Password Reset)

```bash
dotnet user-secrets set "SendGrid:ApiKey" "YOUR_SENDGRID_API_KEY"
dotnet user-secrets set "SendGrid:FromEmail" "your-verified-email@example.com"
```

#### 4. Run the Application

```bash
cd Horizons.Web
dotnet run
```

---

## 🐳 Docker Deployment

### Build and Run

```bash
# Build the image
docker build -t horizons .

# Run with PostgreSQL (via docker-compose)
docker compose up -d

# Or run standalone with external PostgreSQL
docker run -d -p 8083:8080 \
  -e ConnectionStrings__DefaultConnection="Host=postgres;Port=5432;Database=HorizonsDB;Username=postgres;Password=secret" \
  -e ASPNETCORE_ENVIRONMENT=Production \
  --name horizons horizons
```

### Environment Variables

| Variable | Description | Required |
|----------|-------------|----------|
| `ASPNETCORE_ENVIRONMENT` | Runtime environment (Development/Production) | ✅ |
| `ConnectionStrings__DefaultConnection` | PostgreSQL connection string | ✅ |
| `POSTGRES_DB` | Database name (for docker-compose) | ✅ |
| `POSTGRES_USER` | Database user (for docker-compose) | ✅ |
| `POSTGRES_PASSWORD` | Database password (for docker-compose) | ✅ |
| `SendGrid:ApiKey` | SendGrid API key for email | ❌ |
| `SendGrid:FromEmail` | Verified sender email | ❌ |

---

## 🔧 CI/CD Pipeline

The project uses **GitHub Actions** for CI/CD:

- On push to `main` or `deployment-test`:
  1. Builds the Docker image
  2. Pushes it to **Docker Hub**
  3. Render automatically deploys the latest version

### Deployment Status

- **Live URL:** [https://horizons-qghl.onrender.com](https://horizons-qghl.onrender.com)
- **Deployment Platform:** Render.com
- **Database:** Neon (serverless PostgreSQL)
- **Container Registry:** Docker Hub

---

## 👥 User Roles & Permissions

| Permission | 👑 Admin | 📋 Manager | 👤 User |
|------------|----------|------------|---------|
| Browse destinations | ✅ | ✅ | ✅ |
| View destination details | ✅ | ✅ | ✅ |
| Add to favorites | ✅ | ✅ | ✅ |
| Leave reviews | ✅ | ✅ | ✅ |
| Contact administrators | ✅ | ✅ | ✅ |
| Manage all destinations | ✅ | ❌ | ❌ |
| Manage users | ✅ | ❌ | ❌ |
| Manage reviews | ✅ | ✅ | ❌ (own only) |
| Send system messages | ✅ | ❌ | ❌ |

---

## 📸 Screenshots

### Homepage
<img width="800" alt="homepage" src="https://via.placeholder.com/800x400?text=Horizons+Homepage" />

### Destination Details
<img width="800" alt="destination details" src="https://via.placeholder.com/800x400?text=Destination+Details" />

### Admin Dashboard
<img width="800" alt="admin dashboard" src="https://via.placeholder.com/800x400?text=Admin+Dashboard" />

---

## 🤝 Contributing

1. Fork the repository
2. Create a feature branch (`git checkout -b feature/amazing-feature`)
3. Commit your changes (`git commit -m 'Add amazing feature'`)
4. Push to the branch (`git push origin feature/amazing-feature`)
5. Open a Pull Request

---

## 📄 License

This project is licensed under the MIT License - see the [LICENSE](LICENSE) file for details.


  <br/>
  <sub>Built with .NET 10.0 | PostgreSQL | Docker | Deployed on Render</sub>
</div>
