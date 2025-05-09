using AuthScapeUno;
using UIKit;
using Uno.UI.Hosting;

var host = UnoPlatformHostBuilder.Create()
    .App(() => new AuthScapeUno.App())
    .UseAppleUIKit()
    .Build();

host.Run();
