using System.Collections.Generic;

public interface IVerify <T> 
{

    public string validator { get; set; }
    public string errorMsgStr { get; set; }


    public string[] errorMsg { get; set; }
    public List<string> validatorCallback { get; set; }
    public bool isDirty { get; set; }
    public bool[] isError { get; set; }
    public HashSet<string> errorMsgList { get; set; }
    public T value { get; set; }

    void AddToClassList(string v);
    void RemoveFromClassList(string v);
}