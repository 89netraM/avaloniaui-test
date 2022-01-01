using System;
using Avalonia;

namespace avaloniaui_test;

public static class Program
{
	[STAThread]
	public static void Main(string[] args) =>
		AppBuilder.Configure<App>()
			.UsePlatformDetect()
			.LogToTrace()
			.StartWithClassicDesktopLifetime(args);
}
