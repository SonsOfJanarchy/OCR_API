using System;
using NUnit.Framework;
using ServiceStack;
using ServiceStack.Testing;
using OCR_API.ServiceModel;
using OCR_API.ServiceInterface;

namespace OCR_API.Tests
{
    [TestFixture]
    public class UnitTests
    {
        private readonly ServiceStackHost appHost;

        public UnitTests()
        {
            appHost = new BasicAppHost(typeof(OCRService).Assembly)
            {
                ConfigureContainer = container =>
                {
                    //Add your IoC dependencies here
                }
            }
            .Init();
        }

        [OneTimeTearDown]
        public void TestFixtureTearDown()
        {
            appHost.Dispose();
        }
    }
}
