Утилита предназначена для просмотра метаданных прикладной разработки DirectumRX. 
Утилита поддерживает выгрузку метаданных как из БД, так и из папки с исходниками. 

Поддерживается фильтрация дерева по началу названия соответствующего узла.
Поддерживается поиск по всему дереву метаданных.

Утилита выстраивает дерево метаданных с учетом иерархии наследования и перекрытий. На верхнем уровне используется группировка по типу сущности.

Для визуализации JSON с метаданными используется контрол Scintilla.NET.

Пример файла настроек для работы с папками разработки:

{
  "developmentFolders": [
    "d:\\Projects\\base",
    "d:\\Projects\\work"
  ]
}

Пример файла настроек для получения разработки из БД на MSSQL:

{
  "developmentFolders": [],
  "connectionString": "Data Source=server;Initial Catalog=Database;User ID=user;Pwd=password;Persist Security Info=True;Trusted_Connection=False",
  "databaseEngine": "mssql"
}

Пример файла настроек для получение разработки из БД на Postgres:

{
  "developmentFolders": [],
  "connectionString": "Server=server;Database=Database;User ID=user;Password=password;Port=5432;Client Encoding=UTF8",
  "databaseEngine": "postgres",
}
