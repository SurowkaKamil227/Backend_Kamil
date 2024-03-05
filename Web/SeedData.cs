using ApplicationCore.Interfaces.Repository;
using BackendLab01;
using System.Linq;
using System.Collections.Generic; // Make sure to include this for List<>
using System.Threading.Tasks;

namespace Infrastructure.Memory;
public static class SeedData
{
    public static async Task SeedAsync(this WebApplication app)
    {
        using (var scope = app.Services.CreateScope())
        {
            var provider = scope.ServiceProvider;
            var quizRepo = provider.GetRequiredService<IGenericRepository<Quiz, int>>();
            var quizItemRepo = provider.GetRequiredService<IGenericRepository<QuizItem, int>>();

            // Check if data already exists
            var quizzes = await quizRepo.FindAllAsync();
            if (!quizzes.Any()) // Use FindAllAsync() to check if the database is empty
            {
                // Create first quiz
                var quiz1 = new Quiz { Title = "Basic Mathematics" };
                quizRepo.Add(quiz1); // Add quiz to repository

                // Add questions to the first quiz
                var q1 = new QuizItem(0, "What is 2 + 2?", new List<string>{"B", "C", "D"}, "4");
                var q2 = new QuizItem(0, "What is 5 - 3?", new List<string>{"A", "B", "E"}, "2");
                var q3 = new QuizItem(0, "What is 7 * 6?", new List<string>{"A", "B", "F"}, "42");
                quizItemRepo.Add(q1);
                quizItemRepo.Add(q2);
                quizItemRepo.Add(q3);

                // Create second quiz
                var quiz2 = new Quiz { Title = "World History" };
                quizRepo.Add(quiz2); // Add second quiz to repository

                // Add questions to the second quiz
                var q4 = new QuizItem(0, "In which year did World War II begin?", new List<string>{"1940", "1938", "1941"}, "1939");
                var q5 = new QuizItem(0, "Who invented the lightbulb?", new List<string>{"Nikola Tesla", "Benjamin Franklin", "James Watt"}, "Thomas Edison");
                var q6 = new QuizItem(0, "In which year did the fall of Constantinople occur?", new List<string>{"1443", "1463", "1435"}, "1453");
                quizItemRepo.Add(q4);
                quizItemRepo.Add(q5);
                quizItemRepo.Add(q6);

                // Assuming changes are saved immediately upon Add operations as the interface does not have a Save method.
            }
        }
    }
}
