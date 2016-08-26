using Training.Model.Models;
using Training.Web.Models;

namespace Training.Web.Infrastructure.Extensions
{
    public static class EntityExtensions
    {
        public static void UpdatePostCategory(this PostCategory postCategory, PostCategoryViewModel postCategoryVm)
        {
            postCategory.ID = postCategoryVm.ID;
            postCategory.Name = postCategoryVm.Name;
            postCategory.ParentID = postCategoryVm.ParentID;
            postCategory.Description = postCategoryVm.Description;
            postCategory.Alias = postCategoryVm.Alias;
            postCategory.DisplayOrder = postCategoryVm.DisplayOrder;
            postCategory.Image = postCategoryVm.Image;
            postCategory.HomeFlag = postCategoryVm.HomeFlag;

            postCategory.CreatedBy = postCategoryVm.CreatedBy;
            postCategory.CreatedDate = postCategoryVm.CreatedDate;
            postCategory.UpdatedBy = postCategoryVm.UpdatedBy;
            postCategory.UpdatedDate = postCategoryVm.UpdatedDate;
            postCategory.MetaDescription = postCategoryVm.MetaDescription;
            postCategory.MetaKeyword = postCategoryVm.MetaKeyword;
            postCategory.Status = postCategoryVm.Status;
        }

        public static void UpdatePost(this Post post, PostViewModel postVm)
        {
            post.ID = postVm.ID;
            post.Name = postVm.Name;
            post.Alias = postVm.Alias;
            post.Description = postVm.Description;
            post.CategoryID = postVm.CategoryID;
            post.Content = postVm.Content;
            post.Image = postVm.Image;
            post.ViewCount = postVm.ViewCount;
            post.HotFlag = postVm.HotFlag;
            post.HomeFlag = postVm.HomeFlag;

            post.CreatedBy = postVm.CreatedBy;
            post.CreatedDate = postVm.CreatedDate;
            post.UpdatedBy = postVm.UpdatedBy;
            post.UpdatedDate = postVm.UpdatedDate;
            post.MetaDescription = postVm.MetaDescription;
            post.MetaKeyword = postVm.MetaKeyword;
            post.Status = postVm.Status;
        }

        public static void UpdateProductCategory(this ProductCategory productCategory, ProductCategoryViewModel productCategoryVm)
        {
            productCategory.ID = productCategoryVm.ID;
            productCategory.Name = productCategoryVm.Name;
            productCategory.Alias = productCategoryVm.Alias;
            productCategory.Description = productCategoryVm.Description;
            productCategory.DisplayOrder = productCategoryVm.DisplayOrder;
            productCategory.Image = productCategoryVm.Image;
            productCategory.ParentID = productCategoryVm.ParentID;
            productCategory.HomeFlag = productCategoryVm.HomeFlag;
            
            productCategory.CreatedBy = productCategoryVm.CreatedBy;
            productCategory.CreatedDate = productCategoryVm.CreatedDate;
            productCategory.UpdatedBy = productCategoryVm.UpdatedBy;
            productCategory.UpdatedDate = productCategoryVm.UpdatedDate;
            productCategory.MetaDescription = productCategoryVm.MetaDescription;
            productCategory.MetaKeyword = productCategoryVm.MetaKeyword;
            productCategory.Status = productCategoryVm.Status;
        }
    }
}