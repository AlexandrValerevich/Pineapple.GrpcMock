# Pineapple.GrpcMock

## Run

```bash
dotnet run -p:ProtoFiles=./proto/*.proto -p:StubFiles=./stub/*.json
```

## Build

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
  - ~~Add Swagger~~
  - I like user want to set up stub status code
  - I like user want to set up stub trailer and metadata
  - I like user want to set up delay for stubbed response
  - I like user want to set up stub for grpc service method therefore REST api
  - I like user want to set up priority for my stub messages
  - I like user want to get all services that can be added
  - I like user want to get all current stubs in my application
  - I like user want to have docker image to run stub like container

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
  - Add Docker file
  - Add docker-compose.yaml
  - Add manual for application

## Environment Variable

- **Proto__Folder** - absolute path to folder with protobuf. It affect on project build. Have no default value.
- **Stub__Folder** - absolute path to folder with stubs. It affect on project build. Have no default value.
