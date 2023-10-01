insert into Country (Name, CountryCode) Values('IND', '+91');
select * from ApplicationUsers;
delete from ApplicationUsers where UserName='as';
update ApplicationUsers set Role='Admin' where Username='arnab64g';
select * from ApplicationUser where Id = F45746A2-E6D7-465D-977F-9948FA742AF7
select * from Country;
delete from Country where Id = 6;
select * from Grades;
insert into Grades(GradeName, Points, MinNumber, MaxNumber) values ('F', 0.00, 0, 32)
select * from HealthConditions
ALTER TABLE HealthConditions ADD MeasuredTime DATETIME2 (7)  NOT NULL;
select * from INFORMATION_SCHEMA.TABLES;
select * from Semesters;
delete from Semesters where MonthBng = 51;
select * from Results;
update Semesters set SemesterName='Summer', Year=2018 where Id = 1010;
select CourseCode, CourseTitle, SemesterId, SemesterName, Year, GradeName, Points from (Results 
 Join Courses on Courses.Id = Results.CourseId 
 JOIN Semesters on Semesters.Id = Results.SemesterId
JOIN Grades on Results.GradeId = Grades.Id);

select sum(Points), sum(CreditPoint) from (Results 
Join Courses on Courses.Id = Results.CourseId 
 JOIN Semesters on Semesters.Id = Results.SemesterId
JOIN Grades on Results.GradeId = Grades.Id)
group by SemesterId;

select * from Courses;
select * from Grades;
 select Id from Results 
 group by SemesterId;

 backup database master 
 to disk = 'D:\Github\DailyDataAngular\userdb.bak';
