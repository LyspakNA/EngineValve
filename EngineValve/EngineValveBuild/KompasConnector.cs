using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Kompas6API5;
using Kompas6Constants;
using Kompas6Constants3D;

namespace EngineValveBuild
{
    public class KompasConnector
    {
	    private KompasObject _entity;

	    public void Start()
	    {
		    var activeKompas = GetActiveKompas(out var kompas);
		    if (!activeKompas)
		    {
			    var creatingKompas = OpenKompas(out kompas);
			    if (!creatingKompas)
			    {
				    throw new ArgumentException("Не удалось открыть КОМПАС-3D.");
			    }
		    }

		    kompas.Visible = true;
		    kompas.ActivateControllerAPI();
		    _entity = kompas;
	    }

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

	    public ksDocument3D CreateDocument3D()
	    {
		    ksDocument3D document3D = _entity.Document3D();
		    document3D.Create(false, true);
		    return document3D;
	    }
    }
}
