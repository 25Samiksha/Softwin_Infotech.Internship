USE StudentDB;
GO

CREATE PROCEDURE UpdateStudent
    @StudentID INT,
    @StudentName VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Course VARCHAR(100),
    @Marks INT,
    @City VARCHAR(50)
AS
BEGIN
    UPDATE Student
    SET
        StudentName = @StudentName,
        Email = @Email,
        Phone = @Phone,
        Course = @Course,
        Marks = @Marks,
        City = @City
    WHERE StudentID = @StudentID;
END;
GO
EXEC UpdateStudent
    @StudentID = 1,
    @StudentName = 'Samiksha',
    @Email = 'samiksha@gmail.com',
    @Phone = '8275747525',
    @Course = 'B.Tech CSE',
    @Marks = 95,
    @City = 'Sangli';

SELECT *FROM Student
WHERE StudentID = 1;