namespace GameStop;

public enum StatusCode
{
    UserNotFound = 0,
    UserAlreadyExists = 1,
        
    ProductNotFound = 10,

    OrderNotFound = 20,

    OK = 200,
    InternalServerError = 500
}