#See https://aka.ms/containerfastmode to understand how Visual Studio uses this Dockerfile to build your images for faster debugging.

FROM mcr.microsoft.com/dotnet/aspnet:3.1 AS base
WORKDIR /app
EXPOSE 80

FROM mcr.microsoft.com/dotnet/sdk:3.1 AS build
WORKDIR /src
COPY ["HotelBookingAPI/HotelBookingAPI.csproj", "HotelBookingAPI/"]
COPY ["HotelBookingService/HotelBookingLibrary.csproj", "HotelBookingService/"]
RUN dotnet restore "HotelBookingAPI/HotelBookingAPI.csproj"
COPY . .
WORKDIR "/src/HotelBookingAPI"
RUN dotnet build "HotelBookingAPI.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "HotelBookingAPI.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "HotelBookingAPI.dll"]