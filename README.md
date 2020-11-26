### Cài đặt project

```sh
    git clone https://github.com/quocduvp/rating-review-example.git
    
    cd rating-review-example
    
    click file rating-review-example.sln open visual code
```

#### Update connectionString

1. Mở file Web.config update `connectionStrings`

```xml
  <connectionStrings>
    <add name="DbContext" providerName="System.Data.SqlClient" connectionString="***********************" />
  </connectionStrings>
```

#### Migration database

```sh
    open Nuget console: Tools > Nuget Package Manager > Nuget Package Console
    chạy lệnh cmd:
    PM> enable-migrations
    PM> Add-Migration InitDatabase
    PM> Update-Database
```


### API Document

[Postman](/api_document.postman_collection.json)