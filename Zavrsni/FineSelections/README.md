
# Fine Selections (ASP.NET Core MVC, .NET 8)

Premium web shop za žestoka pića inspiriran izgledom ecuga.com.

## Brzi start

1. **Instaliraj .NET 8 SDK** i (opcionalno) EF Core CLI.
2. U konzoli:
   ```bash
   dotnet restore
   dotnet ef migrations add Initial --project .
   dotnet ef database update --project .
   dotnet run
   ```
3. Prvi start će:
   - Kreirati tablice (Identity + `proizvodi`)
   - Učitati proizvode iz `Data/proizvodi_insert.sql`
   - Napraviti korisnike:
     - Admin: `admin@fineselections.local` / `Admin!2345`
     - User:  `user@fineselections.local` / `User!2345`

## Napomene

- Baza: SQLite (`app.db`). Promijeni u `appsettings.json` ako želiš drugi DB.
- Admin CRUD: `/Products/Admin` (samo za ulogu **Admin**).
- Front inspiriran ecuga.com: tamna paleta, hero sekcija, grid kartice.
