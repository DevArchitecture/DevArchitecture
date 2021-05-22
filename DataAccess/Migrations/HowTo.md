// Eğer IDE'den örneğin staging'e migration yapılacaksa
$env:ASPNETCORE_ENVIRONMENT='Staging'
$env:ASPNETCORE_ENVIRONMENT='Production'


// PostgreSQL

$env:ASPNETCORE_ENVIRONMENT='Staging'
Add-Migration InitialCreate -Context ProjectDbContext -OutputDir Migrations/Pg
$env:ASPNETCORE_ENVIRONMENT='Staging'
Update-Database -context ProjectDbContext

dotnet ef migrations add InitialCreate --context ProjectDbContext --output-dir Migrations/Pg

// Ms Sql Server

$env:ASPNETCORE_ENVIRONMENT='Staging'
Add-Migration InitialCreate -context MsDbContext -OutputDir Migrations/Ms
$env:ASPNETCORE_ENVIRONMENT='Staging'
Update-Database -context MsDbContext

dotnet ef migrations add InitialCreate --context MsDbContext --output-dir Migrations/Ms

// Oracle 

$env:ASPNETCORE_ENVIRONMENT='Staging'
Add-Migration InitialCreate -context OracleDbContext -OutputDir Migrations/Ora
$env:ASPNETCORE_ENVIRONMENT='Staging'
Update-Database -context OracleDbContext

dotnet ef migrations add InitialCreate --context MsDbContext --output-dir Migrations/Ms


// Mysql 

$env:ASPNETCORE_ENVIRONMENT='Staging'
Add-Migration InitialCreate -context MySqlDbContext -OutputDir Migrations/Mysql
$env:ASPNETCORE_ENVIRONMENT='Staging'
Update-Database -context MySqlDbContext

dotnet ef migrations add InitialCreate --context MsDbContext --output-dir Migrations/Ms

/*
İlk şemayı yartmak için aşağıdaki drop gerekebilir.

drop table public."__EFMigrationsHistory";
drop table public."ContactPreferences";
drop table public."GroupClaims";
drop table public."OperationClaims";
drop table public."UserClaims";
drop table public."UserGroups";
drop table public."Logs";
drop table public."MobileLogins";
drop table public."Groups";
drop table public."Users";

select concat('drop table ',table_schema,'."',cast(table_name as varchar),'";') 
from INFORMATION_SCHEMA.TABLES
where table_schema='public';
*/