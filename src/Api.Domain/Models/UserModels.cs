namespace Domain.Models;

public class UserModels
{
    private Guid _id;
						
    public Guid Id
    {
        get { return _id; }
        set { _id = value; }
    }
    private String _name;
						
    public String Name
    {
        get { return _name; }
        set { _name = value; }
    }
    private String _email;
						
    public String Email
    {
        get { return _email; }
        set { _email = value; }
    }
    private DateTime _createAt;
						
    public DateTime CreateAt
    {
        get { return _createAt; }
        set { 
            _createAt = value == null ? DateTime.UtcNow : value; 
        }
    }	
    private DateTime _updateAt;
						
    public DateTime UpdateAt
    {
        get { return _updateAt; }
        set { _updateAt = value; }
    }
}