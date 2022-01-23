using System;
using System.Runtime.InteropServices;
using Kompas6API5;

namespace EngineValveBuild
{
	/// <summary>
	/// Связь с КОМПАС-3D
	/// </summary>
	public class KompasConnector
    {
	    /// <summary>
		/// Экземпляр КОМПАС-3D
		/// </summary>
	    private KompasObject _entity;

		/// <summary>
		/// Свойство получающее экземпляр компаса.
		/// </summary>
	    public KompasObject KompasEntity => _entity;

	    /// <summary>
		/// Запуск связи
		/// </summary>
	    public void Start()
	    {
		    var activeKompas = GetActiveKompas(out var kompas);
		    if (!activeKompas)
		    {
			    var creatingKompas = OpenKompas(out kompas);
			    if (!creatingKompas)
			    {
				    throw new ArgumentException("KOMPAS-3D opening error.");
			    }
		    }

		    kompas.Visible = true;
		    kompas.ActivateControllerAPI();
		    _entity = kompas;
	    }

	    /// <summary>
		/// Поиск открытого экземпляра
		/// </summary>
		/// <param name="kompas">Экземпляр КОМПАС-3D</param>
		/// <returns>Уже существующий экземпляр КОМПАС-3D</returns>
	    private bool GetActiveKompas(out KompasObject kompas)
	    {
		    kompas = null;
		    try
		    {
			    kompas = (KompasObject) Marshal.GetActiveObject("KOMPAS.Application.5");
			    return true;
		    }
		    catch (COMException)
		    {
			    return false;
		    }
	    }

	    /// <summary>
		/// Создание экземпляра КОМПАС-3D
		/// </summary>
		/// <param name="kompas">Экземпляр КОМПАС-3D</param>
		/// <returns>Новый экземпляр КОМПАС-3D</returns>
	    private bool OpenKompas(out KompasObject kompas)
	    {
		    try
		    {
			    var type = Type.GetTypeFromProgID("KOMPAS.Application.5");
			    kompas = (KompasObject) Activator.CreateInstance(type);
			    return true;
		    }
		    catch (COMException)
		    {
			    kompas = null;
			    return false;
		    }
	    }

	    /// <summary>
		/// Создание документа в КОМПАС-3D
		/// </summary>
		/// <returns></returns>
	    public ksDocument3D CreateDocument3D()
	    {
		    ksDocument3D document3D = _entity.Document3D();
		    document3D.Create(false, true);
		    return document3D;
	    }

    }
}
