using NerdStore.WebApp.MVC;
using NerdStore.WebApp.Tests.Config;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.WebApp.Tests
{
    public class UsuarioTests
    {
        private readonly IntegrationTestsFixtures<StartupWebTests> _testsFixture;

        public UsuarioTests(IntegrationTestsFixtures<StartupWebTests> testsFixture)
        {
            _testsFixture = testsFixture;
        }
    }
}
