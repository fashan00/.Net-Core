using System.Threading.Tasks;

namespace MyMVC.Hubs {
    public interface IChatHub {
        // 這個方法是用來發出 Message 給 Client
        Task ServerMessage (string message);
    }
}