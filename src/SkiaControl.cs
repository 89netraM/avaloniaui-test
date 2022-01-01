using System;
using Avalonia.OpenGL;
using Avalonia.OpenGL.Controls;
using SkiaSharp;

namespace avaloniaui_test;

public class SkiaControl : OpenGlControlBase
{
#nullable disable
	private GRGlInterface grGlInterface;
	private GRContext grContext;
	private SKSurface surface;
	private SKCanvas canvas;
#nullable enable

	protected override void OnOpenGlInit(GlInterface gl, int fb)
	{
		grGlInterface = GRGlInterface.Create(gl.GetProcAddress);
		grGlInterface.Validate();
		grContext = GRContext.CreateGl(grGlInterface);
		var renderTarget = new GRBackendRenderTarget((int)Width, (int)Height, 0, 8, new GRGlFramebufferInfo((uint)fb, SKColorType.Rgba8888.ToGlSizedFormat()));
		surface = SKSurface.Create(grContext, renderTarget, GRSurfaceOrigin.BottomLeft, SKColorType.Rgba8888);
		canvas = surface.Canvas;
	}

	protected override void OnOpenGlRender(GlInterface gl, int fb)
	{
		grContext.ResetContext();
		canvas.Clear(SKColors.Cyan);
		using var red = new SKPaint { Color = SKColors.Red };
		canvas.DrawCircle((float)Width / 2.0f, (float)Height / 2.0f, (float)Math.Min(Width, Height) / 4.0f, red);
		canvas.Flush();
	}

	protected override void OnOpenGlDeinit(GlInterface gl, int fb)
	{
		canvas.Dispose();
		surface.Dispose();
		grContext.Dispose();
		grGlInterface.Dispose();
	}
}
