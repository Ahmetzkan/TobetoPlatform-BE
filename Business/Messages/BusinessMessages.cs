using Entities.Concretes;

namespace Business.Messages;

public class BusinessMessages
{
    public static string DataNotFound = "Data is not found.";
    public static string DataAvailable = "This data is in use.";
    public static string NotAssignedToOccupationClass = "You have not been assigned to a class yet.";
    public static string JoinedSession = "You have attended this session before.";


    //User,Authorization
    public static string UserNotFound = "User not found.";
    public static string SuccessfulLogin = "Login is succesful.";
    public static string UserAlreadyExists = "User already exists.";
    public static string UserRegistered = "Register is succesful.";
    public static string AccessTokenCreated = "Token is created.";
    public static string AuthorizationDenied = "You dont have a authorization.";
}