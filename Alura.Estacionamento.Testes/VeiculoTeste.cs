using Alura.Estacionamento.Alura.Estacionamento.Modelos;
using Alura.Estacionamento.Modelos;
using Xunit.Abstractions;

namespace Alura.Estacionamento.Testes;

public class VeiculoTeste : IDisposable
{
    private Veiculo _veiculo;
    private ITestOutputHelper _testOutputHelper;

    public VeiculoTeste(ITestOutputHelper testOutputHelper)
    {
        _testOutputHelper = testOutputHelper;
        _testOutputHelper.WriteLine("Contrutor invocado.");
        _veiculo = new Veiculo();
    }
    
    [Fact]
    public void TestaVeiculoAcelerarComParametro10()
    {
        _veiculo.Acelerar(10);
        Assert.Equal(100, _veiculo.VelocidadeAtual);
    }

    [Fact]
    public void TestaVeiculoFreiarComParametro10()
    {
        _veiculo.Frear(10);
        Assert.Equal(-150, _veiculo.VelocidadeAtual);
    }

    [Fact(Skip = "Teste ainda não implementado, ignorar")]
    public void ValidaNomeProprietarioDoVeiculo()
    {
        
    }

    [Fact]
    public void FichaDeInformacaoDoVeiculo()
    {
        _veiculo = new Veiculo()
        {
            Proprietario = "Carlos Silva",
            Placa = "ASD-1234",
            Modelo = "Variante",
            Cor = "Amarelo",
            Tipo = TipoVeiculo.Automovel
        };
        
        string dados = _veiculo.ToString();
       
        Assert.Contains("Ficha veículo:", dados);
    }

    [Fact]
    public void TestaNomeProprietarioVeiculoComMenosDeTresCaracteres()
    {
        string nomeProprietario = "Ab";
        Assert.Throws<System.FormatException>(
            () => new Veiculo(nomeProprietario)
            );
    }

    [Fact]
    public void TestaMensagemDeExcecaoDoQuartoCaracterDaPlaca()
    {
        string placa = "ADSD9999";
        var mensagem = Assert.Throws<System.FormatException>(
            () => new Veiculo().Placa = placa
        );
        
        Assert.Equal("O 4° caractere deve ser um hífen", mensagem.Message);
    }

    public void Dispose()
    {
        _testOutputHelper.WriteLine("Dispose invocado.");
    }
}