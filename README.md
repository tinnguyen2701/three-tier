add folder + add sln
 1. DataTier -> add project: DataTier.SqlServer
 2. ApplicationTier -> add 4 projects: ApplicationTier.Domain
 3. PresentationTier -> PresentationTier.AngularUI

1. add Tables, Views, Store Procedures, Scripts
   - them key identity cho Id
2. add Swashbuckle.AppNetCor, add cors("CorsPolicy")
   - run localhost:port/swagger
3. ng new PresentationTier.AngularUI + create project 
