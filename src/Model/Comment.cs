using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

using LitJson;
using dm = MySqlDataManager.DataManager;

namespace Model
{
    public class Comment
    {
    	#region SQL Statements
    	private const string SQL_INSERT = 
    		"insert into comments " +
    		"( post_id, number, content, created ) " +
    		"values " +
    		"( {0}, {1}, {2}, {3} )";
    	private const string SQL_UPDATE = 
    		"update comments set " +
    		"post_id = {1}, " +
    		"number = {2}, " +
    		"content = {3}, " +
    		"created = {4} " +
    		"where id = {0}";
    	private const string SQL_SELECT_ALL = 
    		"select " +
    		"c.id, " +
    		"c.post_id, " +
    		"c.number, " +
    		"c.content, " +
    		"c.created " +
    		"from comments c ";
    	private const string SQL_SELECT_BY_ID = SQL_SELECT_ALL +
    		"where id = {0}";
        private const string SQL_SELECT_BY_POST_ID_AND_NUMBER = SQL_SELECT_ALL +
            "where post_id = {0} and number = {1}";
        private const string SQL_SELECT_BY_POST_KEY = SQL_SELECT_ALL +
    		", posts p " +
    		"where c.post_id = p.id " +
    		"and p.`key` = {0}";
    	private const string SQL_SELECT_BY_POST_ID = SQL_SELECT_ALL +
    		"where c.post_id = {0}";
    	#endregion
    	
    	public int ID {
    		get { return _id; }
    		set { _id = value; }
    	}
    	private int _id = -1;
        
        public int Number
        {
            get { return _number; }
            set { _number = value; }
        }
        private int _number;
        
        public int PostID
        {
            get { return _postId; }
            set { _postId = value; }
        }
        private int _postId;

        public string Content {
        	get { return _content; }
        	set { _content = value; }
        }
        private string _content;
        
        public DateTime Created {
        	get { return _created; }
        	set { _created = value; }
        }
        private DateTime _created = DateTime.Now;

        public void Save()
        {
        	string sql = (this.ID > 0) ? 
        		dm.SetSQL(SQL_UPDATE, this.ID, this.PostID, this.Number, this.Content, this.Created) :
        		dm.SetSQL(SQL_INSERT, this.PostID, this.Number, this.Content, this.Created);
        	dm.ExecuteNonQuery(sql);

            Comment comment = Comment.Get(this.PostID, this.Number);
            this.ID = comment.ID;
        }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"_id\":\"{0}\",", this.ID);
            sb.AppendFormat("\"Number\":{0},", this.Number);
            sb.AppendFormat("\"PostID\":\"{0}\",", this.PostID);
            sb.AppendFormat("\"Content\":\"{0}\",", this.Content);
            sb.AppendFormat("\"Created\":\"{0}\"", this.Created.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            sb.Append("}");
            return sb.ToString();
        }

        public static Comment Get(int id)
        {
        	return Read(dm.SetSQL(SQL_SELECT_BY_ID, id));
        }

        public static Comment Get(int postId, int number)
        {
            return Read(dm.SetSQL(SQL_SELECT_BY_POST_ID_AND_NUMBER, postId, number));
        }

        public static List<Comment> GetAll(string postKey)
        {
        	return ReadMany(dm.SetSQL(SQL_SELECT_BY_POST_KEY, postKey));
        }

        public static List<Comment> GetAll(int postId)
        {
        	return ReadMany(dm.SetSQL(SQL_SELECT_BY_POST_ID, postId));
        }

        private static Comment Read(string sql) {
        	Comment comment = null;
        	using (IDataReader reader = dm.ExecuteReader(sql)) {
        		if (reader.Read()) comment = FromReader(reader);
        	}
        	return comment;
        }
        
        private static List<Comment> ReadMany(string sql) {
        	List<Comment> comments = null;
        	using (IDataReader reader = dm.ExecuteReader(sql)) {
        		while (reader.Read()) {
        			if (comments == null) comments = new List<Comment>();
        			comments.Add(FromReader(reader));
        		}
        	}
        	return comments;
        }
        
        private static Comment FromReader(IDataReader reader) {
        	Comment comment = new Comment();
        	comment.ID = dm.ConvertIntColumn(reader, "id");
        	comment.PostID = dm.ConvertIntColumn(reader, "post_id");
        	comment.Number = dm.ConvertIntColumn(reader, "number");
        	comment.Content = dm.ConvertStringColumn(reader, "content");
        	comment.Created = (DateTime)reader["created"];
        	return comment;
        }
        
        private static Comment FromJson(string json)
        {
            Comment comment = JsonMapper.ToObject<Comment>(json);
            return comment;
        }
    }
}
