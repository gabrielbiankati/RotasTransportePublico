using RotasTransportePublico.classes;

namespace RotasTransportePublico.Tests
{
    public class UnitTest1
    {
        [Fact]
        public void AdicionarRota_DeveAdicionarRotaValida()
        {
            // Arrange
            var gerenciador = new GerenciadorRotas();

            // Act
            gerenciador.AdicionarRota(1, "Rota 1");

            // Assert
            Assert.Equal("Rota 1", gerenciador.Rotas[0].Nome);
        }

        [Fact]
        public void AdicionarParada_DeveAdicionarParadaValida()
        {
            // Arrange
            var rota = new Rota(2, "Rota 2");
            var parada = new Parada("Parada Inicial", new TimeSpan(7, 0, 0), new TimeSpan(7, 3, 0));

            // Act
            rota.AdicionarParada(parada);

            // Assert
            Assert.Equal("Parada Inicial", rota.Paradas[0].Nome);
        }

        [Fact]
        public void AdicionarRota_DeveLancarExcecaoParaRotaDuplicada()
        {
            // Arrange
            var gerenciador = new GerenciadorRotas();
            gerenciador.AdicionarRota(5, "Rota 5");

            // Assert
            var rotaDulicada = Assert.Throws<Exception>(() => gerenciador.AdicionarRota(5, "Rota 5"));
        }

        [Fact]
        public void RemoverRota_DeveRemoverRotaExistente()
        {
            // Arrange
            var gerenciador = new GerenciadorRotas();
            gerenciador.AdicionarRota(7, "Rota 7");

            // Act
            gerenciador.RemoverRota(7);

            // Assert
            Assert.Empty(gerenciador.Rotas);
        }

        [Fact]
        public void RemoverRota_DeveLancarExcecaoParaRotaInexistente()
        {
            // Arrange
            var gerenciador = new GerenciadorRotas();

            // Assert
            var rotaInexistente = Assert.Throws<Exception>(() => gerenciador.RemoverRota(444));
        }

        [Fact]
        public void RemoverParada_DeveRemoverParadaExistente()
        {
            // Arrange
            var rota = new Rota(10, "Rota 10");
            var parada = new Parada("Parada 10", TimeSpan.Parse("17:00"), TimeSpan.Parse("17:30"));
            rota.AdicionarParada(parada);

            // Act
            rota.RemoverParada("Parada 10");

            // Assert
            Assert.Empty(rota.Paradas);
        }

        [Fact]
        public void RemoverParada_DeveLancarExcecaoParaParadaInexistente()
        {
            // Arrange
            var rota = new Rota(1, "Rota 1");

            // Assert
            var paradaNula = Assert.Throws<Exception>(() => rota.RemoverParada("Parada Inexistente"));
        }

        [Fact]
        public void AtualizarNome_DeveAtualizarNomeCorretamente()
        {
            // Arrange
            var nome = new Rota(1, "Rota 1");

            // Act
            nome.AtualizarNome("Rota Curitiba");

            //Assert
            Assert.Equal("Rota Curitiba", nome.Nome);
        }

        [Fact]
        public void ListarRotas_DeveListarTodasAsRotas()
        {
            // Arrange
            var gerenciador = new GerenciadorRotas();
            gerenciador.AdicionarRota(1, "Rota 1");
            gerenciador.AdicionarRota(2, "Rota 2");

            // Act
            var rotas = gerenciador.ListarRotas();

            // Assert
            Assert.Equal(2, rotas.Count);
        }

        [Fact]
        public void ListarParadas_DeveListarTodasParadas()
        {
            // Arrange
            var rota = new Rota(1, "Rota Francisco Beltrao");
            var parada1 = new Parada("Parada 1", new TimeSpan(7, 0 ,0), new TimeSpan(7, 50, 0));
            var parada2 = new Parada("Parada 2", new TimeSpan(15, 0, 0), new TimeSpan(15, 30, 0));

            rota.AdicionarParada(parada1);
            rota.AdicionarParada(parada2);

            // Act
            var paradas = rota.Paradas;

            // Assert
            Assert.Equal(2, paradas.Count);
            Assert.Equal("Parada 1", paradas[0].Nome);
            Assert.Equal("Parada 2", paradas[1].Nome);
        }

        [Fact]
        public void BuscarRota_DeveRetornarRotaCorreta()
        {
            // Arrange
            var gerenciador = new GerenciadorRotas();
            gerenciador.AdicionarRota(1, "Rota 1");
            gerenciador.AdicionarRota(2, "Rota 2");

            // Act
            var rota = gerenciador.BuscarRota(2);

            //Assert
            Assert.Equal("Rota 2", rota.Nome);
        }

        [Fact]
        public void BuscarRota_DeveRetornarNuloParaRotaInexistente()
        {
            // Arrange
            var gerenciador = new GerenciadorRotas();
            gerenciador.AdicionarRota(1, "Rota 1");

            // Act
            var rota = gerenciador.BuscarRota(25);

            //Assert
            Assert.Null(rota);
        }

        [Fact]
        public void VerificarConflitos_DeveIdentificarConflitosCorretamente()
        {
            // Arrange
            var gerenciador = new GerenciadorRotas();

            var rota1 = new Rota(1, "Rota 1");
            rota1.AdicionarParada(new Parada("Parada 1", TimeSpan.Parse("08:00"), TimeSpan.Parse("08:10")));

            var rota2 = new Rota(2, "Rota 2");
            rota2.AdicionarParada(new Parada("Parada 1", TimeSpan.Parse("08:00"), TimeSpan.Parse("08:10")));

            gerenciador.AdicionarRota(1, "Rota 1");
            gerenciador.AdicionarRota(2, "Rota 2");

            // Act
            var conflito = gerenciador.VerificarConflitos();

            // Assert
            Assert.False(conflito); 
        }

        [Fact]
        public void VerificarConflitos_AposRemoverRota_DeveRetornarFalso()
        {
            // Arrange
            var gerenciador = new GerenciadorRotas();

            var rota1 = new Rota(1, "Rota 1");
            rota1.AdicionarParada(new Parada("Parada 1", TimeSpan.Parse("08:00"), TimeSpan.Parse("08:10")));

            var rota2 = new Rota(2, "Rota 2");
            rota2.AdicionarParada(new Parada("Parada 1", TimeSpan.Parse("08:00"), TimeSpan.Parse("08:10")));

          
            gerenciador.AdicionarRota(1, "Rota 1");
            gerenciador.AdicionarRota(2, "Rota 2");

            var existeConflitoAntes = gerenciador.VerificarConflitos();
            Assert.False(existeConflitoAntes); 

            // Act
            gerenciador.RemoverRota(2); 

            // Assert
            var existeConflitoDepois = gerenciador.VerificarConflitos();
            Assert.False(existeConflitoDepois); 
        }
    }
}