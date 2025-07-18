# aldi-homework
Simple MVC implementation of a Library.

Because of time constraints and the simplicity of the task I decided to keep it to a single API csproj that follows a clean, layered architecture. With some exception handling and an example unit test.

API layer (Controller) -> Service layer -> Repository layer -> EFC -> DB

Things to improve on if this was a real project / I skipped because of time constraints: Base class implementations (repository, entity etc.), UnitOfWork, 
DDD & Clean Architecture, CQRS, Vertical slice, Logging, Metrics, More Tests, More Validation, DTOs, IOptions