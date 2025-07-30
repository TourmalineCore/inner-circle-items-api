# inner-circle-items-api

## Migrations

### Adding a new migration  (Windows via Visual Studio)

Run the database using doocker compose executing the following script (don't close the terminal unless you want to stop the containers)
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
