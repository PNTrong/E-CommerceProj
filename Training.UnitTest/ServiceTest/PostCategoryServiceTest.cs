using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using System.Collections.Generic;
using Training.Data.Infrastructure;
using Training.Data.Repositories;
using Training.Model.Models;
using Training.Service;

namespace Training.UnitTest.ServiceTest
{
    [TestClass]
    public class PostCategoryServiceTest
    {
        private Mock<IPostCategoryRepository> _mockRepository;
        private Mock<IUnitOfWork> _mockUnitOfWork;
        private IPostCategoryService _postCategoryService;
        private List<PostCategory> listPostCategory;

        [TestInitialize]
        public void Initialize()
        {
            _mockRepository = new Mock<IPostCategoryRepository>();
            _mockUnitOfWork = new Mock<IUnitOfWork>();
            _postCategoryService = new PostCategoryService(_mockRepository.Object, _mockUnitOfWork.Object);
            listPostCategory = new List<PostCategory>()
            {
                new PostCategory() {ID = 1 , Name = "DM1" , Status = true },
                new PostCategory() {ID = 2 , Name = "DM2" , Status = true },
                new PostCategory() {ID = 3 , Name = "DM3" , Status = true }
            };
        }

        [TestMethod]
        public void PostCategory_Service_GetAll()
        {
            //setup method
            _mockRepository.Setup(m => m.GetAll(null)).Returns(listPostCategory);

            //call action
            var result = _postCategoryService.GetAll() as List<PostCategory>;

            //compare
            Assert.IsNotNull(result);
            Assert.AreEqual(3, result.Count);
        }

        [TestMethod]
        public void PostCategory_Service_Create()
        {
            var postCategory = new PostCategory();
            postCategory.Name = "Test";
            postCategory.Alias = "Test";
            postCategory.Status = true;

            _mockRepository.Setup(m => m.Add(postCategory)).Returns((PostCategory p) =>
           {
               p.ID = 1;
               return p;
           });

            var result = _postCategoryService.Add(postCategory);

            Assert.IsNotNull(result);
            Assert.AreEqual(1, result.ID);
        
        }
    }
}