using SampleProject.IServices;
using SampleProject.Models;

namespace SampleProject.Services
{
    public class UserDataService : IUserDataService
    {   //to store user data
        private static readonly Dictionary<string, UserData> UserDataStore = new Dictionary<string, UserData>();

        //to store state of user questions
        private static readonly Dictionary<string, int> UserQuestionState = new Dictionary<string, int>();

        //an array of questions
        private static readonly string[] Questions = new[]
        {
               "Which symptoms are you experiencing?",
               "When was your last doctor's visit?"
        };

        public Question GetNextQuestion(string userId)
        {
            int questionIndex = UserQuestionState[userId];
            if (questionIndex < Questions.Length)
            {
                return new Question { QuestionText = Questions[questionIndex] };
            }
            return new Question { QuestionText = "All Question Answred" };
        }

        public int GetQuestionIndex(string userId)
        {
            throw new NotImplementedException();
        }

        public UserData GetUserData(string userId)
        {
            if(UserDataStore.ContainsKey(userId))
            {
                return UserDataStore[userId];
            }
            return null;
        }

        public void IncrementQuestionIndex(string userId)
        {
            UserQuestionState[userId]++;
        }

        public void InitializaUser(string userId)
        {
            if(!UserQuestionState.ContainsKey(userId))
            {
                UserQuestionState[userId] = 0;
            }
        }

        public bool IsValidDate(string date)
        {
            return DateTime.TryParse(date, out _);
        }

        public void SaveResponse(string userId, string response)
        {
            int questionIndex = UserQuestionState[userId];
            if (!UserDataStore.ContainsKey(userId))
            {
                UserDataStore[userId] = new UserData { UserId = userId };
            }

            if(questionIndex == 0)
            {
                UserDataStore[userId].Symptoms = response;
            }
            else if(questionIndex == 1)
            {
                if(!IsValidDate(response))
                {
                    throw new Exception("Invalid date format. Please enter the date in YYYY-MM-DD format.");
                }
                UserDataStore[userId].LastVisit = response;
            }
        }

        public bool UserExists(string userId)
        {
            return UserDataStore.ContainsKey(userId);
        }
    }
}
