using System;
using System.Collections;
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
        public EditorialTags GetEditorialTags(int editorialId)
        {
            return new EditorialTags(editorialId);
        }
        public Comment GetComment(int commentId)
        {
            return new Comment(commentId);
        }
        public List<Comment> GetCommentsOfEditorial(int editorialId)
        {
            List<Comment> commentList = new List<Comment>();
            string query = "select commentid from comment where editorialid = " + editorialId + " and parentid is null";
            DataTable dataTable = new DBHelper().getTable(query);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                commentList.Add
                (
                    GetComment
                    (
                        Convert.ToInt32(dataTable.Rows[i][0]) // comment id
                    )
                );
            }
            return commentList;
        }
        public List<Comment> GetRepliesOfComment(int parentCommentId)
        {
            List<Comment> commentList = new List<Comment>();
            string query = "select commentid from comment where parentId = " + parentCommentId;
            DataTable dataTable = new DBHelper().getTable(query);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                commentList.Add
                (
                    GetComment
                    (
                        Convert.ToInt32(dataTable.Rows[i][0])
                    )
                );
            }
            return commentList;
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
        public int userId { get; set; }
        public string userName { get; set; }
        string email;
        string password;
        int point;
        string userType;

        public User() { }
        public User(int userId)
        {
            this.userId = userId;
            string query = "select * from [user] where userid = " + userId;
            DataTable dataTable = new DBHelper().getTable(query);
            int i = 1;
            this.userName = Convert.ToString(dataTable.Rows[0][i++]);
            this.email = Convert.ToString(dataTable.Rows[0][i++]);
            this.password = Convert.ToString(dataTable.Rows[0][i++]);
            this.point = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.userType = Convert.ToString(dataTable.Rows[0][i++]);
        }
        public string getUserNameOnly(int userId)
        {
            string query = "select username from [user] where userid = " + userId;
            DataTable dataTable = new DBHelper().getTable(query);
            return Convert.ToString(dataTable.Rows[0][0]);
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

        public Feedback() { }
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
        public OnlineJudge() { }
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
        public string text { get; set; }
        public Tag() { }
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
        public int problemId { get; set; }
        public int ojId { get; set; }

        public string title { get; set; }
        string problemCode;
        public Problem() { }
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
    Rephrase text NULL,
    Solution text NULL,
    Details text NULL,
    UpVote int DEFAULT 0,
    DownVote int DEFAULT 0,
    DateOfPublishing datetime DEFAULT GETDATE()
    );
    */
    public class Editorial
    {
        public int editorialId { get; set; }
        public int userId { get; set; }
        public int problemId { get; set; }
        public int tagId { get; set; }
        public string rephrase { get; set; }
        public string solution { get; set; }
        public string details { get; set; }
        public int upvote { get; set; }
        public int downvote { get; set; }
        public string dateOfPublishing { get; set; }
        public Editorial() {}

        public Editorial(int editorialId)
        {
            this.editorialId = editorialId;
            string query = "select * from editorial where editorialid = " + editorialId;
            DataTable dataTable = new DBHelper().getTable(query);
            int i = 1;
            this.userId = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.problemId = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.tagId = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.rephrase = Convert.ToString(dataTable.Rows[0][i++]);
            this.solution = Convert.ToString(dataTable.Rows[0][i++]);
            this.details = Convert.ToString(dataTable.Rows[0][i++]);
            this.upvote = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.downvote = Convert.ToInt32(dataTable.Rows[0][i++]);
            this.dateOfPublishing = Convert.ToString(dataTable.Rows[0][i++]);
        }
    }
    /*
        CREATE TABLE EditorialTags (
        EditorialId int FOREIGN KEY REFERENCES Editorial(EditorialId),
        TagId int FOREIGN KEY REFERENCES Tag(TagId)
        );
    */
    public class EditorialTags
    {
        public List<Tag> tagList { get; }
        public int editorialId { get; set; }

        public EditorialTags() { }
        public EditorialTags(int editorialId)
        {
            this.tagList = new List<Tag>();
            this.editorialId = editorialId;
            string query = "select tagid from editorialtags where editorialid = " + editorialId;
            DataTable dataTable = new DBHelper().getTable(query);
            for (int i = 0; i < dataTable.Rows.Count; i++)
            {
                this.tagList.Add
                    (
                        new Tag
                        (
                            Convert.ToInt32(dataTable.Rows[i][0])
                        )
                    );
            }
        }
    }
    /*
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
    */

    public class Comment
    {
        public int commentId { get; set; }
        public int userId { get; set; }
        public int editorialId { get; set; }
        public int parentId { get; set; }
        public string text { get; set; }
        public int upvote { get; set; }
        public int downvote { get; set; }
        public string dateOfPublishing { get; set; }

        public Comment()
        {

        }
        public Comment(int commentId)
        {
            this.commentId = commentId;
            string query = "select * from comment where commentid = " + commentId;
            DataTable dtable = new DBHelper().getTable(query);
            int i = 1;
            this.userId = Convert.ToInt32(dtable.Rows[0][i++]);
            try
            {
                this.editorialId = Convert.ToInt32(dtable.Rows[0][i++]);
            }
            catch (System.FormatException)
            {
                this.editorialId = 0;
            }
            catch (System.InvalidCastException)
            {
                this.editorialId = 0;
            }
            try
            {
                this.parentId = Convert.ToInt32(dtable.Rows[0][i++]);
            }
            catch (System.FormatException)
            {
                this.parentId = 0;
            }
            catch (System.InvalidCastException)
            {
                this.parentId = 0;
            }
            this.text = Convert.ToString(dtable.Rows[0][i++]);
            this.upvote = Convert.ToInt32(dtable.Rows[0][i++]);
            this.downvote = Convert.ToInt32(dtable.Rows[0][i++]);
            this.dateOfPublishing = Convert.ToString(dtable.Rows[0][i++]);
        }


    }

    //    CREATE TABLE Report(
    //        ReportID int IDENTITY(9001, 1) PRIMARY KEY,
    //        UserID int FOREIGN KEY REFERENCES[User](UserID),
    //	PostID int NOT NULL,
    //	PostType varchar(10) NOT NULL,
    //   Text text NOT NULL
    //);

    public class Report
    {
        public int reportId { get; set; }
        public int userId { get; set; }
        public int postId { get; set; }
        public string postType { get; set; }
        public string text { get; set; }

        public Report(int reportId)
        {
            this.reportId = reportId;
            string query = "Select * from report where reportId = " + reportId;

            DataTable dtable = new DBHelper().getTable(query);

            int i = 1;

            userId = Convert.ToInt32(dtable.Rows[0][i++]);
            postId = Convert.ToInt32(dtable.Rows[0][i++]);
            postType = Convert.ToString(dtable.Rows[0][i++]);
            text = Convert.ToString(dtable.Rows[0][i++]);

        }

    }
    //    CREATE TABLE Bookmark(
    //    BookmarkID int IDENTITY(10001, 1) PRIMARY KEY,
    //    UserID int FOREIGN KEY REFERENCES[User](UserID),
    //	EditorialID int FOREIGN KEY REFERENCES Editorial(EditorialID),  
    //);

    public class Bookmark
    {
        public int bookmarkId { get; set; }
        public int userId { get; set; }
        public int editorialId { get; set; }

        public Bookmark(int bookmarkId)
        {
            this.bookmarkId = bookmarkId;
            string query = "Select * from Bookmark where bookmarkId = " + bookmarkId;

            DataTable dtable = new DBHelper().getTable(query);

            int i = 1;

            userId = Convert.ToInt32(dtable.Rows[0][i++]);
            editorialId = Convert.ToInt32(dtable.Rows[0][i++]);
        }
    }
}
