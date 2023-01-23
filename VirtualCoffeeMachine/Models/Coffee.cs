using System.ComponentModel.DataAnnotations;

namespace VirtualCoffeeMachineContAPI.Models
{
    public class Coffee
    {
        public long Id { get; set; }
        public string message { get; set; }
        public string prepared { get; set; }
    }
}
