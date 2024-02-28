using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes;

public class PatioTeste : IDisposable
{

    private Patio _estacionamento;
    private Veiculo _veiculo;
    private ITestOutputHelper _testOutputHelper;
    private Operador _operador;

    public PatioTeste(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testOutputHelper.WriteLine("Construtor invocado.");
        _estacionamento = new Patio();
        _veiculo = new Veiculo();
        _operador = new Operador();
        _operador.Nome = "Pedro Fagundes";
    }
    
    [Fact(DisplayName = "Validando o faturamento do estacionamento")]
    public void ValidaFaturamentoDoEstacionamentoComUmVeiculo()
    {
        _veiculo = new Veiculo()
        {
            Proprietario = "Bruno Vinícius",
            Tipo = TipoVeiculo.Automovel,
            Cor = "Prata",
            Modelo = "Corolla",
            Placa = "ads-9999"
        };

        _estacionamento.OperadorPatio = _operador;
        _estacionamento.RegistrarEntradaVeiculo(_veiculo);
        _estacionamento.RegistrarSaidaVeiculo(_veiculo.Placa);
        
        double faturamento = _estacionamento.TotalFaturado();
        
        Assert.Equal(2, faturamento);
    }

    [Theory]
    [InlineData("Ana Julia", "ASW-1293", "prata", "Corolla")]
    [InlineData("Silvio", "ASD-1233", "azul", "BMW")]
    [InlineData("Carina", "FDS-1235", "branco", "Volvo V60")]
    public void ValidaFaturamentoDoEstacionamentoComVariosVeiculos(string propietario, string placa, string cor, string modelo)
    {
        _veiculo = new Veiculo()
        {
            Proprietario = propietario,
            Placa = placa,
            Cor = cor,
            Modelo = modelo
        };

        _estacionamento.OperadorPatio = _operador;
        _estacionamento.RegistrarEntradaVeiculo(_veiculo);
        _estacionamento.RegistrarSaidaVeiculo(_veiculo.Placa);
        
        double faturamento = _estacionamento.TotalFaturado();
        
        
        Assert.Equal(2, faturamento);
    }

    [Theory]
    [InlineData("Bruno", "ASI-1298", "branco", "Corolla")]
    public void LocalizaVeiculoNoPatioComBaseNoIdDoTicket(string proprietario, string placa, string cor, string modelo)
    {
        _veiculo = new Veiculo()
        {
            Proprietario = proprietario,
            Placa = placa,
            Modelo = modelo,
            Cor = cor
        };

        _estacionamento.OperadorPatio = _operador;
        _estacionamento.RegistrarEntradaVeiculo(_veiculo);
        
        var consultado = _estacionamento.PesquisaVeiculo(_veiculo.IdTicket);

        Assert.Contains("### Ticket Estacionamento Alura ###", consultado.Ticket);
    }

    [Fact]
    public void AlterarDadosVeiculoDoProprioVeiculo()
    {
        _veiculo = new Veiculo()
        {
            Proprietario = "José Silva",
            Placa = "SDC-3218",
            Cor = "Verde",
            Modelo = "Opala"
        };
        Veiculo veiculoAlterado = new Veiculo()
        {
            Proprietario = "José Silva",
            Placa = "SDC-3218",
            Cor = "Preto",
            Modelo = "Opala"
        };

        _estacionamento.OperadorPatio = _operador;
        _estacionamento.RegistrarEntradaVeiculo(_veiculo);
        
        Veiculo alterado = _estacionamento.VeiculoAlterado(veiculoAlterado);

        Assert.Equal(alterado.Cor, veiculoAlterado.Cor);

    }

    public void Dispose()
    {
        _testOutputHelper.WriteLine("Dispose invocado");
    }
}