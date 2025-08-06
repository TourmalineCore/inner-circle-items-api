# inner-circle-items-api

## Migrations

### Adding a new migration  (Windows via Visual Studio)

Run the database using docker compose executing the following script (don't close the terminal unless you want to stop the containers)
```bash
docker compose --profile db-only up --build
```
>Note: `--build` gurantees that we run the latest code after re-applying the script

After making changes to the model and AppDbContext open Tools -> NuGet Package Manager -> Package Manager Console

If you want to use 'Update-Database' commands to apply migrations to the database please execute following command in Package Manager Console every time you open the solution.
```bash
$env:ASPNETCORE_ENVIRONMENT = 'MockForDevelopment';
```

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

## Database Schema

```mermaid
erDiagram
    ItemTypes ||--o{ Item : "1-to-many"
    
    Item {
        long Id PK
        long TenantId
        string Name
        string SerialNumber "nullable"
        long ItemTypeId FK
        decimal Price
        string Description
        DateOnly PurchaseDate "nullable (possibly will be changed to NodaTime)"
        long HolderId FK "nullable"
    }
    ItemTypes {
        long Id
        string Name
    }
