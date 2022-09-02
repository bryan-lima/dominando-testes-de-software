using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NerdStore.BDD.Tests.Config
{
    [CollectionDefinition(nameof(AutomacaoWebFixtureCollection))]
    public class AutomacaoWebFixtureCollection : ICollectionFixture<AutomacaoWebTestsFixture>
    {

    }

    public class AutomacaoWebTestsFixture
    {
        public SeleniumHelper BrowserHelper;
        public readonly ConfigurationHelper Configuration;

        public AutomacaoWebTestsFixture()
        {
            Configuration = new ConfigurationHelper();
            BrowserHelper = new SeleniumHelper(Browser.Chrome, Configuration, false);
        }
    }
}
