CREATE DATABASE SQLQueryPractice;
GO

Use SQLQueryPractice;
GO

BEGIN TRANSACTION;

--	CREATE TABLE'S

-- Create Departments table
CREATE TABLE Departments (
    department_id INT PRIMARY KEY,
    department_name VARCHAR(100),
    location VARCHAR(100)
);

-- Create Employees table
CREATE TABLE Employees (
    employee_id INT PRIMARY KEY,
    first_name VARCHAR(50),
    last_name VARCHAR(50),
    department_id INT,
    salary DECIMAL(10, 2),
    hire_date DATE,
    manager_id INT NULL,
    FOREIGN KEY (department_id) REFERENCES Departments(department_id),
    FOREIGN KEY (manager_id) REFERENCES Employees(employee_id)
);

-- Create Customers table
CREATE TABLE Customers (
    customer_id INT PRIMARY KEY,
    customer_name VARCHAR(100),
    country VARCHAR(50)
);

-- Create Orders table
CREATE TABLE Orders (
    order_id INT PRIMARY KEY,
    customer_id INT,
    order_date DATE,
    total_amount DECIMAL(10, 2),
    FOREIGN KEY (customer_id) REFERENCES Customers(customer_id)
);

ROLLBACK TRANSACTION;
-- COMMIT TRANSACTION;

BEGIN TRANSACTION;

-- ADD SAMPLE DATA

-- Insert Departments
INSERT INTO Departments (department_id, department_name, location) VALUES
(1, 'HR', 'New York'),
(2, 'IT', 'San Francisco'),
(3, 'Finance', 'Chicago'),
(4, 'Marketing', 'Boston');

-- Insert Employees
INSERT INTO Employees (employee_id, first_name, last_name, department_id, salary, hire_date, manager_id) VALUES
(101, 'John', 'Doe', 2, 80000.00, '2019-05-10', NULL),
(102, 'Jane', 'Smith', 2, 75000.00, '2020-07-15', 101),
(103, 'Alice', 'Brown', 1, 60000.00, '2021-03-20', 101),
(104, 'Bob', 'Johnson', 3, 70000.00, '2018-11-01', NULL),
(105, 'Charlie', 'Davis', 4, 65000.00, '2022-01-12', 104),
(106, 'Eve', 'Wilson', 3, 72000.00, '2020-06-23', 104),
(107, 'David', 'Lee', 2, 78000.00, '2023-02-05', 101),
(108, 'Grace', 'Kim', 4, 63000.00, '2021-09-14', 105);

-- Insert Customers
INSERT INTO Customers (customer_id, customer_name, country) VALUES
(201, 'Acme Corp', 'USA'),
(202, 'Global Tech', 'Canada'),
(203, 'Future Solutions', 'UK'),
(204, 'Visionary Ltd', 'Australia'),
(205, 'Innovatech', 'USA'),
(206, 'Bright Minds', 'Germany');

-- Insert Orders
INSERT INTO Orders (order_id, customer_id, order_date, total_amount) VALUES
(301, 201, '2024-01-15', 1500.00),
(302, 202, '2024-02-20', 2000.00),
(303, 203, '2024-03-05', 1750.50),
(304, 201, '2024-03-18', 2200.00),
(305, 204, '2024-04-12', 980.00),
(306, 205, '2024-04-25', 1450.00),
(307, 206, '2024-05-07', 3100.75),
(308, 202, '2024-05-20', 2650.00),
(309, 203, '2024-06-01', 1980.00),
(310, 201, '2024-06-15', 1250.00);

ROLLBACK TRANSACTION;
-- COMMIT TRANSACTION;

--Step 1

Select * from Departments;
Select * from Employees;
Select * from Customers;
Select * from Orders;

--Step 2 employees in IT Deptmt

Select employee_id, first_name, last_name 
from Employees
Where department_id = '2';

--step 3 sort employees by salary

Select first_name, last_name, salary 
from Employees
Order by salary desc;

--step 4 Count employees in each department

select department_id, count(employee_id) as Total_Employees --can use * instead of employee_id
from Employees
group by department_id;

--step 5 Join Employees and Departments

select e.employee_id, e.first_name, e.last_name, d.department_name, d.location, e.hire_date
from Employees e INNER JOIN Departments d on e.department_id = d.department_id; 

--step 6 Find employees with above-average salary

