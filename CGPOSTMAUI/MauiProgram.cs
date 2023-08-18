using CGPost.Base;
using CGPost.Installers;
using CGPost.Services.IOC;
using CGPost.Services.Services;
using CGPost.ViewModels.IOC;
using CommunityToolkit.Maui;
using Material.Components.Maui.Extensions;
using Microsoft.Extensions.Logging;
using NavigationServices.Installers;

namespace CGPOSTMAUI;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.UseMauiCommunityToolkit()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-SemiBold.ttf", "OpenSansSemibold");
			});
#if DEBUG
		builder.Logging.AddDebug();
#endif
        builder
           .UseMaterialComponents(new List<string>
           {
                //generally, we needs add 6 types of font families
                "OpenSans-Regular.ttf",
                "OpenSans-Italic.ttf",
                "OpenSans-Medium.ttf",
                "OpenSans-MediumItalic.ttf",
                "OpenSans-Bold.ttf",
                "OpenSans-BoldItalic.ttf",
           });
        builder.Services.AddHttpClient();
		var assemblyPool = new AssemblyPool(typeof(MauiProgram).Assembly);
		assemblyPool.AddServices();
        IKernel kernel = new Kernel.Kernel(builder.Services);
		assemblyPool.LoadTypes();
		kernel.InstallServicesFromAssemblyPool(assemblyPool);
		kernel.InstallViewModels();
		kernel.AddPages();
		kernel.InstallNavigationServices(typeof(MauiProgram).Assembly);

		var app =  builder.Build();
        var databaseService = app.Services.GetService<IDatabaseService>();
		databaseService?.CreateOrUpgradeDatabase();
        return app;
	}
}
