namespace Controllers.JwtControllers
{
    public interface IManejoJwt
    {
        public string GenerarToken(string email, bool superAdmin);
    }
}
