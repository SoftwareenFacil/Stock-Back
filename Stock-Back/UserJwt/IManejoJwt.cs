namespace Stock_Back.UserJwt
{
    public interface IManejoJwt
    {
        public string GenerarToken(string email, string password);
    }
}
