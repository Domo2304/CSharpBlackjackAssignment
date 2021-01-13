using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace S00209029_CA3
{
    class Game
    {
        public int CardsDrawn { get; set; }

        public List<Card> DealersCards { get; set; }

        //starts the game
        public void StartGame()
        {
            List<Card> playersCards = new List<Card>();
            
            
            Deck deck = new Deck();
            Card firstCard = deck.DrawCard();
            Card secondCard = deck.DrawCard();

            playersCards.Add(firstCard);
            playersCards.Add(secondCard);

            Console.WriteLine(firstCard);
            Console.WriteLine(secondCard);

            //PlayerScore += firstCard.Value;
            //PlayerScore += secondCard.Value;

            Console.WriteLine("Your score is {0}.",CalculateCardValues(playersCards));
            Console.WriteLine("Do you want to stick or twist - s/t?");

            string input = Console.ReadLine();

            //If player decides to end their drawing after two cards, the game switches to the dealer's drawing phase.
            if (input.ToLower().Equals("s"))
            {
                DealersGo(deck);
            }
            
            //Loop for player to continually draw cards.
            else
            {
                while (true)
                {
                    Card card = deck.DrawCard();
                    playersCards.Add(card);
                    Console.WriteLine(card);

                    Console.WriteLine("Your score is {0}.", CalculateCardValues(playersCards));  

                    if (CheckBust(playersCards, CalculateCardValues(playersCards)))
                    {
                        Console.WriteLine("You are bust.");
                        DealersGo(deck);
                        break;
                    }

                    Console.WriteLine("Do you want to stick or twist - s/t?");
                    input = Console.ReadLine();

                    if (input.ToLower().Equals("s"))
                    {
                        DealersGo(deck);
                        break;
                    }

                }
            }

            Console.ReadLine();
        }

        //Checks through the cards drawn by either the dealer or player to see if they have went bust
        private Boolean CheckBust(List<Card> cards, int score)
        {
            if(CalculateCardValues(cards) <= 21)
            {
                return false;
            }

            return true;
        }

        //Calculates ace-high and ace-low values of the cards player or dealer has. Returns ace-low value if ace-high is bust (above 21).
        private int CalculateCardValues(List<Card> cards)
        {
            int aceHighCardValues = 0;
            int aceLowCardValues = 0;

            foreach(Card currentCard in cards){
                aceHighCardValues += currentCard.Value;
            }


            if(aceHighCardValues <= 21)
            {
                return aceHighCardValues;
            }

            
                foreach(Card currentCard in cards)
                {
                    if(currentCard.Suit.Contains("ace of"))
                    {
                        aceLowCardValues += 1;
                    }

                    else
                    {
                        aceLowCardValues += currentCard.Value;
                    }

                }

            return aceLowCardValues;
        }

        //Simulates dealers turns in the game.
        private void DealersGo(Deck deck)
        {
            List<Card> dealersCards = new List<Card>();

            Console.WriteLine("Dealer plays!");



            while(CalculateCardValues(dealersCards) <= 17){

                if ((CalculateCardValues(dealersCards) > 21)){
                    Console.WriteLine("Dealer is bust.");
                    break;
                }

                Card card = deck.DrawCard();
                dealersCards.Add(card);

                Console.WriteLine("Dealer drew a {0}, value: {1}.", card.Suit, card.Value);

            }

            Console.WriteLine("Dealer has stuck with {0}.", CalculateCardValues(dealersCards));

            Console.ReadLine();

            
        }


        //Testing method for draw and shuffle functionality of deck.
        public void DeckTest()
        {

            Deck deck = new Deck();
            deck.DrawCard();
            Console.WriteLine();

            while (true)
            {
                Console.WriteLine("Enter any key to draw a card - X to exit!");
                string input = Console.ReadLine();

                if (input.ToLower().Equals("x"))
                {
                    break;
                }

                
                Card card = deck.DrawCard();
                if(card != null)
                {
                   Console.WriteLine(card);
                   Console.WriteLine("Cards left: {0}", 51-deck.CardsDrawn);
                }

                else{
                    Console.WriteLine("No more cards to draw");
                    Console.ReadLine();
                    break;
                }
               

            }


        }

    }
}
