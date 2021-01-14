using System;
using System.Collections.Generic;

namespace S00209029_CA3
{
    class Game
    {

        public int CardsDrawn { get; set; }

        public List<Card> DealersCards { get; set; }
        public List<Card> PlayersCards { get; set; }

        public bool DealerBust { get; set; }
        public bool PlayerBust { get; set; }
        public bool PlayerHasBlackJack { get; set; }
        public bool DealerHasBlackJack { get; set; }


        //starts the game
        public void StartGame()
        {
            DealerBust = false;
            PlayerBust = false;

            //Lists to represent the cards the player and dealer draw.
            DealersCards = new List<Card>();
            PlayersCards = new List<Card>();

            //creates the deck and and draws two cards.
            Deck deck = new Deck();
            Card firstCard = deck.DrawCard();
            Card secondCard = deck.DrawCard();

            //adds the two drawn cards to the player's card list.
            PlayersCards.Add(firstCard);
            PlayersCards.Add(secondCard);

            Console.WriteLine(firstCard);
            Console.WriteLine(secondCard);

            //Checks if player has BlackJack.
            if (IsBlackJack(PlayersCards))
            {
                PlayerHasBlackJack = true;
                Console.WriteLine("Player has Blackjack!");
                DealersGo(deck);
            }
            
            else
            {
                Console.WriteLine("Your score is {0}.", CalculateCardValues(PlayersCards));
                Console.WriteLine("Do you want to stick or twist - s/t?");

                string input = Console.ReadLine();

                //If player decides to end their drawing after two cards, the game switches to the dealer's drawing phase.
                if (input.ToLower().Equals("s"))
                {
                    DealersGo(deck);
                }

                //Loop for player to continually draw cards until bust or they decide to stick.
                else
                {
                    while (true)
                    {
                        Card card = deck.DrawCard();
                        
                       
                        PlayersCards.Add(card);
                        
                        
                        Console.WriteLine(card);

                        Console.WriteLine("Your score is {0}.", CalculateCardValues(PlayersCards));

                        //Checks if player has gone bust after drawing another card.
                        if (CheckBust(PlayersCards, CalculateCardValues(PlayersCards)))
                        {
                            Console.WriteLine("You are bust.");
                            PlayerBust = true;
                            DealersGo(deck);
                            break;
                        }

                        Console.WriteLine("Do you want to stick or twist - s/t?");
                        input = Console.ReadLine();

                        //If player decides to stick, switches to Dealers go.
                        if (input.ToLower().Equals("s"))
                        {
                            DealersGo(deck);
                            break;
                        }

                    }

                }

            }
            //Method for deciding who won the game.
            DeclareWinner();
        }

        //Checks through the cards drawn by either the dealer or player to see if they have went bust
        private Boolean CheckBust(List<Card> cards, int score)
        {
            if (CalculateCardValues(cards) <= 21)
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
            int numberOfAces = 0;

            foreach (Card currentCard in cards)
            {
                aceHighCardValues += currentCard.Value;
                

                //Counts number of aces in the player/dealer has.
                if (currentCard.Suit.Contains("ace")) { numberOfAces++; }
            }

            //returns ace-high value if value is not bust.
            if (aceHighCardValues <= 21) { return aceHighCardValues; }

            //loop for if ace-high is bust and cards contains 0-1 aces

            else if (aceHighCardValues > 21 && numberOfAces <= 1)
            {

                foreach (Card currentCard in cards)
                {
                    if (currentCard.Suit.Contains("ace"))
                    {
                        aceLowCardValues += 1;
                    }

                    else
                    {
                        aceLowCardValues += currentCard.Value;
                    }

                }
            }
            //calculates best ace-low value if there is more than 1 ace.
            else if(aceHighCardValues > 21 && numberOfAces > 1)
            {
                int counter = 0;
                int numberOfLowAcesToUse = 1;
                int lowAcesUsed = 0;

                while (counter < numberOfAces)
                {
                    aceLowCardValues = 0;
                    lowAcesUsed = 0;
                    foreach (Card currentCard in cards)
                    {
                    
                        if (currentCard.Suit.Contains("ace") && (lowAcesUsed < numberOfLowAcesToUse))
                        {
                            aceLowCardValues += 1;
                            lowAcesUsed++;
                        }
                        else if (currentCard.Suit.Contains("ace") && (lowAcesUsed >= numberOfLowAcesToUse))
                        {
                            aceLowCardValues += 11;
                        }
                        else {  aceLowCardValues += currentCard.Value; }
                    }


                    //if, on this iteration, the ace-low value is less than 21, it breaks out of the loop.
                    if (aceLowCardValues <= 21)
                    { break; }
                    counter++;
                    numberOfLowAcesToUse++;
                }

            }

            return aceLowCardValues;
        }