Select first_name, last_name, salary 
from Employees
Where salary >= (select AVG(salary) from Employees);

--step 7 Get the second highest salary

select MAX(salary) AS Second_Max_Salary
from Employees 
Where salary < (Select Max(salary) from Employees);

	-- Get the top 3 employees with highest salary
	select top 3 first_name, last_name, salary
	from Employees
	order by salary desc;

	--- Works in other sql servers except MS SQL Server (ex: MySQL, PostgreSQL, SQLite)
	--select first_name, last_name, salary
	--from Employees
	--order by salary desc
	--LIMIT 3;


--step 8 List all employees and their managers
/*
	Can use Self-join to match employees with their managers
*/

SELECT e1.first_name AS employee_name, e2.first_name AS manager_name
FROM Employees e1
LEFT JOIN Employees e2 ON e1.manager_id = e2.employee_id;

--Omits employees without managers
SELECT e1.first_name AS employee_name, e2.first_name AS manager_name
FROM Employees e1
INNER JOIN Employees e2 ON e1.manager_id = e2.employee_id;

--step 9 Rank employees by salary within their department

--Select first_name, department_id, salary 
--From Employees
--Group by department_id, first_name, salary
--Order by salary desc;

/*
	Using a window function (RANK) to assign ranks to employees within each 
	department based on salary.
*/
SELECT first_name, last_name, department_id, salary,
RANK() OVER (PARTITION BY department_id ORDER BY salary DESC) as salary_rank
FROM Employees;

--step 10 Find duplicate employee records
/*
	Identifies duplicate records by grouping and using HAVING to filter groups 
	with more than one occurrence.
*/

-- Insert a duplicte data
--INSERT INTO Employees (employee_id, first_name, last_name, department_id, salary, hire_date, manager_id) 
--VALUES (109, 'Grace', 'Kim', 4, 63000.00, '2021-09-14', 105);  --delete from Employees where employee_id = 109;

SELECT first_name, last_name, COUNT(*) as count
FROM Employees
GROUP BY first_name, last_name
HAVING COUNT(*) > 1;

--step 10.1 Remove duplicate records from table

DELETE FROM Employees
WHERE employee_id NOT IN (SELECT MIN(employee_id) FROM Employees GROUP BY first_name, last_name, department_id);

--step 11 Running total of orders by customer

--Total
Select c.customer_name, 
COUNT(o.order_id) As Order_Count, SUM(o.total_amount) AS Total_Amount
From Customers c INNER JOIN Orders o ON c.customer_id = o.customer_id
Group by c.customer_name;

--Running Total
SELECT customer_id, order_id, order_date, total_amount,
SUM(total_amount) OVER (PARTITION BY customer_id ORDER BY order_date) as running_total
FROM Orders;

--step 12 Find employees who earn more than their manager

SELECT e1.first_name AS employee_name, e1.salary, 
e2.first_name AS manager_name, e2.salary
FROM Employees e1
INNER JOIN Employees e2 ON e1.manager_id = e2.employee_id
Where e1.salary > e2.salary;

--step 13 Get top 3 employees by salary per department

WITH RankedEmployees AS (
SELECT first_name, last_name, department_id, salary,
DENSE_RANK() OVER (PARTITION BY department_id ORDER BY salary DESC) as rnk
FROM Employees)
SELECT first_name, last_name, department_id, salary
FROM RankedEmployees
WHERE rnk <= 3;

--step 14 Pivot department salary totals by location

SELECT *
FROM (
    SELECT d.location, d.department_name, e.salary
    FROM Employees e
    INNER JOIN Departments d ON e.department_id = d.department_id
) AS SourceTable
PIVOT (
    SUM(salary)
    FOR department_name IN ([IT], [Finance], [HR])) AS PivotTable;

--step 15 Find employees hired in the last 6 months

-- Insert a new employee data
--INSERT INTO Employees (employee_id, first_name, last_name, department_id, salary, hire_date, manager_id) 
--VALUES (109, 'Dan', 'Millman', 1, 100000.00, '2025-06-14', 105);  --delete from Employees where employee_id = 109;

SELECT first_name, last_name, hire_date
FROM Employees
WHERE hire_date >= DATEADD(MONTH, -6, GETDATE());

--step 16 Find customers with no orders
SELECT c.customer_id, c.customer_name
FROM Customers c
LEFT JOIN Orders o ON c.customer_id = o.customer_id
WHERE o.order_id IS NULL;

