using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ProAgil.Api.Dtos
{
    public class EventoDto
    {
        // public EventoDto(int id, string local, string dataEvento, string tema, int qtdPessoas, string imagemURL, string telefone, string email)
        // {
        //     this.Id = id;
        //     this.Local = local;
        //     this.Data = dataEvento;
        //     this.Tema = tema;
        //     this.QtdPessoas = qtdPessoas;
        //     this.ImagemUrl = imagemURL;
        //     this.Telefone = telefone;
        //     this.Email = email;

        // }
        public int Id { get; set; }

        [Required(ErrorMessage = "Campo Obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "Local é entre 3 e 100 Caracters")]
        public string Local { get; set; }
        public string Data { get; set; }

        [Required(ErrorMessage = "O Tema deve ser Preeenchido")]
        public string Tema { get; set; }

        [Range(2, 120000, ErrorMessage = "Quatidade de Pessoas é entre 2 e 120000")]
        public int QtdPessoas { get; set; }
        public string ImagemUrl { get; set; }

        [Phone]
        public string Telefone { get; set; }

        [EmailAddress]
        public string Email { get; set; }
        public List<LoteDto> Lotes { get; set; }
        public List<RedeSocialDto> RedeSociais { get; set; }
        public List<PalestranteDto> Palestrantes { get; set; }
    }
}