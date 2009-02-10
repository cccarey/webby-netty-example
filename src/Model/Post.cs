using System;
using System.Collections.Generic;
using System.Text;
using System.Data;

using LitJson;
using dm = MySqlDataManager.DataManager;

namespace Model
{
    public class Post
    {
    	#region SQL Statements
    	private const string SQL_INSERT =
    		"insert into posts " +
    		"( `key`, title, content, created ) " +
    		"values " +
    		"( {0}, {1}, {2}, {3} );";
    	private const string SQL_UPDATE =
    		"update posts set " +
    		"`key` = {1}, " +
    		"title = {2}, " +
    		"content = {3}, " +
    		"created = {4} " +
    		"where id = {0}";
    	private const string SQL_SELECT_ALL =
    		"select " +
    		"id, " +
    		"`key`, " +
    		"title, " +
    		"content, " +
    		"created " +
    		"from posts ";
    	private const string SQL_SELECT_BY_ID = SQL_SELECT_ALL +
    		"where id = {0}";
    	private const string SQL_SELECT_BY_KEY = SQL_SELECT_ALL +
    		"where `key` = {0}";
    	#endregion
    	
    	public int ID {
    		get { return _id; }
    		set { _id = value; }
    	}
    	private int _id = -1;
    	
    	public string Key {
    		get { return _key; }
    		set { _key = value; }
    	}
    	private string _key;
    	
    	public string Title {
    		get { return _title; }
    		set { _title = value; }
    	}
    	private string _title;
    	
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

        public List<Comment> Comments
        {
            get
            {
                if (_comments == null && this.ID > 0)
                {
                    _comments = Comment.GetAll(this.ID);
                    if (_comments == null) _comments = new List<Comment>();
                }
                return _comments;
            }
            set { _comments = value; }
        }
        private List<Comment> _comments = null;

        public void Save()
        {
        	string sql = (this.ID > 0) ?
        		dm.SetSQL(SQL_UPDATE, this.ID, this.Key, this.Title, this.Content, this.Created) :
        		dm.SetSQL(SQL_INSERT, this.Key, this.Title, this.Content, this.Created);
        	
        	dm.ExecuteNonQuery(sql);

            Post readPost = Post.Get(this.Key);
            this.ID = readPost.ID;
        }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"_id\":\"{0}\",", this.ID);
            sb.AppendFormat("\"Key\":\"{0}\",", this.Key);
            sb.AppendFormat("\"Title\":\"{0}\",", this.Title);
            sb.AppendFormat("\"Content\":\"{0}\",", this.Content);
            sb.AppendFormat("\"Created\":\"{0}\"", this.Created.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            sb.Append("}");
            return sb.ToString();
        }

        public static Post Get(int id) {
        	return Read(dm.SetSQL(SQL_SELECT_BY_ID, id));
        }
        
        public static Post Get(string key) {
        	return Read(dm.SetSQL(SQL_SELECT_BY_KEY, key));
        }

        public static List<Post> GetAll()
        {
            return ReadMany(SQL_SELECT_ALL);
        }

        private static Post Read(string sql) {
        	Post post = null;
        	using (IDataReader reader = dm.ExecuteReader(sql)) {
        		if (reader.Read()) post = FromReader(reader);
        	}
        	return post;
        }

        private static List<Post> ReadMany(string sql)
        {
            List<Post> posts = null;
            using (IDataReader reader = dm.ExecuteReader(sql))
            {
                while (reader.Read())
                {
                    if (posts == null) posts = new List<Post>();
                    posts.Add(FromReader(reader));
                }
            }
            return posts;
        }

        private static Post FromReader(IDataReader reader) {
        	Post post = new Post();
        	post.ID = dm.ConvertIntColumn(reader, "id");
        	post.Key = dm.ConvertStringColumn(reader, "key");
        	post.Title = dm.ConvertStringColumn(reader, "title");
        	post.Content = dm.ConvertStringColumn(reader, "content");
        	post.Created = (DateTime)reader["created"];
        	return post;
        }
        
        private static Post FromJson(string json)
        {
            Post post = JsonMapper.ToObject<Post>(json);
            return post;
        }
    }
}
