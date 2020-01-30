
FROM microsoft/dotnet:2.2-sdk AS build
WORKDIR /SevenPeaksSoftware.VehicleTracking/Src
COPY ["Src/SevenPeaksSoftware.VehicleTracking.Application/SevenPeaksSoftware.VehicleTracking.Application.csproj", "Src/SevenPeaksSoftware.VehicleTracking.Application/"]
COPY ["Src/SevenPeaksSoftware.VehicleTracking.Domain/SevenPeaksSoftware.VehicleTracking.Domain.csproj", "Src/SevenPeaksSoftware.VehicleTracking.Domain/"]
COPY ["Src/SevenPeaksSoftware.VehicleTracking.Infrastructure/SevenPeaksSoftware.VehicleTracking.Infrastructure.csproj", "Src/SevenPeaksSoftware.VehicleTracking.Infrastructure/"]
COPY ["Src/SevenPeaksSoftware.VehicleTracking.Ioc/SevenPeaksSoftware.VehicleTracking.Ioc.csproj", "Src/SevenPeaksSoftware.VehicleTracking.Ioc/"]
COPY ["Src/SevenPeaksSoftware.VehicleTracking.WebApi/SevenPeaksSoftware.VehicleTracking.WebApi.csproj", "Src/SevenPeaksSoftware.VehicleTracking.WebApi/"]


RUN dotnet restore "Src/SevenPeaksSoftware.VehicleTracking.WebApi/SevenPeaksSoftware.VehicleTracking.WebApi.csproj"
COPY . .
RUN dotnet build "Src/SevenPeaksSoftware.VehicleTracking.WebApi/SevenPeaksSoftware.VehicleTracking.WebApi.csproj" -c Release -o /app

FROM build AS publish
RUN dotnet publish "Src/SevenPeaksSoftware.VehicleTracking.WebApi/SevenPeaksSoftware.VehicleTracking.WebApi.csproj" -c Release -o /app

FROM microsoft/dotnet:2.2-sdk AS final
WORKDIR /app
EXPOSE 80/tcp
COPY --from=publish /app .
ENTRYPOINT ["dotnet", "SevenPeaksSoftware.VehicleTracking.WebApi.dll"]