# inner-circle-items-api

## Migrations

### Adding a new migration  (Windows via Visual Studio)

Run the database using docker compose executing the following script
```bash
docker compose --profile db-only up -d
```

After making changes to the model and AppDbContext open Tools -> NuGet Package Manager -> Package Manager Console

Execute the following with your migration name
```bash
Add-Migration <YOUR_MIGRATION_NAME> -Project Application
```

To apply migration run the following:
```bash
Update-Database -Project Application
```


## Tests

### Running karate tests in dev-container

Run the docker compose with MockForPullRequest profile executing the following command

```bash
docker compose --profile MockForPullRequest up -d
```

Open VS Code for the `inner-circle-items-api` repo to use dev-container

Execute following command inside of the dev-container
```bash
java -jar /karate.jar .
```
## Databse Schema


### Database schema which covers every USM

The scheme below need to be implemented and might be changed

We do not use own index for `Delisted` table because the `Item` may be delisted only once and we can use the `ItemId` to store and access details for delisted records

Since we may have a repair history for each `Item` we need very own index for every `Repair` record

Problems: 
* It's not clear if the `Status` should be an separate table or hardcoded values, will we have more statuses? Why hardcoded values? 

* Afterwards, should we update contract and add required endpoints? 

```mermaid
UserDiagram
    ItemTypes ||--o{ Item : "1-to-many"
    Status ||--o{ Item : "1-to-many"
    Item ||--|| DelistedItemRecord : "1-to-1"
    Item ||--o{ BrokenItemRecord : "1-to-many"
    
    Item {
        long Id PK
        string Name
        string SerialNumber
        long ItemTypeId FK
        double Price
        DateOnly PurchaseDate
        long HolderId FK
        Status Status FK
        bool IsDeleted
    }
    ItemTypes {
        long Id
        string Name
    }
    BrokenItemRecord {
        long Id PK
        long ItemId FK
        string Description
        DateOnly DateOfBroke
        DateOnly RepairDate
        bool IsFinished
    }
    DelistedItemRecord {
        long ItemId PK
        string Description
        DateOnly Date
    }
    Status {
        Status Status
    }
```