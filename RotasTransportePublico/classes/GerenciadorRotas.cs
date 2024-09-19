using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace RotasTransportePublico.classes
{
    public class GerenciadorRotas
    {
        public List<Rota> Rotas { get; set; }

        public GerenciadorRotas()
        {
            Rotas = new List<Rota>();
        }

        public void AdicionarRota(int numero, string nome)
        {
            if (Rotas.Any(r => r.Numero == numero))
            {
                throw new Exception("Rota com este número já existe.");
            }

            Rota novaRota = new Rota(numero, nome);
            Rotas.Add(novaRota);
        }

        public void RemoverRota(int numero)
        {
            var rota = Rotas.FirstOrDefault(r => r.Numero == numero);
            if (rota == null)
            {
                throw new Exception("Rota não encontrada.");
            }
            Rotas.Remove(rota);
        }

        public Rota BuscarRota(int numero)
        {
            return Rotas.Find(r => r.Numero == numero);
        }

        public List<Rota> ListarRotas()
        {
            return Rotas;
        }

        public bool VerificarConflitos()
        {
            foreach (var rota in Rotas)
            {
                foreach (var outraRota in Rotas)
                {
                    if (rota != outraRota)
                    {
                        foreach (var parada in rota.Paradas)
                        {
                            var conflito = outraRota.Paradas.FirstOrDefault(p => p.Nome == parada.Nome && p.HorarioChegada == parada.HorarioChegada);
                            if (conflito != null)
                            {
                                return true;
                            }
                        }
                    }
                }
            }
            return false;
        }
    }
}
