USE StudentDB;
GO
SELECT *FROM Student;

SELECT StudentName, Course, Marks
FROM Student;

SELECT *FROM Student
WHERE City = 'Sangli';

SELECT *FROM Student
WHERE City <> 'Sangli';

SELECT *FROM Student
WHERE Marks > 80;

SELECT *FROM Student
WHERE Marks >= 85;

SELECT *FROM Student
WHERE Marks < 85;

SELECT *FROM Student
WHERE Marks <= 80;

SELECT *FROM Student
WHERE City = 'Sangli'
AND Marks > 80;

SELECT *FROM Student
WHERE City = 'Sangli'
OR City = 'Pune';

SELECT *FROM Student
WHERE NOT City = 'Sangli';

SELECT *FROM Student
WHERE Marks BETWEEN 80 AND 90;

SELECT *FROM Student
WHERE Marks NOT BETWEEN 80 AND 90;

SELECT *FROM Student
WHERE City IN ('Sangli', 'Pune', 'Satara');

SELECT *FROM Student
WHERE City NOT IN ('Sangli', 'Pune');

SELECT *FROM Student
WHERE StudentName LIKE 'S%';

SELECT *FROM Student
WHERE StudentName LIKE '%a';

SELECT *FROM Student
WHERE StudentName LIKE '%an%';

SELECT *FROM Student
WHERE StudentName LIKE '_a%';

SELECT *FROM Student
WHERE StudentName NOT LIKE 'S%';

SELECT *FROM Student
WHERE Email IS NULL;

SELECT *FROM Student
WHERE Email IS NOT NULL;

SELECT *FROM Student
ORDER BY Marks ASC;

SELECT *FROM Student
ORDER BY Marks DESC;

SELECT *FROM Student
ORDER BY StudentName ASC;

SELECT MAX(Marks) AS HighestMarks
FROM Student;

SELECT MIN(Marks) AS LowestMarks
FROM Student;

SELECT AVG(Marks) AS AverageMarks
FROM Student;

SELECT COUNT(*) AS TotalStudents
FROM Student;

SELECT SUM(Marks) AS TotalMarks
FROM Student;

SELECT Course, COUNT(*) AS TotalStudents
FROM Student
GROUP BY Course;

SELECT Course, AVG(Marks) AS AverageMarks
FROM Student
GROUP BY Course;

SELECT *FROM Student;

UPDATE Student
SET Marks = 95
WHERE StudentID = 1;

SELECT *
FROM Student
WHERE StudentID = 1;

SELECT *
FROM Student
WHERE StudentID = 5;


