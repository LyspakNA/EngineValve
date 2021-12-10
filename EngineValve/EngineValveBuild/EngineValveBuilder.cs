using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineValveParameter;
using Kompas6API5;

namespace EngineValveBuild
{
	public class EngineValveBuilder
	{
		private EngineValveParameters _parameters;
		private ksPart _part;

		public EngineValveBuilder(ksDocument3D document3D, EngineValveParameters parameters)
		{
			_part = document3D.GetPart(-1);
			_parameters = parameters;
		}

		public void BuildEngineValve()
		{
			BuildPlate();
			BuildStem();
			FilletPlate();
			ChamferPlate();
			BuildGroove();
			ChamferStem();
		}

		private void BuildPlate()
		{
			ksEntity planeXOY = _part.GetDefaultEntity(1);
			var length = _parameters.ThicknessPlate + _parameters.LengthChamfer;
			CreateCylinder(planeXOY,_parameters.DiameterPlate,length);
		}

		private void BuildStem()
		{
			ksEntity planeOffset = _part.NewEntity(14);
			ksPlaneOffsetDefinition planeOffsetDefinition = planeOffset.GetDefinition();

			planeOffsetDefinition.direction = true;
			planeOffsetDefinition.offset = _parameters.ThicknessPlate;

			ksEntity planeXOY = _part.GetDefaultEntity(1);
			planeOffsetDefinition.SetPlane(planeXOY);

			planeOffset.Create();

			var length = _parameters.LengthValve - _parameters.ThicknessPlate;

			CreateCylinder(planeOffset, _parameters.DiameterStem, length);
		}

		private void CreateCircle(ksDocument2D document2D, double diameter)
		{
			document2D.ksCircle(0, 0, diameter / 2, 1);
		}

		private void CreateExtrusion(double length, ksEntity sketch)
		{
			ksEntity extrusion = _part.NewEntity(24);
			ksBaseExtrusionDefinition extrusionDefinition = extrusion.GetDefinition();

			extrusionDefinition.SetSideParam(true, 0, length);
			extrusionDefinition.SetSketch(sketch);
			extrusion.Create();
		}

		private void CreateCylinder(ksEntity plane, double diameter, double length)
		{
			ksEntity sketch = _part.NewEntity(5);
			ksSketchDefinition sketchDefinition = sketch.GetDefinition();

			sketchDefinition.SetPlane(plane);
			sketch.Create();

			ksDocument2D document2D = sketchDefinition.BeginEdit();

			CreateCircle(document2D, diameter);

			sketchDefinition.EndEdit();

			CreateExtrusion(length, sketch);
		}

		private void CreateFillet(ksEntity face, double radius)
		{
			ksEntity fillet = _part.NewEntity(34);

			ksFilletDefinition filletDefinition = fillet.GetDefinition();

			filletDefinition.radius = radius;
			filletDefinition.tangent = false;

			ksEntityCollection entityCollectionFillet = filletDefinition.array();
			entityCollectionFillet.Add(face);

			fillet.Create();
		}

		private void FilletPlate()
		{
			ksEntityCollection faceCollection = _part.EntityCollection(7);
			var length = _parameters.ThicknessPlate + _parameters.LengthChamfer;
			faceCollection.SelectByPoint(_parameters.DiameterStem/2, 0, length);

			ksEntity baseFace = faceCollection.First();
			CreateFillet(baseFace, _parameters.RadiusTransition);
		}

		private void CreateChamfer(ksEntity face, double length)
		{
			ksEntity chamfer = _part.NewEntity(33);
			ksChamferDefinition chamferDefinition = chamfer.GetDefinition();

			chamferDefinition.tangent = false;
			chamferDefinition.SetChamferParam(true, length, length);

			ksEntityCollection entityCollectionChamfer = chamferDefinition.array();
			entityCollectionChamfer.Add(face);

			chamfer.Create();
		}

		private void ChamferPlate()
		{
			ksEntityCollection faceCollection = _part.EntityCollection(7);
			var length = _parameters.ThicknessPlate + _parameters.LengthChamfer;
			faceCollection.SelectByPoint(_parameters.DiameterPlate / 2, 0, length);

			ksEntity baseFace = faceCollection.First();
			CreateChamfer(baseFace, _parameters.LengthChamfer);
		}

		private void ChamferStem()
		{
			const double length = 1.0;
			ksEntityCollection faceCollection = _part.EntityCollection(7);
			faceCollection.SelectByPoint(_parameters.DiameterStem/2, 
				0, _parameters.LengthValve);

			ksEntity baseFace = faceCollection.First();
			CreateChamfer(baseFace, length);
		}

		private void BuildGroove()
		{
			ksEntity planeOffset = _part.NewEntity(14);
			ksPlaneOffsetDefinition planeOffsetDefinition = 
				planeOffset.GetDefinition();

			planeOffsetDefinition.direction = true;
			planeOffsetDefinition.offset = _parameters.LengthValve -
			                               _parameters.DistanceGroove;

			ksEntity planeXOY = _part.GetDefaultEntity(1);
			planeOffsetDefinition.SetPlane(planeXOY);

			planeOffset.Create();
			ksEntity sketch = _part.NewEntity(5);
			ksSketchDefinition sketchDefinition = sketch.GetDefinition();

			sketchDefinition.SetPlane(planeOffset);
			sketch.Create();

			ksDocument2D document2D = sketchDefinition.BeginEdit();
			CreateCircle(document2D,_parameters.DiameterStem);
			CreateCircle(document2D,_parameters.DiameterStem -
			                        _parameters.DepthGroove);
			sketchDefinition.EndEdit();

			CreateCutExtrusion(_parameters.WidthGroove,sketch);
		}

		private void CreateCutExtrusion(double length, ksEntity sketch)
		{
			ksEntity cutExtrusion = _part.NewEntity(26);
			ksCutExtrusionDefinition cutExtrusionDefinition = 
				cutExtrusion.GetDefinition();

			
			cutExtrusionDefinition.SetSideParam(false, 0, length);
			cutExtrusionDefinition.SetSketch(sketch);
			cutExtrusion.Create();
		}
	}
}
