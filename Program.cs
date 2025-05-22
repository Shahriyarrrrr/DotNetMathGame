// C# Math Game
// This program generates simple arithmetic problems for the user to solve.
// It keeps track of the user's score and allows them to play multiple rounds.

using System;
using System.Collections.Generic; // Required for List

namespace MathGame
{
    class Program
    {
        // Random number generator, initialized once to ensure better randomness.
        static Random random = new Random();
        static List<string> gameHistory = new List<string>(); // To store history of games played

        static void Main(string[] args)
        {
            Console.WriteLine("Welcome to the Simple Math Game!");
            Console.WriteLine("---------------------------------");

            bool playAgain = true;
            int totalScore = 0;
            int roundsPlayed = 0;

            while (playAgain)
            {
                roundsPlayed++;
                int currentRoundScore = PlayRound(roundsPlayed);
                totalScore += currentRoundScore;

                Console.WriteLine($"\nYour score for round {roundsPlayed} is: {currentRoundScore}");
                Console.WriteLine($"Your total score so far is: {totalScore}");
                Console.WriteLine("---------------------------------");

                Console.Write("Do you want to play another round? (yes/no): ");
                string playAgainInput = Console.ReadLine().Trim().ToLower();
                playAgain = (playAgainInput == "yes" || playAgainInput == "y");
                Console.Clear(); // Clear the console for the next round or game end
            }

            Console.WriteLine("\nGame Over!");
            Console.WriteLine($"You played {roundsPlayed} round(s).");
            Console.WriteLine($"Your final score is: {totalScore}");

            ShowGameHistory();

            Console.WriteLine("\nThanks for playing! Press any key to exit.");
            Console.ReadKey();
        }

        /// <summary>
        /// Plays a single round of the math game.
        /// </summary>
        /// <param name="roundNumber">The current round number.</param>
        /// <returns>The score achieved in this round.</returns>
        static int PlayRound(int roundNumber)
        {
            Console.WriteLine($"\n--- Round {roundNumber} ---");
            int score = 0;
            int numberOfQuestions = 5; // Number of questions per round

            for (int i = 0; i < numberOfQuestions; i++)
            {
                // Generate two random numbers
                int num1 = random.Next(1, 21); // Numbers between 1 and 20
                int num2 = random.Next(1, 21); // Numbers between 1 and 20

                // Choose a random operation
                char[] operations = { '+', '-', '*', '/' };
                char operation = operations[random.Next(operations.Length)];

                // Ensure division results in a whole number and num1 >= num2 for subtraction to avoid negative results (optional simplification)
                if (operation == '/')
                {
                    // Ensure num2 is not zero and num1 is divisible by num2
                    // Regenerate numbers if conditions are not met to ensure a simple division problem
                    while (num2 == 0 || num1 % num2 != 0)
                    {
                        num1 = random.Next(1, 21);
                        num2 = random.Next(1, 21); // Ensure num2 is not 0 for division
                        if (num2 == 0) num2 = 1; // Avoid division by zero by setting num2 to 1 if it's 0
                    }
                }
                else if (operation == '-')
                {
                    // Optional: Ensure num1 is greater than or equal to num2 to keep results positive
                    if (num1 < num2)
                    {
                        // Swap numbers
                        int temp = num1;
                        num1 = num2;
                        num2 = temp;
                    }
                }


                // Calculate the correct answer
                int correctAnswer = 0;
                switch (operation)
                {
                    case '+':
                        correctAnswer = num1 + num2;
                        break;
                    case '-':
                        correctAnswer = num1 - num2;
                        break;
                    case '*':
                        correctAnswer = num1 * num2;
                        break;
                    case '/':
                        correctAnswer = num1 / num2;
                        break;
                }

                // Display the question
                Console.Write($"\nQuestion {i + 1}: What is {num1} {operation} {num2}? ");
                string userAnswerStr = Console.ReadLine();

                // Validate user input
                if (int.TryParse(userAnswerStr, out int userAnswer))
                {
                    if (userAnswer == correctAnswer)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("Correct!");
                        Console.ResetColor();
                        score++;
                        gameHistory.Add($"Round {roundNumber}, Q{i + 1}: {num1} {operation} {num2} = {correctAnswer}. Your answer: {userAnswer} (Correct)");
                    }
                    else
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine($"Incorrect. The correct answer is {correctAnswer}.");
                        Console.ResetColor();
                        gameHistory.Add($"Round {roundNumber}, Q{i + 1}: {num1} {operation} {num2} = {correctAnswer}. Your answer: {userAnswer} (Incorrect)");
                    }
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Invalid input. Please enter a number.");
                    Console.ResetColor();
                    gameHistory.Add($"Round {roundNumber}, Q{i + 1}: {num1} {operation} {num2} = {correctAnswer}. Your answer: '{userAnswerStr}' (Invalid Input)");
                    // Optionally, you could penalize or re-ask the question
                }
            }
            return score;
        }

        /// <summary>
        /// Displays the history of all questions and answers.
        /// </summary>
        static void ShowGameHistory()
        {
            Console.WriteLine("\n--- Game History ---");
            if (gameHistory.Count == 0)
            {
                Console.WriteLine("No games played yet or history is empty.");
                return;
            }
            foreach (string entry in gameHistory)
            {
                Console.WriteLine(entry);
            }
            Console.WriteLine("--------------------");
        }
    }
}
