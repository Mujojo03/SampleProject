//using Microsoft.Net.Http.Headers;
using SampleProject.Models;

namespace SampleProject.IServices
{
    public interface IUserDataService
    {
        //for the user data
        Question GetNextQuestion(string userId); //retrieves the next question for a user
        bool IsValidDate(string date); //validates if a given string is a valid date
        UserData GetUserData(string userId); //retrieves user data from a user
        bool UserExists(string userId); //to check if a user exists in data store
        void SaveResponse(string userId, string response); //saves user's response to the current question
        int GetQuestionIndex(string userId); //retrieve current question for a user
        void InitializaUser(string userId); //to initializa the user
        void IncrementQuestionIndex(string userId); //to move to the next question

        //has to have content of the question
    }
}
