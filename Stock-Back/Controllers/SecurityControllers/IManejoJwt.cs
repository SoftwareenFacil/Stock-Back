namespace Controllers.JwtControllers
{
    public interface IManejoJwt
    {
        public string GenerarToken(string name, string email, bool superAdmin);
    }
}
