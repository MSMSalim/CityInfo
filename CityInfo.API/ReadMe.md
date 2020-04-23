docker run -d -p 1433:1433 --name awesome \
  -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=P@55word' \
  microsoft/mssql-server-linux:2017-latest


dotnet ef migrations add initialcreate -s CityInfo.API.csproj

dotnet ef database update -s CityInfo.API.csproj


add new property to entity object and then

dotnet ef migrations add CityInfoDbAddPointOfInterestDesription -s CityInfo.API.csproj

it will create a new script in migration folder called CityInfoDbAddPointOfInterestDesription.cs

then do below to apply the migration

dotnet ef database update CityInfoDbAddPointOfInterestDesription -s CityInfo.API.csproj