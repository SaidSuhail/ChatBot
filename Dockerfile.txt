# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy project file and restore dependencies
COPY ChatBot/ChatBot.csproj ./ChatBot/
WORKDIR /app/ChatBot
RUN dotnet restore

# Copy everything else and build the project
COPY ChatBot/. ./
RUN dotnet publish -c Release -o /app/out

# Stage 2: Run
FROM mcr.microsoft.com/dotnet/aspnet:8.0
WORKDIR /app
COPY --from=build /app/out .

# Run the app
ENTRYPOINT ["dotnet", "ChatBot.dll"]
