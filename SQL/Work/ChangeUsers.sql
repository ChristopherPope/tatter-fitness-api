USE [master]
GO
CREATE LOGIN [IIS APPPOOL\TatterFitness] FROM WINDOWS WITH DEFAULT_DATABASE=[master]
GO
USE [TATTER-FITNESS]
GO


CREATE USER [IIS APPPOOL\TatterFitness] FOR LOGIN [IIS APPPOOL\TatterFitness] WITH DEFAULT_SCHEMA=[dbo]
CREATE USER [IIS APPPOOL\TatterFitnessVideos] FOR LOGIN [IIS APPPOOL\TatterFitnessVideos] WITH DEFAULT_SCHEMA=[dbo]

go
sp_addRoleMember @roleName = 'db_datawriter', @memberName = 'IIS APPPOOL\TatterFitness'
go
sp_addRoleMember @roleName = 'db_datareader', @memberName = 'IIS APPPOOL\TatterFitness'
go
sp_addRoleMember @roleName = 'db_datareader', @memberName = 'IIS APPPOOL\TatterFitnessVideos'
go
