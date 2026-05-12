using Lib.Avalonia.Extensions;
 
using ReactiveUI;

using System;
using System.Collections.Generic; 
using System.Threading;

namespace Lib.Avalonia;

/// <summary>Базовый класс для ViewModel.</summary>
public class ViewModelBase : ReactiveObject, IDisposable
{ 
	protected IList<IDisposable> _disposables = new List<IDisposable>();
	private int _disposed;

	protected virtual void Dispose(bool disposing)
	{
		if(Interlocked.Exchange(ref _disposed, 1) == 1)
			return;
		try
		{
			_disposables.DisposeAll();
		}
		catch(Exception) { }
	} 

	public void Dispose()
	{
		Dispose(disposing: true);
		GC.SuppressFinalize(this);
	}
}
