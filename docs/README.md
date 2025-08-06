# GeoAlert – Serverless Geolocation Alerts with .NET 8 & AWS

GeoAlert is a production-ready serverless system built with **.NET 8**, **AWS Lambda**, **API Gateway**, **DynamoDB**, and **CDK**. It enables real-time alerting based on geolocation events, showcasing modern cloud-native architecture using minimal APIs and modular service registration.

---

## 🔍 Use Case
> Send dynamic alerts to users based on predefined geofences and incoming location data. Ideal for logistics, emergency notifications, marketing campaigns, or real-time operations.

---

## 🚀 Tech Stack

- **.NET 8** – Minimal APIs, modular architecture
- **AWS Lambda** – Serverless compute
- **API Gateway** – RESTful endpoint exposure
- **DynamoDB** – NoSQL database for geofence + device data
- **EventBridge (optional)** – Event-driven alert triggers
- **AWS CDK (TypeScript)** – Infrastructure as code
- **GitHub Actions** – CI/CD (planned)

---

## 📦 Project Structure

```bash
GeoAlert-AWS/
├── apps/
│   ├── GeoAlert.Api/                   # Entry point for the Lambda function
│   │   ├── Program.cs                  
│   │   ├── Endpoints/                 # Minimal API endpoints
│   │   └── Services/                  # Business logic, rules, validation
│   └── GeoAlert.Infrastructure/       # AWS integrations (DynamoDB, SNS, etc.)
│
├── infra/                             # CDK project (TypeScript)
│   ├── lib/                           # Stack definitions
│   └── bin/
│
└── README.md
```

---

## 🧠 Key Features

- ✅ Modular startup with `AddGeoFencesServices()` and `AddMapGeoFences()`
- ⚡ Fast and cold-start optimized Minimal API in .NET 8
- 🗺️ Geofence registration and device location matching
- 🔔 SNS or Webhook integration for alert delivery (extensible)
- 🌩️ Infrastructure defined in AWS CDK with environment-based config

---

## ⏯️ How to Run (Locally)

1. Clone the repo
2. Setup AWS credentials
3. Run the API locally:
```bash
cd src/GeoAlert.Api
dotnet run
```
4. Test endpoints using Postman or Swagger

---

## 🛠️ Deployment
> Coming soon in the video series on [Learn with Andrés](https://www.youtube.com/@learnwithandres)

Deployment will use CDK to provision all resources automatically:
```bash
cd infra
cdk deploy
```

---

## 📹 Tutorials (in progress)
- [x] Build the Lambda with Minimal API
- [ ] Deploy with CDK
- [ ] Integrate SNS + alerts
- [ ] Refactor with GeoFence Domain

---

## 👨‍💻 Author
**Andrés Díaz** – Senior .NET + AWS Developer, educator and creator of [Learn with Andrés](https://www.youtube.com/@learnwithandres)

---

## 🤝 Contributions
PRs and suggestions welcome once the base video series is complete.

---

## 🧭 License
MIT