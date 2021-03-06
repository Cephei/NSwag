﻿using System.Linq;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace NSwag.SwaggerGeneration.WebApi.Tests.Attributes
{
    [TestClass]
    public class ApiExplorerSettingsAttributeTests
    {
        public class TestController : ApiController
        {
            [ApiExplorerSettings(IgnoreApi = false)]
            public object Foo()
            {
                return null; 
            }

            [ApiExplorerSettings(IgnoreApi = true)]
            public object Bar()
            {
                return null; 
            }
        }

        [TestMethod]
        public async Task When_action_has_ApiExplorerSettingsAttribute_with_IgnoreApi_then_it_is_ignored()
        {
            //// Arrange
            var generator = new WebApiToSwaggerGenerator(new WebApiAssemblyToSwaggerGeneratorSettings { IsAspNetCore = true });

            //// Act
            var document = await generator.GenerateForControllerAsync<TestController>();
            var json = document.ToJson();

            //// Assert
            Assert.AreEqual(1, document.Operations.Count());
            Assert.AreEqual("Test_Foo", document.Operations.Single().Operation.OperationId);
        }
    }
}