namespace JogandoBack.API.Data.Services.RefreshTokensEntityService
{
    public interface IRefreshTokensEntityService<Response, Request> : IBaseService<Response, Request>
    {
        Response GetByToken(string token);
        Response GetByUserId(int id);
    }
}