        //Simulates dealers turns in the game.
        private void DealersGo(Deck deck)
        {
            Console.WriteLine("Dealer plays!\n");

            //Dealer draws cards until either bust or they have reached atleast a hand value of 17.
            while (CalculateCardValues(DealersCards) <= 17)
            {
                
                Card card = deck.DrawCard();
                DealersCards.Add(card);

                //Prints the details of the card drawn to the console.
                if (card.Suit.Contains("ace"))
                {
                    Console.WriteLine("Dealer drew a {0}. Value is {1}/1.", card.Suit, card.Value);
                }
                else if (card.Suit.Contains("of"))
                {
                    Console.WriteLine("Dealer drew a {0}. Value is {1}.", card.Suit, card.Value);
                }
                else if (card.Value == 8)
                {
                    Console.WriteLine("Dealer drew an {0} of {1}. Value is {2}.", card.Value, card.Suit, card.Value);
                }
                else
                {
                    Console.WriteLine("Dealer drew a {0} of {1}. Value: is {2}.", card.Value, card.Suit, card.Value);
                }

                //checks if dealer has BlackJack once they have drawn two cards. If so, it exits the function and sets DealerHasBlackJack boolean to true.
                if (DealersCards.Count == 2 && IsBlackJack(DealersCards))
                {
                    DealerHasBlackJack = true;
                    Console.WriteLine("Dealer has BlackJack!");
                    Console.ReadLine();
                    return;
                }

            }

            //After drawing, this checks if dealer went bust or stuck and prints the appropriate text to the console.
            if (CalculateCardValues(DealersCards) > 21)
            {
                DealerBust = true;
                Console.WriteLine("Dealer is bust with {0}.", CalculateCardValues(DealersCards));
            }
            else
            {
                Console.WriteLine("Dealer has stuck with {0}.", CalculateCardValues(DealersCards));
            }


            Console.ReadLine();

        }

        //Calculates the winner.
        private void DeclareWinner()
        {

            int playerScore = CalculateCardValues(PlayersCards);
            int dealersScore = CalculateCardValues(DealersCards);

            if (PlayerBust == false && DealerBust == true)
            {
                Console.WriteLine("Player wins!");
                return;
            }

            if (PlayerBust)
            {
                Console.WriteLine("Dealer wins!");
                return;
            }

            if (playerScore > dealersScore)
            {
                Console.WriteLine("Player wins: {0} to {1}!", playerScore, dealersScore);
                return;
            }

            else if (dealersScore > playerScore)
            {
                Console.WriteLine("Dealer wins: {0} to {1}!", dealersScore, playerScore);
                return;
            }
            else
            {
                //In the event of a tie, it checks if one of the players has BlackJack and declares them the winner. If not, it announces a tie.
                Console.WriteLine("Player and dealer have equal score: {0}-{1}", dealersScore, playerScore);
                if (DealerHasBlackJack && PlayerHasBlackJack != true)
                {
                    Console.WriteLine("Dealer has BlackJack and player does not.");
                    Console.WriteLine("Dealer wins!");
                }

                else if (PlayerHasBlackJack && DealerHasBlackJack != true)
                {
                    Console.WriteLine("Player has BlackJack and dealer does not.");
                    Console.WriteLine("Player wins!");
                }

                else if (PlayerHasBlackJack && DealerHasBlackJack) { Console.WriteLine("Dealer and player tie on BlackJack"); }

                else { Console.WriteLine("Player and dealer tie!"); }

            }
        }

        //Checks if the dealer or player's opening cards are Blackjack: i.e., Ace and a 10.
        private bool IsBlackJack(List<Card> deck)
        {
            bool hasAce = false;
            bool hasTen = false;

            foreach (Card card in deck)
            {
                if (card.Suit.Contains("ace"))
                {
                    hasAce = true;
                }

                if (card.Value == 10)
                {
                    hasTen = true;
                }
            }

            if (hasTen && hasAce)
            {
                return true;
            }

            return false;
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
                if (card != null)
                {
                    Console.WriteLine(card);
                    Console.WriteLine("Cards left: {0}", 51 - deck.CardsDrawn);
                }

                else
                {
                    Console.WriteLine("No more cards to draw");
                    Console.ReadLine();
                    break;
                }

            }

        }

    }
}
