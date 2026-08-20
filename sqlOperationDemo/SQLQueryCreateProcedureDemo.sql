USE StudentDB;
GO

CREATE PROCEDURE GetStudentByID1
    @StudentID INT
AS
BEGIN
    SELECT *
    FROM Student
    WHERE StudentID = @StudentID;
END;
GO
EXEC GetStudentByID1 @StudentID = 1;