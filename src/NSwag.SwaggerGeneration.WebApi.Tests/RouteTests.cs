﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using NSwag.Annotations;

namespace NSwag.SwaggerGeneration.WebApi.Tests
{
    [TestClass]
    public class RouteTests
    {
        // From https://github.com/NSwag/NSwag/issues/664

        public class ProductPagedResult
        {
            public string Foo { get; set; }
        }

        public class Product
        {
            public string Bar { get; set; }
        }

        public class AddProductPayload
        {
            public string Baz { get; set; }
        }

        public class ProductsController : ApiController
        {
            [SwaggerOperation("GetProducts")]
            [HttpGet, Route("products")]
            public async Task<IHttpActionResult> GetProducts([FromUri] ProductPagedResult payload)
            {
                return null;
            }

            [SwaggerOperation("GetProductByUserDefinedId")]
            [HttpGet, Route("products/{userDefinedId}")]
            public IHttpActionResult GetProduct(string userDefinedId)
            {
                return null;
            }

            [SwaggerOperation("GetProductByUniqueId")]
            [HttpGet, Route("products/{id:guid}")]
            public IHttpActionResult GetProductByUniqueId(Guid id)
            {
                return null;
            }

            [SwaggerOperation("DeleteProductByUserDefinedId")]
            [HttpDelete, Route("products/{userDefinedId}")]
            public async Task<IHttpActionResult> Delete(string userDefinedId)
            {
                return null;
            }

            [SwaggerOperation("DeleteProductByUniqueId")]
            [HttpDelete, Route("products/{id:guid}")]
            public async Task<IHttpActionResult> DeleteByUniqueId()
            {
                return null;
            }

            [SwaggerOperation("PutProductByUserDefinedId")]
            [HttpPut, Route("products/{userDefinedId}")]
            public async Task<IHttpActionResult> Put(string userDefinedId, [FromBody] Product data)
            {
                return null;
            }

            [SwaggerOperation("PutProductByUniqueId")]
            [HttpPut, Route("products/{id:guid}")]
            public async Task<IHttpActionResult> Put(Guid id, [FromBody] Product data)
            {
                return null;
            }

            [SwaggerOperation("PostProducts")]
            [HttpPost, Route("products")]
            public async Task<IHttpActionResult> FetchAll([FromBody] AddProductPayload data)
            {
                return null;
            }
        }

        [TestMethod]
        public async Task When_swagger_spec_is_generated_then_no_route_problem_is_detected()
        {
            /// Arrange
            var settings = new WebApiToSwaggerGeneratorSettings
            {
                DefaultUrlTemplate = "{controller}/{id}",
                AddMissingPathParameters = false,
            };

            /// Act
            var generator = new WebApiToSwaggerGenerator(settings);
            var document = await generator.GenerateForControllerAsync<ProductsController>();
            var swaggerSpecification = document.ToJson();
            
            /// Assert
            Assert.IsNotNull(swaggerSpecification);
        }
    }
}
