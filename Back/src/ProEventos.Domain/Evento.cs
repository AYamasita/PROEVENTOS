using System;
using System.Collections.Generic;

namespace ProEventos.Domain
{
    public class Evento
    {
        public Evento(int id, string local, string tema, int qtdPessoas, string imagemUrl, string telefone, string email)
        {
            this.Id = id;
            this.Local = local;
            this.Tema = tema;
            this.QtdPessoas = qtdPessoas;
            this.ImagemUrl = imagemUrl;
            this.Telefone = telefone;
            this.Email = email;

        }
        public int Id { get; set; }     
        public string Local { get; set; }
        public DateTime? DataEvento { get; set; }
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }

        public string ImagemUrl { get; set; }

        public string Telefone { get; set; }

        public string Email { get; set; }

        public IEnumerable<Lote> Lotes { get; set; }

        public IEnumerable<RedeSocial> RedesSociais { get; set; }

        public IEnumerable<PalestranteEvento> PalestrantesEventos { get; set; }

    }
}