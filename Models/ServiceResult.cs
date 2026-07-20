namespace Programming_Contest_Platform.Entity;

public struct ServiceResult
{
    public bool isSuccess { get; set; }
    public bool isFailure { get; set; }
    public string ErrorMessage { get; set; }
    public static ServiceResult Success() => 
        new ServiceResult { isSuccess = true }; 
    public static ServiceResult Failure(string error) => 
        new ServiceResult { isFailure = true, ErrorMessage = error }; 
} 