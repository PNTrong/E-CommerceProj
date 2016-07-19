using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;
using Training.Data.Infrastructure;
using Training.Data.Repositories;
using Training.Model.Models;

namespace Training.UnitTest.RepositoryTest
{
    [TestClass]
    public class PostCategoryRepositoryTest
    {
        private IDbFactory dbFactory;
        private IPostCategoryRepository objRepository;
        private IUnitOfWork unitOfWork;

        //First method to inital object
        [TestInitialize]
        public void Initialize()
        {
            dbFactory = new DbFactory();
            objRepository = new PostCategoryRepository(dbFactory);
            unitOfWork = new UnitOfWork(dbFactory);
        }

        [TestMethod]
        public void PostCategory_Repository_GetAll()
        {
            var listObject = objRepository.GetAll().ToList();

            Assert.AreEqual(9, listObject.Count());
        }

        [TestMethod]
        public void PostCategory_Repository_Create()
        {
            var postCategory = new PostCategory();
            postCategory.Name = "Test Post Category";
            postCategory.Alias = "Test-PostCategory";
            postCategory.Status = true;

            var result = objRepository.Add(postCategory);
            unitOfWork.Commit();

            Assert.IsNotNull(result);
            Assert.AreEqual(9, result.ID);
        }
    }
}