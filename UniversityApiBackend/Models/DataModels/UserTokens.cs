namespace UniversityApiBackend.Models.DataModels
{
    public class UserTokens
    {
        public int Id { get; set; }
        public string Token { get; set; }
        public string UserName{ get; set;}
        public TimeSpan Validity { get; set; } //tiempo que tendra el Token para caducar
        public string RefreshToken { get; set; } = string.Empty;
        public string EmailId { get; set; }
        public Guid GuidId { get; set; } //este tipo de datos Guid es para generar un identificador unico 
        public DateTime ExpiredTime { get; set; } // tiempo de expiracion de nuestro token


    }
}
