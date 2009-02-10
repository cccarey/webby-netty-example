using System;
using System.Collections.Generic;
using System.Text;
using NUnit.Framework;

using m = Model;
using dm = MySqlDataManager.DataManager;

namespace ModelTests
{
    [TestFixture]
    public class Post
    {
        [TestFixtureSetUp]
        public void Setup()
        {
        	dm.ExecuteNonQuery("truncate table posts");
        	dm.ExecuteNonQuery("truncate table comments");
        }

//        [TestFixtureTearDown]
//        public void TearDown()
//        {
//            m.BlogDB.DeleteDatabase();
//        }

        [Test]
        public void CreateAndGetTest()
        {
            m.Post post = new m.Post();
            post.Title = "CreateAndGetTest Document";
            post.Key = "CreateAndGetTest";
            post.Content = "This is a test document.";
            post.Save();

            AssertGoodRead(post, "CreateAndGetTest");
        }

        [Test]
        public void UpdateTest()
        {
            m.Post post = new m.Post();
            post.Title = "UpdateTest Document";
            post.Key = "UpdateTest";
            post.Content = "This is a test document for update.";
            post.Save();

            AssertGoodRead(post, "UpdateTest");

            post.Content = "Change the content for the update test document";
            post.Save();

            AssertGoodRead(post, "UpdateTest");
        }

        private void AssertGoodRead(m.Post post, string docid)
        {
            m.Post readPost = m.Post.Get(docid);
            Assert.AreEqual(post.ID, readPost.ID);
            Assert.AreEqual(post.Title, readPost.Title);
            Assert.AreEqual(post.Key, readPost.Key);
            Assert.AreEqual(post.Content, readPost.Content);
            Assert.AreEqual(post.Created.ToString("yyyy-MM-dd HH:mm:ss"), readPost.Created.ToString("yyyy-MM-dd HH:mm:ss"));
        }
    }
}