--step 17 Optimize a slow query 
--(Get Employees details with department name whose salary above 50000)

/*
	Demonstrates converting an implicit join to an explicit INNER JOIN, 
	adding an EXISTS clause for optimization, 
	and suggesting an index to improve performance.
*/

-- Original slow query
SELECT e.first_name, e.last_name, d.department_name
FROM Employees e, Departments d
WHERE e.department_id = d.department_id
AND e.salary > 70000;

-- Optimized query

BEGIN TRANSACTION;

--CREATE INDEX idx_salary_department
--ON Employees(salary, department_id);

SELECT e.first_name, e.last_name, d.department_name
FROM Employees e
INNER JOIN Departments d ON e.department_id = d.department_id
WHERE e.salary > 70000
AND EXISTS (
    SELECT 1 FROM Departments d2 WHERE d2.department_id = e.department_id
);

DROP INDEX idx_salary_department ON Employees;

ROLLBACK TRANSACTION;
-- COMMIT TRANSACTION;

-- Views

BEGIN TRANSACTION;
GO
-- 1. Employee details with department info and manager name

CREATE OR ALTER VIEW vw_EmployeeDetails AS
SELECT 
    e.employee_id,
    e.first_name,
    e.last_name,
    d.department_name,
    e.salary,
    e.hire_date,
    CONCAT(m.first_name, ' ', m.last_name) AS manager_name
FROM Employees e
LEFT JOIN Departments d ON e.department_id = d.department_id
LEFT JOIN Employees m ON e.manager_id = m.employee_id;
GO
-------------------------------------------------------------

-- 2. Total salary cost per department
CREATE OR ALTER VIEW vw_DepartmentSalarySummary AS
SELECT 
    d.department_name,
    SUM(e.salary) AS total_salary,
    AVG(e.salary) AS avg_salary,
    COUNT(e.employee_id) AS employee_count
FROM Departments d
JOIN Employees e ON d.department_id = e.department_id
GROUP BY d.department_name;
GO
-------------------------------------------------------------

-- 3. Customer orders with total spend
CREATE OR ALTER VIEW vw_CustomerOrderSummary AS
SELECT 
    c.customer_id,
    c.customer_name,
    c.country,
    COUNT(o.order_id) AS total_orders,
    SUM(o.total_amount) AS total_spent
FROM Customers c
LEFT JOIN Orders o ON c.customer_id = o.customer_id
GROUP BY c.customer_id, c.customer_name, c.country;
GO

ROLLBACK TRANSACTION;
-- COMMIT TRANSACTION;

-- Stored Procedures

BEGIN TRANSACTION;
GO
-- 1. Get employees by department
CREATE OR ALTER PROCEDURE sp_GetEmployeesByDepartment
    @DeptId INT
AS
BEGIN
    SET NOCOUNT ON;

    SELECT e.employee_id, e.first_name, e.last_name, e.salary, d.department_name
    FROM Employees e
    INNER JOIN Departments d ON e.department_id = d.department_id
    WHERE e.department_id = @DeptId;
END;
GO

-- Execution:
EXEC sp_GetEmployeesByDepartment @DeptId = 2;
GO
-------------------------------------------------------------

-- 2. Insert new order and return updated customer spend
CREATE OR ALTER PROCEDURE sp_AddOrderAndGetTotal
    @CustId INT,
    @OrderDate DATE,
    @Amount DECIMAL(10,2)
AS
BEGIN
    SET NOCOUNT ON;

    -- Table variable to capture the new order id
    DECLARE @NewOrders TABLE (order_id INT);

    -- Insert order and capture its ID
    INSERT INTO Orders (customer_id, order_date, total_amount)
    OUTPUT INSERTED.order_id INTO @NewOrders(order_id)
    VALUES (@CustId, @OrderDate, @Amount);

    -- (Optional) You can get the new order id like this: SELECT order_id FROM @NewOrders;

    -- Return updated customer total
    SELECT c.customer_id, c.customer_name,
           SUM(o.total_amount) AS total_spent
    FROM Customers c
    INNER JOIN Orders o ON c.customer_id = o.customer_id
    WHERE c.customer_id = @CustId
    GROUP BY c.customer_id, c.customer_name;
END;
GO


