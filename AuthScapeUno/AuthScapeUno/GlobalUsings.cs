global using System.Collections.Immutable;
global using Microsoft.Extensions.DependencyInjection;
global using Microsoft.Extensions.Hosting;
global using Microsoft.Extensions.Localization;
global using Microsoft.Extensions.Logging;
global using Microsoft.Extensions.Options;
global using AuthScapeUno.Models;
global using AuthScapeUno.Presentation;
global using AuthScapeUno.Services.Endpoints;
global using AuthScapeUno.DataContracts;
global using AuthScapeUno.DataContracts.Serialization;
global using AuthScapeUno.Services.Caching;
global using AuthScapeUno.Client;
global using Uno.Extensions.Http.Kiota;
#if MAUI_EMBEDDING
global using AuthScapeUno.MauiControls;
#endif
global using ApplicationExecutionState = Windows.ApplicationModel.Activation.ApplicationExecutionState;
[assembly: Uno.Extensions.Reactive.Config.BindableGenerationTool(3)]
