#!/bin/bash

# Publish the application in Release configuration
dotnet publish "/app/src/Pineapple.GrpcMock.RpcHost/Pineapple.GrpcMock.RpcHost.csproj" -c Release -o /app/publish /p:UseAppHost=false

cd /app/publish
# Set your application entry point and run it
dotnet Pineapple.GrpcMock.RpcHost.dll