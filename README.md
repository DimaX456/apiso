# apiso
 ```mermaid
    classDiagram
    Ticket <.. Hall
    Ticket <.. Theatre
    Ticket <.. Client
    Ticket <.. Performance
    Ticket <.. Staff
    Staff .. Post
    BaseAuditEntity --|> Hall
    BaseAuditEntity --|> Theatre
    BaseAuditEntity --|> Client
    BaseAuditEntity --|> Performance
    BaseAuditEntity --|> Ticket
    BaseAuditEntity --|> Staff
    IEntity ..|> BaseAuditEntity
    IEntityAuditCreated ..|> BaseAuditEntity
    IEntityAuditDeleted ..|> BaseAuditEntity
    IEntityAuditUpdated ..|> BaseAuditEntity
    IEntityWithId ..|> BaseAuditEntity
    class IEntity{
        <<interface>>
    }
    class IEntityAuditCreated{
        <<interface>>
        +DateTimeOffset CreatedAt
        +string CreatedBy
    }
    class IEntityAuditDeleted{
        <<interface>>
        +DateTimeOffset? DeletedAt
    }
    class IEntityAuditUpdated{
        <<interface>>
        +DateTimeOffset UpdatedAt
        +string UpdatedBy
    }
    class IEntityWithId{
        <<interface>>
        +Guid Id
    }        
    class Hall{
        +short Number
        +short NumberOfSeats
    }
    class Theatre{
        +string Title
        +string Address
    }
    class Client {
        +string LastName
        +string FirstName
        +string Patronymic
        +short Age
        +string? Email
    }
    class Performance {
        +string Title
        +string? Description
        +short Limitation
    }

    class Staff {
        +string LastName
        +string FirstName
        +string Patronymic
        +short Age
        +Post Post
    }
    class Ticket {
        +Guid HallId 
        +Guid TheatreID
        +Guid PerformanceID
        +Guid ClientId
        +Guid? StaffId
        +short Row
        +short Place
        +decimal Price
        +DateTimeOffset Date
    }
    class Post {
        <<enumeration>>
        Cashier(Кассир)
        Manager(Менеджер)
        None(Неизвестно)
    }
    class BaseAuditEntity {
        <<Abstract>>        
    }
```


# SQL Script
