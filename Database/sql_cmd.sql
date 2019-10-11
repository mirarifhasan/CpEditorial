CREATE DATABASE CpEditorial;
USE CpEditorial;


CREATE TABLE [User] (
	UserID int IDENTITY(1001, 1) PRIMARY KEY ,
	UserName varchar (30) NOT NULL,
	Email varchar (30) NOT NULL,
	Password varchar (30) NOT NULL,
	Point int NOT NULL,
	UserType varchar (10) NOT NULL
);

INSERT INTO [User] VALUES ('Arif', 'arifishan@gmail.com', '1', 0, 'Admin')
INSERT INTO [User] VALUES ('Irina', 'inari55@gmail.com', '11', 0, 'Moderator');
INSERT INTO [User] VALUES ('Bill', 'billal201@gmail.com', '222', 0, 'Coach');
INSERT INTO [User] VALUES ('Qua', 'qua@gmail.com', '5555', 0, 'User');

SELECT * FROM [User];


CREATE TABLE Feedback (
	FeedbackId int IDENTITY(2001, 1) PRIMARY KEY,
	Name varchar (30) NOT NULL,
	Email varchar (30) NOT NULL,
	Message text NOT NULL
);

INSERT INTO Feedback VALUES ('Ripon', 'iamriponvideo@gmail.com', 'I want to be moderator');
INSERT INTO Feedback VALUES ('Rifat', 'rifisfat@gmail.com', 'Why I am having trouble posting editorial');

SELECT * FROM Feedback;


CREATE TABLE OnlineJudge (
	OJID int IDENTITY(3001, 1) PRIMARY KEY,
	OJName varchar (15) NOT NULL
);

INSERT INTO OnlineJudge VALUES ('Uva');
INSERT INTO OnlineJudge VALUES ('TopCoder');
INSERT INTO OnlineJudge VALUES ('HackerRank');
INSERT INTO OnlineJudge VALUES ('CoderByte');

SELECT * FROM OnlineJudge;


CREATE TABLE Tag (
	TagID int IDENTITY(4001, 1) PRIMARY KEY,
	Text varchar (20) NOT NULL
);

INSERT INTO Tag VALUES ('Strings');
INSERT INTO Tag VALUES ('Bit Manipulation');
INSERT INTO Tag VALUES ('Algorithms');
INSERT INTO Tag VALUES ('Data Structures');
INSERT INTO Tag VALUES ('Math');

SELECT * FROM Tag;


CREATE TABLE Problem (
	ProblemID int IDENTITY(5001, 1) PRIMARY KEY,
	OJID int FOREIGN KEY REFERENCES OnlineJudge(OJId),
	Title varchar (30) NOT NULL,
	ProblemCode varchar (20) NULL
);

INSERT INTO Problem VALUES ('3001', 'Max Sum',  1200);
INSERT INTO Problem VALUES ('3002', 'EOF',  null);
INSERT INTO Problem VALUES ('3003', 'Min value',  null);

SELECT * FROM Problem;


CREATE TABLE Editorial(
	EditorialID int IDENTITY(6001, 1) PRIMARY KEY,
	UserID int FOREIGN KEY REFERENCES [User](UserID),
	ProblemID int FOREIGN KEY REFERENCES Problem(ProblemID),
	TagID int FOREIGN KEY REFERENCES Tag(TagID),
	Rephrase text NULL,
	Solution text NULL,
	Details text NULL,
	UpVote int DEFAULT 0,
	DownVote int DEFAULT 0,
	DateOfPublishing datetime DEFAULT GETDATE()
);

INSERT INTO Editorial (UserID, ProblemID, TagID, Solution) VALUES (1001, 5001, 4001, 'This is a demo editorial');
INSERT INTO Editorial (UserID, ProblemID, TagID, Solution ) VALUES (1001, 5002, 4002, 'This is another demo editorial with a new line');

SELECT * FROM Editorial;


CREATE TABLE EditorialTags (
	EditorialId int FOREIGN KEY REFERENCES Editorial(EditorialId),
	TagId int FOREIGN KEY REFERENCES Tag(TagId)
);

INSERT INTO EditorialTags VALUES (6001, 4001);
INSERT INTO EditorialTags VALUES (6002, 4002);

SELECT * FROM EditorialTags;


CREATE TABLE Comment(
	CommentID int IDENTITY(7001, 1) PRIMARY KEY,
	UserID int NOT NULL FOREIGN KEY REFERENCES [User](UserID),
	EditorialID int NOT NULL FOREIGN KEY REFERENCES Editorial(EditorialID),
	ParentID int DEFAULT 0,
	Text text NOT NULL,
	UpVote int DEFAULT 0,
	DownVote int DEFAULT 0,
	DateOfPublishing datetime DEFAULT GETDATE()
);

INSERT INTO Comment (UserID, EditorialID, Text) VALUES(1001,6001,'Good Editorial');
INSERT INTO Comment (UserID, EditorialID, ParentId, Text) VALUES(1001,6001, 7001, 'It really is helpful');

SELECT * FROM Comment;


CREATE TABLE Report(
	ReportID int IDENTITY(9001, 1) PRIMARY KEY,
	UserID int FOREIGN KEY REFERENCES [User](UserID),
	PostID int NOT NULL,
	PostType varchar (10) NOT NULL,
	Text text NOT NULL
);

INSERT INTO Report VALUES(1001, 7001, 'Comment','Bad');
INSERT INTO Report VALUES(1001, 7001, 'Comment','Offensive');

SELECT * FROM Report;


CREATE TABLE Bookmark(
	BookmarkID int IDENTITY(10001, 1) PRIMARY KEY,
	UserID int FOREIGN KEY REFERENCES [User](UserID),
	EditorialID int FOREIGN KEY REFERENCES Editorial(EditorialID),  
);

INSERT INTO Bookmark VALUES(1002,6002);

SELECT * FROM Bookmark;




--Drop table commands
--DROP TABLE [User];
--DROP TABLE Feedback;
--DROP TABLE OnlineJudge;
--DROP TABLE Tag;
--DROP TABLE Problem;
--DROP TABLE Editorial;
--DROP TABLE EditorialTags;
--DROP TABLE Comment;
--DROP TABLE Report;
--DROP TABLE Bookmark;




