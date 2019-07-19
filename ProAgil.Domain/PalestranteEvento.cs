namespace ProAgil.Domain
{
    public class PalestranteEvento
    {
        public int PalestranteId { get; set; }

        public int EnventoId { get; set; }

        public Palestrante Palestrante { get; set;}

        public Evento Evento { get; set;}
    }
}