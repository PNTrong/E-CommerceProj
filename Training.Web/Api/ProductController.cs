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
    [RoutePrefix("api/product")]
    public class ProductController : ApiControllerBase
    {
        private IProductService _productService;

        public ProductController(IErrorService errorService, IProductService productService) : base(errorService)
        {
            this._productService = productService;
        }

        [Route("getall")]
        public HttpResponseMessage GetAll(HttpRequestMessage req, string keyword, int page, int pageSize = 20)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                int totalRow = 0;
                var products = _productService.GetAll(keyword);
                totalRow = products.Count();

                var query = products.OrderByDescending(x => x.CreatedDate).Skip(page * pageSize).Take(pageSize);

                var resData = Mapper.Map<IEnumerable<Product>, IEnumerable<ProductViewModel>>(query);

                var paginationSet = new PaginationSet<ProductViewModel>()
                {
                    Items = resData,
                    Page = page,
                    TotalRow = totalRow,
                    TotalPage = (int)Math.Ceiling((decimal)totalRow / pageSize)
                };
                res = req.CreateResponse(HttpStatusCode.OK, paginationSet);
                return res;
            });
        }

        [Route("create")]
        [HttpPost]
        [AllowAnonymous]
        public HttpResponseMessage Create(HttpRequestMessage req, ProductViewModel productVm)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                if (ModelState.IsValid)
                {
                    var product = new Product();
                    product.UpdateProduct(productVm);
                    product.CreatedDate = DateTime.Now;

                    _productService.Add(product);
                    _productService.SaveChanges();

                    var resData = Mapper.Map<Product, ProductViewModel>(product);
                    res = req.CreateResponse(HttpStatusCode.OK, resData);
                }
                else
                {
                    req.CreateErrorResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return res;
            });
        }

        [Route("update")]
        [HttpPut]
        [AllowAnonymous]
        public HttpResponseMessage Update(HttpRequestMessage req, ProductViewModel productVm)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage res = null;
                if (ModelState.IsValid)
                {
                    var dbProduct = _productService.GetById(productVm.ID);
                    dbProduct.UpdateProduct(productVm);
                    dbProduct.UpdatedDate = DateTime.Now;

                    _productService.Update(dbProduct);
                    _productService.SaveChanges();

                    var resData = Mapper.Map<Product, ProductViewModel>(dbProduct);
                    res = req.CreateResponse(HttpStatusCode.OK, resData);
                }
                else
                {
                    res = req.CreateResponse(HttpStatusCode.BadRequest, ModelState);
                }
                return res;
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
                var product = _productService.Delete(id);
                _productService.SaveChanges();
                res = req.CreateResponse(HttpStatusCode.OK, product);
                return res;
            });
        }

        [Route("deletemulti")]
        [HttpDelete]
        [AllowAnonymous]
        public HttpResponseMessage DeleteMulti(HttpRequestMessage req, string checkedProduct)
        {
            return CreateHttpResponse(req, () =>
            {
                HttpResponseMessage response = null;
                var products = new JavaScriptSerializer().Deserialize<List<int>>(checkedProduct);
                foreach (var item in products)
                {
                    _productService.Delete(item);
                }

                _productService.SaveChanges();
                response = req.CreateResponse(HttpStatusCode.OK, products.Count);
                return response;
            });
        }
    }
}