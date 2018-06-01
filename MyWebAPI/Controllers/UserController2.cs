using Microsoft.AspNetCore.Mvc;

public interface IUserLogic {
    void Create(User user);
}

public class UserLogic2 : IUserLogic {
    public void Create(User user) {
        // ...
    }
}

public class UserController2 : Controller {
    private readonly IUserLogic _userLogic;

    public UserController2() {
        _userLogic = new UserLogic2();
    }

    public void Register(User user){
        _userLogic.Create(user);
        // ...
    }
}