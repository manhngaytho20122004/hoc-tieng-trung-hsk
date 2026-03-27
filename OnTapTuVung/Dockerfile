FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /app

# Copy csproj và restore dependencies
COPY *.csproj ./
RUN dotnet restore

# Copy toàn bộ source
COPY . ./
RUN dotnet publish -c Release -o out

# Build runtime image
FROM mcr.microsoft.com/dotnet/aspnet:6.0
WORKDIR /app
COPY --from=build /app/out .

# Tạo thư mục data cho SQLite
RUN mkdir -p /app/data

# Mở cổng
EXPOSE 8080
EXPOSE 80
EXPOSE 443

# Cấu hình environment
ENV ASPNETCORE_URLS=http://+:8080

ENTRYPOINT ["dotnet", "OnTapTuVung.dll"]