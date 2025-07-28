# inner-circle-items-api

## Migrations

### Adding a new migration  (Windows via Visual Studio)

Run the database using doocker compose executing the following script (don't close the terminal unless you want to stop the containers)
```bash
docker compose --profile db-only up --build
```
>Note: `--build` gurantees that we run the latest code after re-applying the script

After making changes to the model and AppDbContext open Tools -> NuGet Package Manager -> Package Manager Console

Execute the following with your migration name
```bash
Add-Migration <YOUR_MIGRATION_NAME> -Project Application
```

To apply migration run the following:
```bash
Update-Database -Project Application
```
