using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace CpEditorial.Models
{
    public class DbSchema
    {
        public User GetUser(int userId)
        {
            return new User(userId);
        }

        public Feedback GetFeedback(int feedbackId)
        {
            return new Feedback(feedbackId);
        }

        public OnlineJudge GetOnlineJudge(int ojId)
        {
            return new OnlineJudge(ojId);
        }

        public Tag GetTag(int tagId)
        {
            return new Tag(tagId);
        }


        public Problem GetProblem(int problemId)
        {
            return new Problem(problemId);
        }


        public Editorial GetEditorial(int editorialId)
        {
            return new Editorial(editorialId);
        }
    }
    /**
        CREATE TABLE [User] (
        UserID int IDENTITY(1001, 1) PRIMARY KEY ,
        UserName varchar (30) NOT NULL,
        Email varchar (30) NOT NULL,
        Password varchar (30) NOT NULL,
        Point int NOT NULL,
        UserType varchar (10) NOT NULL
        );
    **/

    public class User
    {
        int userId;
        string userName;
        string email;
        string password;
        int point;
        string userType;

        public User(int userId)
        {
            this.userId = userId;
            string query = "select * from user table where userid = " + userId;
            DataTable dataTable = new DBHelper().getTable(query);
            int i = 1;
            this.userName = Convert.ToString(dataTable.Rows[0][i++]);
            this.email = Convert.ToString(dataTable.Rows[0][i++]);
            this.password = Convert.ToString(dataTable.Rows[0][i++]);
            this.point = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.userType = Convert.ToString(dataTable.Rows[0][i++]);
        }
    }
    /*
    CREATE TABLE Feedback (
    FeedbackId int IDENTITY(2001, 1) PRIMARY KEY,
    Name varchar (30) NOT NULL,
    Email varchar (30) NOT NULL,
    Message text NOT NULL
    );
    */

    public class Feedback
    {
        int feedbackId;
        string name;
        string email;
        string message;

        public Feedback(int feedbackId)
        {
            this.feedbackId = feedbackId;
            string query = "select * from feedback where feedbackid = " + feedbackId;
            DataTable dataTable = new DBHelper().getTable(query);
            int i = 1;
            this.name = Convert.ToString(dataTable.Rows[0][i++]);
            this.email = Convert.ToString(dataTable.Rows[0][i++]);
            this.message = Convert.ToString(dataTable.Rows[0][i++]);
        }
    }
    /*
    CREATE TABLE OnlineJudge (
    OJID int IDENTITY(3001, 1) PRIMARY KEY,
    OJName varchar (15) NOT NULL);
    */
    public class OnlineJudge
    {
        int ojId;
        string ojName;
        public OnlineJudge(int ojId)
        {
            this.ojId = ojId;
            string query = "select * from onlinejudge where ojid = " + ojId;
            DataTable dataTable = new DBHelper().getTable(query);
            this.ojName = Convert.ToString(dataTable.Rows[0][1]);
        }
    }

    /*
    CREATE TABLE Tag (
    TagID int IDENTITY(4001, 1) PRIMARY KEY,
    Text varchar (20) NOT NULL
    );
    */
    public class Tag
    {
        int tagId;
        string text;
        public Tag(int tagId)
        {
            this.tagId = tagId;
            string query = "select * from tag where tagid = " + tagId;
            DataTable dataTable = new DBHelper().getTable(query);
            this.text = Convert.ToString(dataTable.Rows[0][1]);
        }
    }

    /*
CREATE TABLE Problem (
    ProblemID int IDENTITY(5001, 1) PRIMARY KEY,
    OJID int FOREIGN KEY REFERENCES OnlineJudge(OJId),
    Title varchar (30) NOT NULL,
    ProblemCode varchar (20) NULL
);
*/

    public class Problem
    {
        int problemId;
        int ojId;
        string title;
        string problemCode;
        public Problem(int problemId)
        {
            this.problemId = problemId;
            string query = "select * from problem where problemid = " + problemId;
            DataTable dataTable = new DBHelper().getTable(query);
            int i = 1;
            this.ojId = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.title = Convert.ToString(dataTable.Rows[0][i++]);
            this.problemCode = Convert.ToString(dataTable.Rows[0][i++]);
        }
    }

    /*
    CREATE TABLE Editorial(
    EditorialID int IDENTITY(6001, 1) PRIMARY KEY,
    UserID int FOREIGN KEY REFERENCES [User](UserID),
    ProblemID int FOREIGN KEY REFERENCES Problem(ProblemID),
    TagID int FOREIGN KEY REFERENCES Tag(TagID),
    Description text NOT NULL,
    UpVote int DEFAULT 0,
    DownVote int DEFAULT 0,
    DateOfPublishing datetime DEFAULT GETDATE()
    );
    */
    public class Editorial
    {
        int editorialId;
        int userId;
        int problemId;
        int tagId;
        string description;
        int upvote;
        int downvote;
        string dateOfPublishing;

        public Editorial (int editorialId)
        {
            this.editorialId = editorialId;
            string query = "select * from editorial where editorialid = " + editorialId;
            DataTable dataTable = new DBHelper().getTable(query);
            int i = 1;
            this.userId = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.problemId = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.tagId = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.description = Convert.ToString(dataTable.Rows[0][i++]);
            this.upvote = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.downvote = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.dateOfPublishing = Convert.ToString(dataTable.Rows[0][i++]);
        }
    }

}
