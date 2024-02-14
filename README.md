# HamRadioStudy

## Migrations

Go to `Tools > NuGet Package Manager > Package Manager Console` and run the following command to create a new migration:

```sh
Add-Migration Questions -Context ApplicationDbContext -StartupProject HamRadioStudy
```

To apply the migration, run the following command:

```sh
Update-Database -Context ApplicationDbContext -StartupProject HamRadioStudy
```
