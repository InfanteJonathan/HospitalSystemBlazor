namespace HospitalSystemBlazor.Web.Utilities
{
    public class MensajeOperacion
    {
        public bool Exito { get; set; }
        public string Mensaje { get; set; }
        public int? Value { get; set; } 
    }

    public class AuthResponse
    {
        public bool isSucces { get; set; }
        public string message { get; set; }
    }
}
