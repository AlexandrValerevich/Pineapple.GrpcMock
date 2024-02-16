FROM mcr.microsoft.com/dotnet/sdk:7.0 as build
# Set the working directory
WORKDIR /app
# Copy the source code into the Docker image
COPY . .
# Restore project
RUN dotnet restore
# Publish main project
RUN dotnet publish "./src/Pineapple.GrpcMock.RpcHost/Pineapple.GrpcMock.RpcHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Use the aspnet base image
FROM mcr.microsoft.com/dotnet/sdk:7.0

# Expose necessary ports
EXPOSE 5001
EXPOSE 5002

# Define environment variables
ENV Proto__Folder=/proto
ENV Stub__Folder=/stub
ENV ASPNETCORE_ENVIRONMENT=Production

# Create necessary folders
RUN mkdir /proto /stub

# Copy proto assembly
COPY ./src/Pineapple.GrpcMock.Protos /proto-assembly
WORKDIR /proto-assembly
RUN dotnet restore

# Set the working directory
WORKDIR /app
# Copy the output of the first project from Stage 1
COPY --from=build /app/publish .

WORKDIR /scripts
COPY ./scripts .
# Make the script executable as root
RUN chmod +x /scripts/run.sh
# replace those CR characters with nothing, which will leave these lines with
# LF (\n) as the ending, and Bash will be able to read and execute the file by running
RUN sed -i -e 's/\r$//' "/scripts/run.sh"
RUN chmod +x /app/Pineapple.GrpcMock.RpcHost.dll
# Set the entry point to the script
ENTRYPOINT ["/scripts/run.sh"]
