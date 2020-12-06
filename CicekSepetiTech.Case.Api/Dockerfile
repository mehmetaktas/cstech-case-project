FROM mcr.microsoft.com/dotnet/core/aspnet:2.2-stretch-slim AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

FROM mcr.microsoft.com/dotnet/core/sdk:2.2-stretch AS build
WORKDIR /src
COPY ["CicekSepetiTech.Case.Api/CicekSepetiTech.Case.Api.csproj", "CicekSepetiTech.Case.Api/"]
COPY ["CicekSepetiTech.Case.Domain/CicekSepetiTech.Case.Domain.csproj", "CicekSepetiTech.Case.Domain/"]
COPY ["CicekSepetiTech.Case.Data/CicekSepetiTech.Case.Data.csproj", "CicekSepetiTech.Case.Data/"]
COPY ["CicekSepetiTech.Case.Business/CicekSepetiTech.Case.Business.csproj", "CicekSepetiTech.Case.Business/"]
RUN dotnet restore "CicekSepetiTech.Case.Api/CicekSepetiTech.Case.Api.csproj"
COPY . .
WORKDIR "/src/CicekSepetiTech.Case.Api"
RUN dotnet build "CicekSepetiTech.Case.Api.csproj" -c Release -o /app/build

FROM build AS publish
RUN dotnet publish "CicekSepetiTech.Case.Api.csproj" -c Release -o /app/publish

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .
ENTRYPOINT ["dotnet", "CicekSepetiTech.Case.Api.dll"]