UPDATE 
	dbo.Employee 
SET 
	FirstName = @FirstName
,	LastName = @LastName,
Age = @Age,
Salary = @Salary 
WHERE 
	Id = @Id