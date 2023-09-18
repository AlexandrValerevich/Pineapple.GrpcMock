# Use the aspnet base image
FROM mcr.microsoft.com/dotnet/sdk:7.0

# Expose necessary ports
EXPOSE 5001
EXPOSE 5002

# Define environment variables
ENV Proto__Folder=/proto
ENV Stub__Folder=/stub

# Create necessary folders
RUN mkdir /proto
RUN mkdir /stub

# Set the working directory
WORKDIR /app/src

# Copy the source code into the Docker image
COPY . .

RUN dotnet restore

# Make the script executable as root
RUN chmod +x /app/src/scripts/mock.sh

# Set the entry point to the script
ENTRYPOINT ["/app/src/scripts/mock.sh"]
