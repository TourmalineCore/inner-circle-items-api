# inner-circle-items-api

## Migrations

### Adding a new migration

Run the database using docker compose executing the following script
```bash
docker compose --profile db-only up -d
```

#### Visual Studio 
After making changes to the model and AppDbContext open Tools -> NuGet Package Manager -> Package Manager Console

Execute the following with your migration name
```bash
Add-Migration <YOUR_MIGRATION_NAME> -Project Application
```

To apply migration run the following:
```bash
Update-Database -Project Application
```

#### VS Code

Open project in the devcontainer and open Terminal -> New Terminal

Execute the following with your migration name
```bash
dotnet ef migrations add <YOUR_MIGRATION_NAME> --startup-project Api/ --project Application/ -- --environment MockForDevelopment
```

To apply migration run the following:
```bash
dotnet ef database update --startup-project Api/ --project Application/ -- --environment MockForDevelopment
```
