using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Training.Model.Models;
using Training.Service;
using Training.Web.Infrastructure.Core;
using Training.Web.Infrastructure.Extensions;
using Training.Web.Models;

namespace Training.Web.Api
{
    [RoutePrefix("api/productcategory")]
    public class ProductCategoryController : ApiControllerBase
    {
        private IProductCategoryService _productCategoryService;

        public ProductCategoryController(IErrorService errorService, IProductCategoryService productCategoryService)
            : base(errorService)
        {
            this._productCategoryService = productCategoryService;
        }

        [Route("getall")]
        [HttpGet]
        public HttpResponseMessage Get(HttpRequestMessage req, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(req, () =>
             {
                 int totalRow = 0;

                 var model = _productCategoryService.GetAll(keyword);

                 totalRow = model.Count();

                 //thuat toan phan trang
                 var query = model.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                 var listProductCategory = Mapper.Map<List<ProductCategoryViewModel>>(query);

                 var paginationSet = new PaginationSet<ProductCategoryViewModel>()
                 {
                     Items = listProductCategory,
                     Page = page,
                     TotalRow = totalRow,
                     TotalPage = (int)Math.Ceiling((decimal)totalRow / pageSize)
                 };

                 var respone = req.CreateResponse(HttpStatusCode.OK, paginationSet);

                 return respone;
             });
        }

        [Route("getallparents")]
        [HttpGet]
        public HttpResponseMessage GetAll(HttpRequestMessage req)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                var model = _productCategoryService.GetAll();
                var resData = Mapper.Map<IEnumerable<ProductCategory>, IEnumerable<ProductCategoryViewModel>>(model);
                res = req.CreateResponse(HttpStatusCode.OK, resData);
                return res;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage req, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage respone = null;
                if (ModelState.IsValid)
                {
                    var productCategory = new ProductCategory();
                    productCategory.UpdateProductCategory(productCategoryVm);

                    _productCategoryService.Add(productCategory);
                    _productCategoryService.SaveChanges();

                    var responeData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(productCategory);
                    respone = req.CreateResponse(HttpStatusCode.OK, responeData);
                }
                else
                {
                    req.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return respone;
            });
        }
    }
}