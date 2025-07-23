# inner-circle-items-api

## Migrations

### Adding a new migration

Run the database using doocker compose executing the following script
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
