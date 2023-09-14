# Pineapple.GrpcMock

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
  - Result model end with *Result*

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

## Steps for implementing service

- ~~Collect all grpc service from assembly~~
- ~~I need some registry for my stubs~~
- ~~Before start I need to collect all stubs from folder and put to registry~~
- Write endpoint for substitution
- Add possibility to mock trailer and status codes
- Add OneOf library instead of Exceptions

## Environment Variable

- **Proto__Folder** - absolute path to folder with protobuf. It affect on project build. Have no default value.
- **Stub__Folder** - absolute path to folder with stubs. It affect on project build. Have no default value.
