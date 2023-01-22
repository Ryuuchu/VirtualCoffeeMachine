using System.ComponentModel.DataAnnotations;

namespace VirtualCoffeeMachineContAPI.Models
{
    public class Coffee
    {
        private static string COFFEE = "Your piping hot coffee is ready", TEA = "I’m a teapot";

        public long Id { get; set; }
        public string message 
        { 
            get => message;
            set {
                if (prepared.Contains("04-01"))
                {
                    message = TEA;
                }
                else
                {
                    message = COFFEE;
                }
            }
        }
        public string prepared 
        { 
            get => prepared; 
            set 
            { 
                DateTime.Now.ToString("yyyy-MM-ddTHH\\:mm\\:ss.fffffffzzz"); 
            } 
        }
    }
}
