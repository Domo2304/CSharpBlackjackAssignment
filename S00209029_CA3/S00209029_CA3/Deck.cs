using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S00209029_CA3
{
    class Deck
    {

        private List<Card> deck;
        public int CardsDrawn { get; set; }
        public bool IsEmpty { get; set; }

        //Constructtor for deck.
        public Deck()
        {
            deck = new List<Card>();
            FillDeck();
            ShuffleDeck();
            IsEmpty = false;
        }

        //Creates the cards and adds them to the deck.
        private void FillDeck()
        {

            for(int i = 2; i <= 10; i++)
            {
                Card spade = new Card("spades", i);
                Card club = new Card("clubs", i);
                Card heart = new Card("hearts", i);
                Card diamond = new Card("diamonds", i);
                deck.Add(spade);
                deck.Add(club);
                deck.Add(heart);
                deck.Add(diamond);

            }

            string[] cardTypes = { "spades", "diamonds", "hearts", "clubs" };

            for(int i = 0; i < 4; i++)
            {
                Card king = new Card("king of " + cardTypes[i], 10);
                deck.Add(king);
                Card queen = new Card("queen of " + cardTypes[i], 10);
                deck.Add(queen);
                Card jack = new Card("jack of " + cardTypes[i], 10);
                deck.Add(jack);
                Card ace = new Card("ace of " + cardTypes[i], 11);
                deck.Add(ace);
            }            

        }

        //Shuffles deck of cards
        public void ShuffleDeck()
        {
            Random random = new Random();

            for(int i = deck.Count-1; i >0; --i)
            {
                int nextRandom = random.Next(i + 1);
                Card temp = deck.ElementAt(i);
                deck[i] = deck[nextRandom];
                deck[nextRandom] = temp;
            }

        }

        //Draws a card from the deck, if the deck is not empty
        public Card DrawCard()
        {
            //DEGUG LINES
            /*if(TestCounter == 0)
            {
                Card ace = new Card("ace of hearts", 11);
                TestCounter++;
                return ace;
            }
            //DEGUG LINES FOR GUARANTEED BLACKJACK ON PLAYER DRAW
            if (TestCounter == 1)
            {
                Card myCard = new Card("ten of hearts", 10);
                TestCounter++;
                return myCard;
            }*/

            if (CardsDrawn == deck.Count-1)
            {
                IsEmpty = true;
                return null;
            }
            
            Card card = deck[CardsDrawn];
            CardsDrawn++;
            return card;
        }

    }
}
