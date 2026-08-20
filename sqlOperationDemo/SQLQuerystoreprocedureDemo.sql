USE StudentDB;
GO

CREATE PROCEDURE AddStudent
    @StudentName VARCHAR(100),
    @Email VARCHAR(100),
    @Phone VARCHAR(15),
    @Course VARCHAR(100),
    @Marks INT,
    @City VARCHAR(50)
AS
BEGIN
    INSERT INTO Student
    (
        StudentName,
        Email,
        Phone,
        Course,
        Marks,
        City
    )
    VALUES
    (
        @StudentName,
        @Email,
        @Phone,
        @Course,
        @Marks,
        @City
    );
END;
GO
EXEC AddStudent
    @StudentName = 'Madhura',
    @Email = 'm@gmail.com',
    @Phone = '9876542460',
    @Course = 'B.Tech CSE',
    @Marks = 87,
    @City = 'Satara';

	SELECT * FROM Student;


