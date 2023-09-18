#!/bin/bash

# Build the application in Release configuration
dotnet build -c Release -o /app/build

# Publish the application in Release configuration
dotnet publish -c Release -o /app/publish /p:UseAppHost=false

# Set your application entry point and run it
dotnet /app/publish/Pineapple.GrpcMock.RpcHost.dll