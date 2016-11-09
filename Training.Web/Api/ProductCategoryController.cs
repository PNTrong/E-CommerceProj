using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Script.Serialization;
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
        [AllowAnonymous]
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

        [Route("getbyid/{id:int}")]
        [HttpGet]
        public HttpResponseMessage GetById(HttpRequestMessage req, int id)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                var model = _productCategoryService.GetById(id);
                var resData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(model);
                res = req.CreateResponse(HttpStatusCode.OK, resData);
                return res;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage req, ProductCategoryViewModel productCategoryVm)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                if (ModelState.IsValid)
                {
                    var dbProductCategory = _productCategoryService.GetById(productCategoryVm.ID);
                    dbProductCategory.UpdateProductCategory(productCategoryVm);
                    dbProductCategory.UpdatedDate = DateTime.Now;

                    _productCategoryService.Update(dbProductCategory);
                    _productCategoryService.SaveChanges();

                    var resData = Mapper.Map<ProductCategory, ProductCategoryViewModel>(dbProductCategory);
                    res = req.CreateResponse(HttpStatusCode.OK, resData);
                }
                else
                {
                    req.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return res;
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
                    productCategory.CreatedDate = DateTime.Now;

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

        [Route("delete")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage Delete(HttpRequestMessage req, int id)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                var productCategories = _productCategoryService.Delete(id);
                _productCategoryService.SaveChanges();
                res = req.CreateResponse(HttpStatusCode.OK, productCategories);
                return res;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage req, string checkedProductCategories)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage response = null;
                var productCategories = new JavaScriptSerializer().Deserialize<List<int>>(checkedProductCategories);
                foreach (var item in productCategories)
                {
                    _productCategoryService.Delete(item);
                }

                _productCategoryService.SaveChanges();
                response = req.CreateResponse(HttpStatusCode.OK, productCategories.Count);
                return response;
            });
        }
    }
}