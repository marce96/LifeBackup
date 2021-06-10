using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace LifeBackupIntegration.Tests.Setup
{
    [CollectionDefinition("api")]
    public class CollectionFixture : ICollectionFixture<TestContext>
    {
        //this class will run before any tests, as long as it contains the attribute api.
        //LocalStack is used to run an in-memory instance of Amazon S3.


    }
}
