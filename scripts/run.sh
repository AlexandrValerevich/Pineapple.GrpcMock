#!/bin/bash

# Build the Protos project
dotnet build /proto-assembly/Pineapple.GrpcMock.Protos.csproj --no-restore -c Release -o /app /p:UseAppHost=false

# Check if the build was successful
if [ $? -eq 0 ]; then
    echo "Protos project build successful. Running the RpcHost project."

    cd /app
    # Run the RpcHost project
    dotnet Pineapple.GrpcMock.RpcHost.dll
else
    echo "Protos project build failed."
fi