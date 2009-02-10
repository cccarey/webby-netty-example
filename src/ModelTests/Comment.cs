using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using m = Model;
using dm = MySqlDataManager.DataManager;

namespace ModelTests
{
    [TestFixture]
    public class Comment
    {
        [TestFixtureSetUp]
        public void Setup()
        {
        	dm.ExecuteNonQuery("truncate table posts");
        	dm.ExecuteNonQuery("truncate table comments");
        	
        	m.Post post = new m.Post();
        	post.Key = "CorePostForCommentTest";
        	post.Title = "Core Post For Comment Test";
        	post.Content = "This is some content for the core post.";
        	post.Save(); // this post has id 1
        	Assert.IsTrue(post.ID == 1, "Cannot continue with tests.  Something wrong with core post.");
        }

//        [TestFixtureTearDown]
//        public void TearDown()
//        {
//            m.BlogDB.DeleteDatabase();
//        }

        [Test]
        public void CreateAndGetTest()
        {
            m.Comment comment = new m.Comment();
            comment.PostID = 1;
            comment.Number = 1;
            comment.Content = "this is a comment for the CreateAndGetTest.";
            comment.Save();
            
            Assert.IsFalse(comment.ID == -1, "Comment ID should not be -1 after insert.");
            
            AssertGoodRead(comment, comment.ID);
        }

        [Test]
        public void UpdateTest()
        {
            m.Comment comment = new m.Comment();
            comment.PostID = 1;
            comment.Number = 2;
            comment.Content = "this is a comment for the UpdateTest.";
            comment.Save();

            Assert.IsFalse(comment.ID == -1, "Comment ID should not be -1 after insert.");

            AssertGoodRead(comment, comment.ID);

            comment.Content = "Change the content for the update test document";
            comment.Save();

            AssertGoodRead(comment, comment.ID);
        }

        [Test]
        public void PostWithCommentTest()
        {
            m.Post post = new m.Post();
            post.Key = "PostWithCommentTest";
            post.Title = "Post With Comment Test";
            post.Content = "This is a post that will have comments.";
            post.Save();

            post.Comments = new List<Model.Comment>();

            m.Comment comment = null;

            for (int pass = 1; pass < 4; pass++)
            {
                comment = new m.Comment();
                comment.PostID = post.ID;
                comment.Number = pass;
                comment.Content = string.Format("this is comment number {0}.", pass);
                comment.Save();

                post.Comments.Add(comment);

                AssertGoodRead(comment, comment.ID);
            }
            Assert.IsTrue(post.Comments.Count == 3, "Wrong number of comments in post");

            m.Post readPost = m.Post.Get(post.Key);
            Assert.IsTrue(readPost.Comments.Count == 3, "Wrong number of comments in readPost");
            for (int pass = 1; pass < 4; pass++)
            {
                AssertAreEqual(post.Comments[pass - 1], readPost.Comments[pass - 1]);
            }
        }

        private void AssertGoodRead(m.Comment comment, int id)
        {
            m.Comment readComment = m.Comment.Get(id);
            AssertAreEqual(comment, readComment);
        }

        private void AssertAreEqual(m.Comment comment, m.Comment readComment)
        {
            Assert.AreEqual(comment.ID, readComment.ID);
            Assert.AreEqual(comment.Number, readComment.Number);
            Assert.AreEqual(comment.PostID, readComment.PostID);
            Assert.AreEqual(comment.Content, readComment.Content);
            Assert.AreEqual(comment.Created.ToString("yyyy-MM-dd HH:mm:ss"), readComment.Created.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
