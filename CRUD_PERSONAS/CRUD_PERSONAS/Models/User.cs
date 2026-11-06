namespace CRUD_PERSONAS.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
        public string Role { get; set; } = "Cajero"; // "Admin" o "Cajero"
        public string Shift { get; set; } = string.Empty; // "Mañana", "Tarde", "Noche"
    }
}