namespace OmidProject.Frameworks.Contracts.Common;

public class ACLResult<TResult> where TResult : class
{
    public bool HasError { get; set; } = false;
    public string Message { get; set; }
    public TResult Result { get; set; }

    public ACLResult() { }

    public ACLResult(TResult result = null, bool hasError = false, string message = "")
    {
        HasError = hasError;
        Result = result;
        Message = message;
    }

}