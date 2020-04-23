docker run -d -p 1433:1433 --name awesome \
  -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=P@55word' \
  microsoft/mssql-server-linux:2017-latest


