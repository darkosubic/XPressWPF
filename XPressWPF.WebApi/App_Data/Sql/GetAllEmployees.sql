SELECT em.[Id] 
,	em.[FirstName]	
,	em.[LastName] 
,	em.[Age] 
,	em.[Salary] 
,	em.[DepartmentId] 
,	dp.[Name] as DepartmentName 
FROM 
	[DapperTutorialDB].[dbo].[Employee] AS em 
LEFT JOIN 
	[dbo].[Department] dp on em.DepartmentId = dp.Id