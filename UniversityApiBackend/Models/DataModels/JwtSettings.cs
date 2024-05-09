namespace UniversityApiBackend.Models.DataModels
{
    public class JwtSettings
    {
        public bool ValidateIssuerSigningKey { get; set; } //validar la clave de firma del emisor
        public string IssuerSigningKey { get; set; } = string.Empty; //Clave de firma del emisor
        public bool ValidateIssuer { get; set; } = true;
        public string ValidIssuer { get; set; } = string.Empty;
        public bool ValidateAudience {  get; set; } = true;
        public string? ValidAudience { get; set; }
        public bool RequireExpirationTime { get; set; }
        public bool ValidateLifetime { get; set; } = true;
    }
}
