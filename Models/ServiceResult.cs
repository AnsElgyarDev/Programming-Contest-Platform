namespace Programming_Contest_Platform.Entity;

public struct ServiceResult
{
    public bool isSuccess { get; set; }
    public bool isFailure { get; set; }
    public string ErrorMessage { get; set; }

    public static ServiceResult Success(string token) => 
        new ServiceResult { isSuccess = true }; 

    public static ServiceResult Failure(string error) => 
        new ServiceResult { isFailure = true, ErrorMessage = error, isSuccess = false }; 
}

public struct ServiceResult<T>
{
    public bool isSuccess { get; set; }
    public bool isFailure { get; set; }
    public string ErrorMessage { get; set; }
    public T? Data { get; set; }

    public static ServiceResult<T> Success(T data) => 
        new ServiceResult<T> { isSuccess = true, Data = data }; 

    public static ServiceResult<T> Failure(string error) => 
        new ServiceResult<T> { isFailure = true, ErrorMessage = error, isSuccess = false }; 
}