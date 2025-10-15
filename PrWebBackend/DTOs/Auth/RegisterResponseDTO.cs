using System.Collections.Generic;

namespace PrWebBackend.DTOs.Auth
{
    public class RegisterResponseDTO
    {
        private bool successful = false;
        private List<string> messages = new List<string>();

        public RegisterResponseDTO() { }

        public List<string> Messages { get => messages; set => messages = value; }
        public bool Successful { get => successful; set => successful = value; }
    }
}
