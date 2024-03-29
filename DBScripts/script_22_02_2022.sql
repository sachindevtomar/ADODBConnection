USE [tempdb]
GO
/****** Object:  Table [dbo].[Employees]    Script Date: 2/22/2022 12:35:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Employees](
	[Name] [nvarchar](50) NOT NULL,
	[Gender] [nvarchar](50) NOT NULL,
	[Department] [nvarchar](50) NULL,
	[City] [nvarchar](50) NULL,
	[Id] [int] IDENTITY(1,1) NOT NULL
) ON [PRIMARY]
GO
SET IDENTITY_INSERT [dbo].[Employees] ON 

INSERT [dbo].[Employees] ([Name], [Gender], [Department], [City], [Id]) VALUES (N'Sachin', N'M', N'Technology', N'Agra', 1)
INSERT [dbo].[Employees] ([Name], [Gender], [Department], [City], [Id]) VALUES (N'Mohit', N'M', N'Technology', N'Agra', 2)
INSERT [dbo].[Employees] ([Name], [Gender], [Department], [City], [Id]) VALUES (N'Rashi', N'M', N'HR', N'Lucknow', 3)
INSERT [dbo].[Employees] ([Name], [Gender], [Department], [City], [Id]) VALUES (N'Rohit', N'M', N'Technology', N'Kanpur', 4)
INSERT [dbo].[Employees] ([Name], [Gender], [Department], [City], [Id]) VALUES (N'Raghinmi', N'F', N'HR', N'Agra', 5)
SET IDENTITY_INSERT [dbo].[Employees] OFF
GO
/****** Object:  StoredProcedure [dbo].[usp_AddEmployee]    Script Date: 2/22/2022 12:35:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_AddEmployee] 
@EmployeeName nvarchar(50),  
@Gender nvarchar(50),  
@Department nvarchar(50),  
@City nvarchar(50)  
AS  
BEGIN  
	INSERT INTO Employees(  
	Name,  
	Gender,  
	Department,
	City)  
	VALUES (   
	@EmployeeName,  
	@Gender,  
	@Department,
	@City)  
END  
GO
/****** Object:  StoredProcedure [dbo].[usp_DeleteEmployee]    Script Date: 2/22/2022 12:35:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_DeleteEmployee] 
@EmpId int
AS  
BEGIN  
	DELETE FROM Employees 
	WHERE Id = @EmpId;
END  
GO
/****** Object:  StoredProcedure [dbo].[usp_GetAllEmployees]    Script Date: 2/22/2022 12:35:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_GetAllEmployees] 
AS  
BEGIN  
	SELECT  * 
	FROM Employees  
END  
GO
/****** Object:  StoredProcedure [dbo].[usp_UpdateEmployee]    Script Date: 2/22/2022 12:35:57 AM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[usp_UpdateEmployee] 
@EmpId int,  
@EmployeeName nvarchar(50),  
@Gender nvarchar(50),  
@Department nvarchar(50),  
@City nvarchar(50)  
AS  
BEGIN  
	UPDATE Employees 
	SET Name = @EmployeeName,
		Department = @Department,
		Gender = @Gender,
		City = @City
	WHERE Id = @EmpId

END  
GO
