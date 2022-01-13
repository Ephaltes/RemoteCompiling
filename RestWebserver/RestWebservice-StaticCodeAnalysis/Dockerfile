FROM mcr.microsoft.com/dotnet/aspnet:5.0 AS base
WORKDIR /app
EXPOSE 80
EXPOSE 443

# Build app
FROM mcr.microsoft.com/dotnet/sdk:5.0 AS build
WORKDIR /build
COPY . .

RUN dotnet restore "./RestWebservice-StaticCodeAnalysis/RestWebservice-StaticCodeAnalysis.csproj"
RUN dotnet build --no-restore "./RestWebservice-StaticCodeAnalysis/RestWebservice-StaticCodeAnalysis.csproj"

# Publish app
FROM build AS publish
RUN dotnet publish "./RestWebservice-StaticCodeAnalysis/RestWebservice-StaticCodeAnalysis.csproj" -c Release -o /app

# Configure runtime environment
FROM base as final

RUN apt update

# Install gcc and g++
RUN apt install -y gcc
RUN apt install -y g++

# Install .NET 6 SDK
RUN apt install -y wget && \
	wget https://packages.microsoft.com/config/ubuntu/21.04/packages-microsoft-prod.deb -O packages-microsoft-prod.deb && \
	dpkg -i packages-microsoft-prod.deb && \
	rm packages-microsoft-prod.deb && \
	apt install -y apt-transport-https && \
    apt update && \
	apt install -y dotnet-sdk-6.0;

# Install OpenJDK-11
RUN apt install -y openjdk-11-jdk && \
    apt install -y ant;

# Setup JAVA_HOME
ENV JAVA_HOME /usr/lib/jvm/java-11-openjdk-amd64/
RUN export JAVA_HOME

# Install .NET-only sonarscanner
RUN dotnet tool install -g dotnet-sonarscanner
ENV PATH="${PATH}:/root/.dotnet/tools"

# Install generic sonarscanner
RUN apt install -y unzip
RUN wget https://binaries.sonarsource.com/Distribution/sonar-scanner-cli/sonar-scanner-cli-4.6.2.2472-linux.zip
RUN unzip sonar-scanner-cli-4.6.2.2472-linux.zip
ENV PATH=${PATH}:/app/sonar-scanner-4.6.2.2472-linux/bin

# Install valgrind
RUN apt install -y valgrind

COPY --from=publish /app . 
WORKDIR /app

# Run app
ENTRYPOINT ["dotnet", "RestWebservice-StaticCodeAnalysis.dll"]