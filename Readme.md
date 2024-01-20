# Pineapple.GrpcMock

![Your App Logo](link_to_logo.png)

## Overview

Pineapple.GrpcMock is a application that help to mock grpc services in integration tests.

## Table of Contents

- [Pineapple.GrpcMock](#pineapplegrpcmock)
  - [Overview](#overview)
  - [Table of Contents](#table-of-contents)
  - [Getting Started](#getting-started)
  - [Administration](#administration)
  - [Environment Variable Description](#environment-variable-description)
  - [Logging](#logging)
  - [Ports](#ports)
  - [Development](#development)
    - [Run](#run)
    - [Build](#build)
  - [Codding convention](#codding-convention)
  - [Backlog](#backlog)

## Getting Started

Before usage you need to prepare stub files in json format and put it to **/stub** folder in container. Example json:

```json
{
  "serviceShortName": "EchoService",
  "method": "Echo",
  "priority": 10,
  "request": {
    "body": {
      "message": "Hello World",
      "startTime": "2023-09-18T12:34:56.7890123Z",
      // other fields
    }
  },
  "response": {
    "body": {
      "message": "Hello World",
      "time": "2023-09-18T12:34:56.7890123Z",
       // other fields
    },
    "status": {
      "code": 0,
      "details": "Everything is okay just a test!"
    },
    "metadata": {
      "code": 1009,
      "string": "text",
      "string-bin": "text"
    },
    "delay": "00:00:05.000"
  }
}
```

And proto file of mocked service and put it into **/proto** folder in container. Example proto:

```proto
syntax = "proto3";

package grpc.mock;

import "google/protobuf/timestamp.proto";

option csharp_namespace = "GrpcMock.Proto";

service EchoService{
  rpc Echo (EchoModel) returns (EchoModel);
}

message EchoModel
{
  string message = 1;
  google.protobuf.Timestamp time = 2;
}

```

GrpcMock can be easily deployed using Docker or Docker-Compose. Docker allows for containerization, making it simple to set up and run your application.

To run the application using Docker:

```bash
docker run -d \
    -p 5001:5001 \
    -p 5002:5002 \
    -v $(pwd)/stub:/stub \
    -v $(pwd)/proto:/proto \
    mazaika/pineapple.grpc.mock
```

Or use docker-compose.yaml:

```yaml
version: '3.4'

services:
  pineapple.grpc.mock.rpchost:
    image: pineappleslice/grpc.mock:0.0.1
    ports:
      - 5001:5001
      - 5002:5002
    volumes:
      - ./stub:/stub
      - ./proto:/proto
```

With command:

```bash
docker-compose up -d
```

## Administration

This application includes Swagger for API documentation. You can access the Swagger UI at:

```txt
http://127.0.0.1:5001/swagger
```

## Environment Variable Description

- **Proto__Folder**: Absolute path to folder with proto files. Default: /proto
- **Stub__Folder**: Absolute path to folder with stubs. Default: /stub

## Logging

All logs can be configured with help of Serilog env variables. Example:

```txt
- Serilog__MinimumLevel__Default=Information
- Serilog__WriteTo__0__Name=Console
- Serilog__WriteTo__0__Args__outputTemplate={Timestamp:MM-dd HH:mm:ss.fff zzz} [{TraceId}] [{Level:u3}] {SourceContext}. {Message:lj}{NewLine}{Exception}
```

To write logs in json format:

```txt
- Serilog__MinimumLevel__Default=Verbose
- Serilog__WriteTo__0__Name=Console
- Serilog__WriteTo__0__Args__formatter=Serilog.Formatting.Json.JsonFormatter
```

## Ports

App runs on the following ports:

- The REST API is available on port 5001 (HTTP 1.1).
- The gRPC service is available on port 5002 (HTTP 2.0).

## Development

### Run

```bash
dotnet run -p:ProtoFiles=./proto/*.proto -p:StubFiles=./stub/*.json
```

### Build

```bash
dotnet build -p:ProtoFiles=./proto/*.proto -p:StubFiles=./stub/*.json
```

## Codding convention

During development this rules can be changed.

- **Http Api** contracts should:
  - Entry model end with *ApiRequest*
  - Common model end with *ApiModel*
  - Output model end with *ApiResponse*

- **Grpc** contracts should:
  - Entry model end with *Request*
  - Common model end with *Model*
  - Output model end with *Response*

- **CQRS** contract should:
  - Entry command/query model end with *Command/Query*
  - Common model end with *Dto*
  - Result model end with *CommandResult/QueryResult*

- **Between Layer** contracts should:
  - Common model end with *Dto*

- **Http client** contracts should:
  - Entry model end with *Request*
  - Common model end with *Model*
  - Output model end with *Response*

- **Grpc client** contracts should:
  - Entry model end with *Request*
  - Common model end with *Model*
  - Output model end with *Response*

- **RabbitMq/Kafka** contracts should:
  - Entry model end with *Message*
  - Common model end with *Model*

## Backlog

- Features
  - ~~As a user I want to have Swagger~~
  - ~~As a user I want to set up stub status code~~
  - ~~As a user I want to set up stub trailer and metadata~~
  - ~~As a user I want to get all services that can be added~~
  - ~~As a user I want to set up delay for stubbed response~~
  - ~~As a user I want to set up stub for grpc service method therefore REST api~~
  - ~~As a user I want to get all current stubs in my application~~
  - ~~As a user I want to set up priority for my stub messages~~
  - ~~As a user I want to have docker image to run stub like container~~
  - ~~As a user I want to clear all my stubs~~
  - As a user I want to get possible input/output properties and their type name for method
  - As a user I want to set up default stub for Service/Method
  - As a user I want to proxy request to actual server if there no stub

- Code
  - ~~How to return response?~~
  - ~~Before start I need to collect all stubs from folder and put to registry~~
  - ~~I need some registry for my stubs~~
  - ~~Collect all grpc service from assembly~~
  - ~~Generate grpc services classes with help of reflection and than use interceptor to substitute response~~
  - ~~Add IProtoConverter to serialize and deserialize protobuf models~~
  - ~~Add Logging Behavior~~ Formulated process of adding business logs
  - ~~Add OneOf library instead of Exceptions~~ Add ErrorOr instead
  - ~~Add Validation Behavior~~
  - ~~Add global Exception Handler for Grpc~~
  - ~~Add global Exception Handler for Http~~
  - ~~Add Docker file~~
  - ~~Add docker-compose.yaml~~
  - ~~Add validation for all commands/query~~
  - ~~Add ErrorOr Handling in controllers~~
  - Add documentation for application
  - Make proto assembly like a plugin and build only in time of project start instead of whole project
  - Find out anther features from wiremock.
