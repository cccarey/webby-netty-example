using System;
using System.Collections.Generic;
using System.Text;

using LitJson;

namespace Model
{
    public class Post
    {
        public string _id = null;
        public string _rev = null;
        public string Key;
        public string Title;
        public string Content;
        public DateTime Created = DateTime.Now;

        public List<Comment> Comments
        {
            get
            {
                if (_comments == null) _comments = Comment.GetAll(this.Key);
                return _comments;
            }
            set { _comments = value; }
        }
        private List<Comment> _comments = null;

        public void Save()
        {
            BlogDB.CreateDocument(this.Key, this.ToJson());

            Post readPost = Post.Get(this.Key);
            this._id = readPost._id;
            this._rev = readPost._rev;
        }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"_id\":\"{0}\",", (this._id == null) ? this.Key : this._id);
            if (this._rev != null) sb.AppendFormat("\"_rev\":\"{0}\",", this._rev);
            sb.AppendFormat("\"Key\":\"{0}\",", this.Key);
            sb.AppendFormat("\"Title\":\"{0}\",", this.Title);
            sb.AppendFormat("\"Content\":\"{0}\",", this.Content);
            sb.AppendFormat("\"Created\":\"{0}\"", this.Created.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            sb.Append("}");
            return sb.ToString();
        }

        public static Post Get(string id)
        {
            return FromJson(BlogDB.GetDocument(id));
        }

        private static Post FromJson(string json)
        {
            Post post = JsonMapper.ToObject<Post>(json);
            return post;
        }
    }
}
