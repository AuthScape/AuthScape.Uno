using AuthScape.Uno.Client;

namespace AuthScapeUno.Presentation;

public partial record LoginModel(IDispatcher Dispatcher, INavigator Navigator, IAuthenticationService Authentication)
{
    public string Title { get; } = "Login";


    public async ValueTask Login(CancellationToken token)
    {
        await Authenticator.Authenticate();
        //var success = await Authentication.LoginAsync(Dispatcher);
        //if (success)
        //{
        //    await Navigator.NavigateViewModelAsync<MainModel>(this, qualifier: Qualifiers.ClearBackStack);
        //}
    }

}
