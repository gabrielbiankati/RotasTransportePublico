using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RotasTransportePublico.classes
{
    public class Rota
    {
        public int Numero {  get; set; }
        public string Nome { get; set; }
        public List<Parada> Paradas { get; set; }

        public Rota(int numero, string nome)
        {
            Numero = numero;
            Nome = nome;
            Paradas = new List<Parada>();
        }

        public void AdicionarParada(Parada parada)
        {
            Paradas.Add(parada);
        }

        public void RemoverParada(string nomeParada)
        {
            var parada = Paradas.Find(p => p.Nome == nomeParada);
            if (parada == null)
            {
                throw new KeyNotFoundException($"Parada '{nomeParada}' não existe.");
            }

            Paradas.Remove(parada);
        }

        public void AtualizarNome(string novoNome)
        {
            Nome = novoNome;
        }

        public void ListaParadas()
        {
            foreach (var parada in Paradas)
            {
                Console.WriteLine($"Parada: {parada.Nome}");
            }
        }
    }
}
