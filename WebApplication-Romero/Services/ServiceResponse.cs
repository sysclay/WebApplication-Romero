namespace WebApplication_Romero.Services
{
    public class ServiceResponse
    {
        public bool Success { get; set; }
        public string? Message { get; set; }
        public string? ErrorDetails { get; set; }

        //public T? Data { get; set; } // Aquí se almacenan los datos resultantes
    }
}
