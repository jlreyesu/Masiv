using System;

namespace Masiv.Core.Entities
{
    public class Bet
    {
        public int BetId { get; set; }
        public int Number { get; set; }
        public String Color { get; set; }
        public Decimal BetAmount { get; set; }
        public DateTime DateAmount { get; set; }
        public bool Winner { get; set; }
        public Decimal PaymentxN { get; set; }
        public Decimal PaymentxColor { get; set; }
        public Decimal TotalxN { get; set; }
        public Decimal TotalxColor { get; set; }
        public Decimal Total { get; set; }
        public String State { get; set; }
        public bool Active { get; set; }
        public int ClientId { get; set; }
        public int UserId { get; set; }
        public int RouletteId { get; set; }
    }
}
