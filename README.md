# apiso
 ```mermaid
    classDiagram
    Ticket <.. Hall
    Ticket <.. Cinema
    Ticket <.. Client
    Ticket <.. Film
    Ticket <.. Staff
    Staff .. Post
    BaseAuditEntity --|> Hall
    BaseAuditEntity --|> Cinema
    BaseAuditEntity --|> Client
    BaseAuditEntity --|> Film
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
    class Cinema{
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
    class Film {
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
        +Guid CinemaId
        +Guid FilmId
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



  INSERT INTO [dbo].[Cinemas] 
           ([Id]  
           ,[Title]  
           ,[Address]  
           ,[CreatedAt]  
           ,[CreatedBy]  
           ,[DeletedAt]  
           ,[UpdatedAt]  
           ,[UpdatedBy])  
     VALUES 
           ('c1207373-b0af-495c-a33f-d0fac1266e8a' 
           ,N'Карофильм' 
           ,N'пр. Юрия Гагарина, 1, Санкт-Петербург' 
           ,GETDATE() 
           ,'Insert' 
           ,null 
           ,GETDATE() 
           ,'Insert') 

INSERT INTO [dbo].[Cinemas] 
           ([Id] 
           ,[Title] 
           ,[Address] 
           ,[CreatedAt] 
           ,[CreatedBy] 
           ,[DeletedAt] 
           ,[UpdatedAt] 
           ,[UpdatedBy]) 
     VALUES 
           ('0440e29d-7d0e-4d48-acf4-3ec4353c4f9e' 
           ,N'Ленфильм' 
           ,N'Байконурская ул., 14А, Санкт-Петербург' 
           ,GETDATE() 
           ,'Insert' 
           ,null 
           ,GETDATE() 
           ,'Insert') 

INSERT INTO [dbo].[Clients] 
           ([Id] 
           ,[Email] 
           ,[LastName] 
           ,[FirstName] 
           ,[Patronymic] 
           ,[Age] 
           ,[CreatedAt] 
           ,[CreatedBy] 
           ,[DeletedAt] 
           ,[UpdatedAt] 
           ,[UpdatedBy]) 
     VALUES 
           ('edf6b122-3fc3-4793-978b-3fd42f840c45' 
           ,'Insert@gmail.com' 
           ,N'Кочетков' 
           ,N'Денис' 
           ,N'Александрович' 
           ,19 
           ,GETDATE() 
           ,'Insert' 
           ,null 
           ,GETDATE() 
           ,'Insert') 

INSERT INTO [dbo].[Clients] 
           ([Id] 
           ,[Email] 
           ,[LastName] 
           ,[FirstName] 
           ,[Patronymic] 
           ,[Age] 
           ,[CreatedAt] 
           ,[CreatedBy] 
           ,[DeletedAt] 
           ,[UpdatedAt] 
           ,[UpdatedBy]) 
     VALUES 
           ('cb511c9f-3f6d-445d-9cc3-a62392949a6d' 
           ,'Insert@yandex.ru' 
           ,N'Малышева' 
           ,N'Александра' 
           ,N'Юрьевна' 
           ,25 
           ,GETDATE() 
           ,'Insert' 
           ,null 
           ,GETDATE() 
           ,'Insert') 

INSERT INTO [dbo].[Films] 
           ([Id] 
           ,[Title] 
           ,[Description] 
           ,[Limitation] 
           ,[CreatedAt] 
           ,[CreatedBy] 
           ,[DeletedAt] 
           ,[UpdatedAt] 
           ,[UpdatedBy]) 
     VALUES 
           ('0c742592-0cd5-4cba-b76e-be17b1ed8e0b' 
           ,N'Барби' 
           ,N'Ничего такой фильмец!' 
           ,16 
           ,GETDATE() 
           ,'Insert' 
           ,null 
           ,GETDATE() 
           ,'Insert') 

INSERT INTO [dbo].[Films] 
           ([Id] 
           ,[Title]
           ,[Description]
           ,[Limitation]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[DeletedAt]
           ,[UpdatedAt]
           ,[UpdatedBy])
     VALUES
           ('80f6b724-c314-449f-8569-437837fae723'
           ,N'Елки 5'
           ,null
           ,12
           ,GETDATE()
           ,'Insert'
           ,null
           ,GETDATE()
           ,'Insert')

INSERT INTO [dbo].[Halls]
           ([Id]
           ,[Number]
           ,[NumberOfSeats]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[DeletedAt]
           ,[UpdatedAt]
           ,[UpdatedBy])
     VALUES
           ('4ebf7677-ea7c-404e-a633-1795e795feaf'
           ,1
           ,35
           ,GETDATE()
           ,'Insert'
           ,null
           ,GETDATE()
           ,'Insert')

INSERT INTO [dbo].[Halls]
           ([Id]
           ,[Number]
           ,[NumberOfSeats]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[DeletedAt]
           ,[UpdatedAt]
           ,[UpdatedBy])
     VALUES
           ('fb445b53-e276-49a6-84ba-49f9a95011f4'
           ,2
           ,20
           ,GETDATE()
           ,'Insert'
           ,null
           ,GETDATE()
           ,'Insert')

INSERT INTO [dbo].[Staffs]
           ([Id]
           ,[Post]
           ,[LastName]
           ,[FirstName]
           ,[Patronymic]
           ,[Age]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[DeletedAt]
           ,[UpdatedAt]
           ,[UpdatedBy])
     VALUES
           ('560ea098-c735-49ef-aa14-4d96cb7649fd'
           ,0
           ,N'Бажин'
           ,N'Кирилл'
           ,N'Андреевич'
           ,19
           ,GETDATE()
           ,'Insert'
           ,null
           ,GETDATE()
           ,'Insert')

INSERT INTO [dbo].[Staffs]
           ([Id]
           ,[Post]
           ,[LastName]
           ,[FirstName]
           ,[Patronymic]
           ,[Age]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[DeletedAt]
           ,[UpdatedAt]
           ,[UpdatedBy])
     VALUES
           ('c0f43b29-6a95-4814-9c11-9812c2f62aa0'
           ,1
           ,N'Коноплев'
           ,N'Анатолий'
           ,N'Александрович'
           ,45
           ,GETDATE()
           ,'Insert'
           ,null
           ,GETDATE()
           ,'Insert')

INSERT INTO [dbo].[Tickets]
           ([Id]
           ,[HallId]
           ,[CinemaId]
           ,[FilmId]
           ,[ClientId]
           ,[StaffId]
           ,[Row]
           ,[Place]
           ,[Price]
           ,[Date]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[DeletedAt]
           ,[UpdatedAt]
           ,[UpdatedBy])
     VALUES
           ('ad822fb5-6c6d-45d8-990f-e5b17bf2cf8a'
           ,'fb445b53-e276-49a6-84ba-49f9a95011f4'
           ,'c1207373-b0af-495c-a33f-d0fac1266e8a'
           ,'0c742592-0cd5-4cba-b76e-be17b1ed8e0b'
           ,'edf6b122-3fc3-4793-978b-3fd42f840c45'
           ,null
           ,1
           ,5
           ,350
           ,GETDATE()
           ,GETDATE()
           ,'Insert'
           ,null
           ,GETDATE()
           ,'Insert')

INSERT INTO [dbo].[Tickets]
           ([Id]
           ,[HallId]
           ,[CinemaId]
           ,[FilmId]
           ,[ClientId]
           ,[StaffId]
           ,[Row]
           ,[Place]
           ,[Price]
           ,[Date]
           ,[CreatedAt]
           ,[CreatedBy]
           ,[DeletedAt]
           ,[UpdatedAt]
           ,[UpdatedBy])
     VALUES
           ('cf6e151a-fb0c-47c2-b08c-ef348f30980c'
           ,'4ebf7677-ea7c-404e-a633-1795e795feaf'
           ,'0440e29d-7d0e-4d48-acf4-3ec4353c4f9e'
           ,'80f6b724-c314-449f-8569-437837fae723'
           ,'cb511c9f-3f6d-445d-9cc3-a62392949a6d'
           ,'c0f43b29-6a95-4814-9c11-9812c2f62aa0'
           ,1
           ,5
           ,350
           ,GETDATE()
           ,GETDATE()
           ,'Insert'
           ,null
           ,GETDATE()
           ,'Insert')