-- Execution:
EXEC sp_AddOrderAndGetTotal 
     @CustId = 201, 
     @OrderDate = '2024-07-10', 
     @Amount = 1800.00;
GO
-- Triggers

-- 1. Prevent salary below 30,000
CREATE OR ALTER TRIGGER trg_CheckSalaryBeforeInsert
ON Employees
INSTEAD OF INSERT
AS
BEGIN
    IF EXISTS (SELECT 1 FROM inserted WHERE salary < 30000)
    BEGIN
        RAISERROR('Salary must be at least 30,000', 16, 1);
        ROLLBACK TRANSACTION;
        RETURN;
    END

    -- Insert valid rows
    INSERT INTO Employees (employee_id, first_name, last_name, department_id, salary, hire_date, manager_id)
    SELECT employee_id, first_name, last_name, department_id, salary, hire_date, manager_id
    FROM inserted;
END;
GO

ROLLBACK TRANSACTION;
-- COMMIT TRANSACTION;
-------------------------------------------------------------

BEGIN TRANSACTION;

-- 2. Audit order insertions
CREATE TABLE Order_Audit (
    audit_id INT IDENTITY(1,1) PRIMARY KEY,
    order_id INT,
    customer_id INT,
    action_time DATETIME DEFAULT GETDATE(),
    action VARCHAR(20)
);
GO

CREATE OR ALTER TRIGGER trg_AuditOrderInsert
ON Orders
AFTER INSERT
AS
BEGIN
    INSERT INTO Order_Audit (order_id, customer_id, action)
    SELECT order_id, customer_id, 'INSERT'
    FROM inserted;
END;
GO

-------------------------------------------------------------

-- 3. Track salary updates in a history table
CREATE TABLE Salary_History (
    history_id INT IDENTITY(1,1) PRIMARY KEY,
    employee_id INT,
    old_salary DECIMAL(10,2),
    new_salary DECIMAL(10,2),
    change_date DATETIME DEFAULT GETDATE()
);
GO

CREATE OR ALTER TRIGGER trg_SalaryUpdate
ON Employees
AFTER UPDATE
AS
BEGIN
    IF UPDATE(salary)
    BEGIN
        INSERT INTO Salary_History (employee_id, old_salary, new_salary)
        SELECT d.employee_id, d.salary, i.salary
        FROM deleted d
        INNER JOIN inserted i ON d.employee_id = i.employee_id
        WHERE d.salary <> i.salary;
    END
END;
GO

ROLLBACK TRANSACTION;
-- COMMIT TRANSACTION;

------------------------- Indexing --------------------------------

BEGIN TRANSACTION;

--- Exection time is measurabally affected on large data sets

--1 index on frequently searched columns

CREATE NONCLUSTERED INDEX idx_dept_salary
ON Employees(department_id, salary); 

SELECT first_name, last_name, salary
FROM Employees
WHERE department_id = 2 AND salary > 70000;

--2 index on foreign key for better JOIN perforamnce

CREATE NONCLUSTERED INDEX idx_orders_customer
ON Orders(customer_id);

select c.customer_name, o.order_id, o.order_date, o.total_amount
from Customers c inner join Orders o 
ON c.customer_id = o.customer_id
where c.country = 'USA';

--3 covering index (includes all columns needed in a query)

CREATE NONCLUSTERED INDEX idx_employee_covering
ON Employees(department_id, salary)
INCLUDE (first_name, last_name, hire_date);

SELECT first_name, last_name, salary, hire_date
FROM Employees
WHERE department_id = 2 AND salary BETWEEN 60000 AND 80000;

--4 filtered index

CREATE NONCLUSTERED INDEX idx_high_salary_employees
ON Employees(salary)
Where salary > 70000;

SELECT employee_id, first_name, last_name, salary
From Employees
Where salary > 75000;

--5 index for range queries (efficient)

CREATE NONCLUSTERED INDEX idx_order_date 
ON Orders(order_date)
INCLUDE (total_amount);

SELECT order_id, order_date, total_amount
FROM Orders
WHERE order_date BETWEEN '2024-01-01' AND '2024-06-30';

--6 unique index to enforce uniqueness 
--(prevents duplicate entries with same name & hire date)

CREATE UNIQUE NONCLUSTERED INDEX idx_unique_employee_email 
ON Employees(first_name, last_name, hire_date);

	
ROLLBACK TRANSACTION;
-- COMMIT TRANSACTION;