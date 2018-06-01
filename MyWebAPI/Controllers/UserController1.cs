using Microsoft.AspNetCore.Mvc;

public class UserLogic1 {
    public void Create(User user) {
        // ...
    }
}

public class UserController1 : Controller {
    public void Register(User user){
        var logic = new UserLogic1();
        logic.Create(user);
        // ...
    }
}