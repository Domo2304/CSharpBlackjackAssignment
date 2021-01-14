using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S00209029_CA3
{
    class Card
    {
        public int Value { get; set; }
        public string Suit { get; set; }

        public Boolean IsDrawn { get; set; }

        public Card(string suit, int value)
        {
            Suit = suit;
            Value = value;
            IsDrawn = false;
        }

        public void DrawCard()
        {
            IsDrawn = true;
        }

        public override string ToString()
        {

            if (Suit.Contains("ace"))
            {
                return String.Format("Card dealt is a {0}. Value is either 11 or 1.", Suit, Value);
            }

            if (Suit.Contains("of"))
            {
                return String.Format("Card dealt is a {0}. Value is {1}.", Suit, Value);
            }
            
            
            return String.Format("Card dealt is is the {0} of {1}. Value is {2}.", Value, Suit, Value);
        }

    }
}
