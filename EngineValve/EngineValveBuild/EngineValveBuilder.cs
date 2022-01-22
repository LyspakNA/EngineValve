﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EngineValveParameters;
using Kompas6API5;

namespace EngineValveBuild
{
	/// <summary>
	/// Строитель клапана
	/// </summary>
	public class EngineValveBuilder
	{
		/// <summary>
		/// Экземпляр класса параметров
		/// </summary>
		private EngineValveParameterCollection _parameters;

		/// <summary>
		/// Экземпляр компонента сборки
		/// </summary>
		private ksPart _part;

		/// <summary>
		/// Соединение с КОМПАС-3D
		/// </summary>
		private KompasConnector _kompas = new KompasConnector();
		
		/// <summary>
		/// Конструктор 
		/// </summary>
		/// <param name="parameters">Экземпляр параметров</param>
		public EngineValveBuilder(EngineValveParameterCollection parameters)
		{
			_kompas.Start();
			var document3D = _kompas.CreateDocument3D();
			_part = document3D.GetPart(-1);
			_parameters = parameters;
		}

		/// <summary>
		/// Сборка клапана
		/// </summary>
		public void BuildEngineValve()
		{
			BuildPlate();
			BuildStem();
			FilletPlate();
			ChamferPlate();
			BuildGroove();
			ChamferStem();
			NecklinePlate();
		}

		/// <summary>
		/// Строитель тарелки клапана
		/// </summary>
		private void BuildPlate()
		{
			//TODO: вынести в словарь констант
			ksEntity planeXOY = _part.GetDefaultEntity(1);
			var length = _parameters.ThicknessPlate + _parameters.LengthChamfer;
			CreateCylinder(planeXOY,_parameters.DiameterPlate,length);
		}

		/// <summary>
		/// Строитель ножки клапана
		/// </summary>
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

		/// <summary>
		/// Метод постройки круга
		/// </summary>
		/// <param name="document2D">Интерфейс графического документа</param>
		/// <param name="diameter">Диаметр круга</param>
		private void CreateCircle(ksDocument2D document2D, double diameter)
		{
			document2D.ksCircle(0, 0, diameter / 2, 1);
		}
		/// <summary>
		/// Метод выдавливания
		/// </summary>
		/// <param name="length">Длина выдавливания</param>
		/// <param name="sketch">Эскиз выдавливания</param>
		private void CreateExtrusion(double length, ksEntity sketch)
		{
			ksEntity extrusion = _part.NewEntity(24);
			ksBaseExtrusionDefinition extrusionDefinition = extrusion.GetDefinition();

			extrusionDefinition.SetSideParam(true, 0, length);
			extrusionDefinition.SetSketch(sketch);
			extrusion.Create();
		}
		/// <summary>
		/// Метод создания цилиндра
		/// </summary>
		/// <param name="plane">Плоскость относительно которой
		/// будет выдавливаться цилиндр</param>
		/// <param name="diameter">Диаметр цилиндра</param>
		/// <param name="length">Длина цилиндра</param>
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

		/// <summary>
		/// Метод скругления
		/// </summary>
		/// <param name="face">Грань скругления</param>
		/// <param name="radius">Радиус скругления</param>
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

		/// <summary>
		/// Скругление тарелки клапана
		/// </summary>
		private void FilletPlate()
		{
			ksEntityCollection faceCollection = _part.EntityCollection(7);
			var length = _parameters.ThicknessPlate + _parameters.LengthChamfer;
			faceCollection.SelectByPoint(_parameters.DiameterStem/2, 0, length);

			ksEntity baseFace = faceCollection.First();
			CreateFillet(baseFace, _parameters.RadiusTransition);
		}

		/// <summary>
		/// Построитель фаски
		/// </summary>
		/// <param name="face">Грань, на которой будет создана фаска</param>
		/// <param name="length">Длина фаски</param>
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

		/// <summary>
		/// Построитель рабочей фаски клапана
		/// </summary>
		private void ChamferPlate()
		{
			ksEntityCollection faceCollection = _part.EntityCollection(7);
			var length = _parameters.ThicknessPlate + _parameters.LengthChamfer;
			faceCollection.SelectByPoint(_parameters.DiameterPlate / 2, 0, length);

			ksEntity baseFace = faceCollection.First();
			CreateChamfer(baseFace, _parameters.LengthChamfer);
		}

		/// <summary>
		/// Построитель фаски ножки
		/// </summary>
		private void ChamferStem()
		{
			const double length = 1.0;
			ksEntityCollection faceCollection = _part.EntityCollection(7);
			faceCollection.SelectByPoint(_parameters.DiameterStem/2, 
				0, _parameters.LengthValve);

			ksEntity baseFace = faceCollection.First();
			CreateChamfer(baseFace, length);
		}

		/// <summary>
		/// Построитель выреза под сухарь
		/// </summary>
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

		/// <summary>
		/// Метод вырезания выдавливанием
		/// </summary>
		/// <param name="length">Длина вырезания</param>
		/// <param name="sketch">Вырезаемый эскиз</param>
		private void CreateCutExtrusion(double length, ksEntity sketch)
		{
			ksEntity cutExtrusion = _part.NewEntity(26);
			ksCutExtrusionDefinition cutExtrusionDefinition = 
				cutExtrusion.GetDefinition();

			
			cutExtrusionDefinition.SetSideParam(false, 0, length);
			cutExtrusionDefinition.SetSketch(sketch);
			cutExtrusion.Create();
		}

		/// <summary>
		/// Построитель выреза в тарелке клапана
		/// </summary>
		private void NecklinePlate()
		{
			if (_parameters.CreateNeckline)
			{
				ksEntity planeXOZ = _part.GetDefaultEntity(2);
				ksEntity sketch = _part.NewEntity(5);
				ksSketchDefinition sketchDefinition = sketch.GetDefinition();

				sketchDefinition.SetPlane(planeXOZ);
				sketch.Create();

				ksDocument2D document2D = sketchDefinition.BeginEdit();

				var ellips = CreateEllips(document2D, _parameters.DiameterNeckline/2,
					_parameters.DepthNeckline/2);
				document2D.ksLineSeg(0, 0, 0, 10, 3);
				document2D.ksTrimmCurve(ellips, 0, _parameters.DepthNeckline/2
					, 0, -_parameters.DepthNeckline/2, _parameters.DiameterNeckline/2
					, 0, 1);

				sketchDefinition.EndEdit();
				CreateCutRotation(sketch);
			}

		}

		/// <summary>
		/// Вырезание вращением
		/// </summary>
		/// <param name="sketch">Эскиз для вырезания</param>
		private void CreateCutRotation(ksEntity sketch)
		{
			ksEntity cutRotated = _part.NewEntity(29);
			ksCutRotatedDefinition cutRotatedDefinition=
				cutRotated.GetDefinition();
			cutRotatedDefinition.SetSideParam(false, 360D);
			cutRotatedDefinition.SetSketch(sketch);
			cutRotated.Create();
		}

		/// <summary>
		/// Построитель эллипса
		/// </summary>
		/// <param name="document2D">Интерфейс графического документа</param>
		/// <param name="width">Ширина эллипса</param>
		/// <param name="height">Высота эллипса</param>
		/// <returns></returns>
		private int CreateEllips(ksDocument2D document2D, double width, double height)
		{
			KompasObject kompas = _kompas.KompasEntity;
			ksEllipseParam param = kompas.GetParamStruct(22);
			param.A = width;
			param.B = height;
			param.angle = 0;
			param.style = 1;
			param.xc = 0;
			param.yc = 0;

			return document2D.ksEllipse(param);
		}


	}
}
