namespace Masiv.Core.Entities
{
    public class Roulette
    {
        public int RouletteId { get; set; }
        public string Names { get; set; }
        public decimal AmountMax { get; set; }
        public string State { get; set; }
        public bool Active { get; set; }
        public Roulette()
        {
            RouletteId = 0;
            Names = string.Empty;
            AmountMax = 0;
            State = "CREADA";
            Active = true;
        }
    }
}
