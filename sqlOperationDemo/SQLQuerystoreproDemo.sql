USE StudentDB;
GO

CREATE PROCEDURE GetAllStudents
AS
BEGIN
    SELECT *
    FROM Student;
END;
GO
EXEC GetAllStudents;