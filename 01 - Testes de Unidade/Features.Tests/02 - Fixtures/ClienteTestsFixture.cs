using Features.Clientes;

namespace Features.Tests
{
    [CollectionDefinition(nameof(ClienteCollection))]
    public class ClienteCollection : ICollectionFixture<ClienteTestsFixture>
    {

    }

    public class ClienteTestsFixture : IDisposable
    {
        public Cliente GerarClienteValido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "Bryan",
                "Lima",
                DateTime.Now.AddYears(-25),
                "bryan@lima.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public Cliente GerarClienteInvalido()
        {
            var cliente = new Cliente(
                Guid.NewGuid(),
                "",
                "",
                DateTime.Now,
                "bryan2lima.com",
                true,
                DateTime.Now);

            return cliente;
        }

        public void Dispose()
        {
            
        }
    }
}
