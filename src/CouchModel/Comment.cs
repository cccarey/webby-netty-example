using System;
using System.Collections.Generic;
using System.Text;

using LitJson;

namespace Model
{
    public class Comment
    {
        public string _id = null;
        public string _rev = null;
        
        public int Number
        {
            get { return _number; }
            set
            {
                _number = value;
                SetId();
            }
        }
        private int _number;
        
        public string PostKey
        {
            get { return _postKey; }
            set
            {
                _postKey = value;
                SetId();
            }
        }
        private string _postKey;

        public string Content;
        public DateTime Created = DateTime.Now;

        public void Save()
        {
            BlogDB.CreateDocument(this._id, this.ToJson());

            Comment comment = Comment.Get(this._id);
            this._id = comment._id;
            this._rev = comment._rev;
        }

        public string ToJson()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("{");
            sb.AppendFormat("\"_id\":\"{0}\",", this._id);
            if (this._rev != null) sb.AppendFormat("\"_rev\":\"{0}\",", this._rev);
            sb.AppendFormat("\"Number\":{0},", this.Number);
            sb.AppendFormat("\"PostKey\":\"{0}\",", this.PostKey);
            sb.AppendFormat("\"Content\":\"{0}\",", this.Content);
            sb.AppendFormat("\"Created\":\"{0}\"", this.Created.ToString("yyyy-MM-dd HH:mm:ss.fffffff"));
            sb.Append("}");
            return sb.ToString();
        }

        private void SetId()
        {
            _id = _postKey + "_" + _number.ToString("0000");
        }

        public static Comment Get(string id)
        {
            return FromJson(BlogDB.GetDocument(id));
        }

        public static List<Comment> GetAll(string postKey)
        {
            string json = BlogDB.ExecTempView(
                "function(doc) { if (doc.PostKey) { if (doc.PostKey == \"" + postKey + "\") emit(doc.Number, doc); } }",
                null,
                null,
                null);
            List<Comment> comments = JsonMapper.ToObject<List<Comment>>(json);
            return comments;
        }

        private static Comment FromJson(string json)
        {
            Comment comment = JsonMapper.ToObject<Comment>(json);
            return comment;
        }
    }
}
